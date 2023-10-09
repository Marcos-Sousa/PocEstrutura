using Dapper;
using Dapper.Contrib.Extensions;
using PocEstrutura.Database;
using PocEstrutura.Models;
using PocEstrutura.Repositorio.Interface;
using PocEstrutura.Saida;

namespace PocEstrutura.Repositorio.Repositorio
{
    public class PerfilRoleRepositorio : IPerfilRoleRepositorio
    {
        private DbSession _session;
        public PerfilRoleRepositorio(DbSession session)
        {
            _session = session;
        }

        public async Task Adicionar(PerfilRole perfilRole)
        => await _session.Connection.InsertAsync(perfilRole, _session.Transaction);

        public async Task Atualizar(PerfilRole perfilRole)
        => await _session.Connection.UpdateAsync(perfilRole, _session.Transaction);

        public async Task<PerfilRole> BuscarPorId(Guid Id)
        => await _session.Connection.GetAsync<PerfilRole>(Id, _session.Transaction);

        public async Task<IEnumerable<PerfilRole>> ListarTodos()
        => await _session.Connection.GetAllAsync<PerfilRole>(_session.Transaction);

        public async Task Deletar(PerfilRole perfilRole)
        {
            await _session.Connection.DeleteAsync(perfilRole, _session.Transaction);
        }

        public async Task<IEnumerable<ListarPerfilRole>> ListarPorPerfil(Guid PerfilId)
        {
            var sql = "SELECT pr.PerfilId, pr.RoleId, r.Nome FROM PerfilRole AS pr " +
                "INNER JOIN Role AS r ON r.Id = pr.RoleId " +
                "WHERE pr.PerfilId = @PerfilId ";
            var response = await _session.Connection.QueryAsync<ListarPerfilRole>(sql, new { PerfilId = PerfilId }, _session.Transaction);
            return response;
        }

        public async Task DeletarPerfil(Guid PerfilId, Guid RoleId)
        {
            var sql = "DELETE FROM PerfilRole WHERE PerfilId = @PerfilId AND RoleId = @RoleId";
            await _session.Connection.ExecuteAsync(sql, new { PerfilId = PerfilId, RoleId = RoleId }, _session.Transaction);
        }
    }
}