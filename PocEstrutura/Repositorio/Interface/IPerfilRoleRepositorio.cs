using PocEstrutura.Models;
using PocEstrutura.Saida;

namespace PocEstrutura.Repositorio.Interface
{
    public interface IPerfilRoleRepositorio : IRepositorioBase<PerfilRole>
    {
        Task<IEnumerable<ListarPerfilRole>> ListarPorPerfil(Guid Id);
        Task DeletarPerfil(Guid PerfilId, Guid RoleId);
    }
}
