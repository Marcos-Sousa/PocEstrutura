using PocEstrutura.Models;

namespace PocEstrutura.Repositorio.Interface
{
    public interface IPerfilRepositorio : IRepositorioBase<Perfil>
    {
        Task AtualizarSenha(Perfil perfil);
        bool VerificarSeUserExiste(string email);
        Task<Perfil> BuscarPorEmail(string email);
        Task DeletarPerfilRole(Guid UserId);
    }
}
