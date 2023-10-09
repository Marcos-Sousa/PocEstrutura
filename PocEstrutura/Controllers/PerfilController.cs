using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PocEstrutura.Comando;
using PocEstrutura.Entrada;
using PocEstrutura.Manipuladores;
using PocEstrutura.Models;
using PocEstrutura.Repositorio.Interface;
using PocEstrutura.Repositorio.Repositorio;
using PocEstrutura.Saida;
using SecureIdentity.Password;

namespace PocEstrutura.Controllers
{
    [ApiController]
    [Route("perfil")]
    public class PerfilController : ControllerBase
    {
        private readonly IPerfilRepositorio _perfilRepositorio;
        private readonly IPerfilRoleRepositorio _perfilRoleRepositorio;
        private readonly IRoleRepositorio _roleRepositorio;
        private readonly ComandoManipuladorPerfil _comandoManipuladorAdmin;

        public PerfilController(IPerfilRoleRepositorio perfilRoleRepositorio, ComandoManipuladorPerfil comandoManipuladorAdmin,
            IPerfilRepositorio perfilRepositorio, IRoleRepositorio roleRepositorio)
        {
            _roleRepositorio = roleRepositorio;
            _perfilRepositorio = perfilRepositorio;
            _perfilRoleRepositorio = perfilRoleRepositorio;
            _comandoManipuladorAdmin = comandoManipuladorAdmin;
        }

        [HttpPost("cadastrar")]
        public async Task<IComandoSaida> Post([FromBody] ComandoManipuladoAdicionarAdmin comando)
        {
            var response = (ComandoSaida)await _comandoManipuladorAdmin.manipulador(comando);
            return response;
        }

        [HttpPut("atualizar")]
        [Authorize]
        public async Task<IComandoSaida> Put([FromBody] ComandoManipuladoAtualizarAdmin comando)
        {
            var response = (ComandoSaida)await _comandoManipuladorAdmin.manipulador(comando);
            return response;
        }

        [HttpPost]
        [Route("login")]
        public async Task<object> PostAsync([FromBody] ComandoManipuladorAutenticar comando)
        {
            if (comando == null)
                return new ComandoSaida(false, "Usúario Invalido", null);
            var perfil = await _perfilRepositorio.BuscarPorEmail(comando.Usuario.ToLower());
            if (perfil == null)
                return Ok(new ComandoSaida(false, "Perfil não encontrado", null));
            if (perfil.Ativo == false)
                return Ok(new ComandoSaida(false, "Perfil desativado, entre em contato com o Admin", null));
            if (!PasswordHasher.Verify(perfil.Senha, comando.Senha))
                return Ok(new ComandoSaida(false, "Usuário ou senha inválidos", null));


            List<ListarPerfilRole> perfilRoles = (List<ListarPerfilRole>)await _perfilRoleRepositorio.ListarPorPerfil(perfil.Id);
            List<string> roles = new List<string>();
            var rolePerfil = "";
            foreach (var item in perfilRoles)   
            {
                Role role = await _roleRepositorio.BuscarPorId(item.RoleId);
                rolePerfil += role.Nome;
                roles.Add(role.Nome);
            }

            try
            {
                var tkn = TokenService.GenerateToken(perfil.Nome, rolePerfil);

                object perfilAutenticado;

                perfilAutenticado = new
                {
                    id = perfil.Id,
                    email = perfil.Email,
                    nome = perfil.Nome,
                    role = roles,
                    token = tkn
                };
                ;
                return Ok(new ComandoSaida(true, "Autenticação realizada com sucesso", perfilAutenticado));
            }
            catch
            {
                return StatusCode(500, new ComandoSaida(false, "05X04 - Falha interna no servidor", null));
            }
        }
    }
}
