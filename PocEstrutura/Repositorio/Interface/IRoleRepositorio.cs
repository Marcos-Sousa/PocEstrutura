using PocEstrutura.Models;

namespace PocEstrutura.Repositorio.Interface
{
    public interface IRoleRepositorio
    {
        Task<Role> BuscarRolePorNome(string nome);
        Task<Role> BuscarPorId(Guid Id);
        Task<IEnumerable<Role>> ListarTodas();
    }
}