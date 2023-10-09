using Flunt.Notifications;
using PocEstrutura.Comando;
using PocEstrutura.Entrada;
using PocEstrutura.Models;
using PocEstrutura.Repositorio.Interface;
using PocEstrutura.UnitOfWork;

namespace PocEstrutura.Manipuladores
{
    public class ComandoManipuladorPerfil : Notifiable, IComandoManipulador<ComandoManipuladoAdicionarAdmin>, IComandoManipulador<ComandoManipuladoAtualizarAdmin>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPerfilRepositorio _perfilRepositorio;
        private readonly IPerfilRoleRepositorio _perfilRoleRepositorio;
        private readonly IRoleRepositorio _roleRepositorio;

        public ComandoManipuladorPerfil(IUnitOfWork unitOfWork, IPerfilRepositorio perfilRepositorio,
           IPerfilRoleRepositorio perfilRoleRepositorio, IRoleRepositorio roleRepositorio)
        {
            _unitOfWork = unitOfWork;
            _perfilRepositorio = perfilRepositorio;
            _perfilRoleRepositorio = perfilRoleRepositorio;
            _roleRepositorio = roleRepositorio;
        }

        public async Task<IComandoSaida> manipulador(ComandoManipuladoAdicionarAdmin comando)
        {
            Perfil perfil = new Perfil(comando.Nome, comando.Email, comando.Ativo);

            perfil.SetarSenha(comando.Senha);
            AddNotifications(comando.Notifications);
            if (_perfilRepositorio.VerificarSeUserExiste(comando.Email))
            {
                return new ComandoSaida(false, "Esse E-mail já Está Cadastrado", perfil);
            }

            try
            {
                _unitOfWork.BeginTransaction();
                await _perfilRepositorio.Adicionar(perfil);
                foreach (var item in comando.Roles)
                {
                    Role role = await _roleRepositorio.BuscarRolePorNome(item);
                    if (role != null)
                    {
                        PerfilRole perfilRole = new PerfilRole(perfil.Id, role.Id);
                        await _perfilRoleRepositorio.Adicionar(perfilRole);
                    }
                    else
                    {
                        _unitOfWork.Rollback();
                        return new ComandoSaida(false, "Erro ao Cadastrar Perfil, Role não encontrado", null);
                    }
                }
                _unitOfWork.Commit();
                return new ComandoSaida(true, "Perfil Cadastrado Com Sucesso", perfil);
            }
            catch (System.Exception)
            {
                _unitOfWork.Rollback();
                return new ComandoSaida(false, "Erro ao Cadastrar Perfil", null);
            }
        }

        public async Task<IComandoSaida> manipulador(ComandoManipuladoAtualizarAdmin comando)
        {
            Perfil perfil = await _perfilRepositorio.BuscarPorId(comando.Id);
            perfil.Atualizar(comando.Nome, comando.Email, comando.Ativo);
            AddNotifications(comando.Notifications);
            if (comando.Invalid)
            {
                return new ComandoSaida(false, "Erro ao Editar Administrador", perfil);
            }
            try
            {
                _unitOfWork.BeginTransaction();
                await _perfilRepositorio.Atualizar(perfil);
                _unitOfWork.Commit();
                return new ComandoSaida(true, "Perfil Atualizado Com Sucesso", perfil);
            }
            catch (System.Exception)
            {
                _unitOfWork.Rollback();
                return new ComandoSaida(false, "Erro ao Atualizar Perfil", perfil);
            }
        }
    }
}
