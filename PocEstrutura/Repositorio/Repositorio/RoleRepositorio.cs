using Dapper;
using PocEstrutura.Database;
using PocEstrutura.Models;
using PocEstrutura.Repositorio.Interface;

namespace PocEstrutura.Repositorio.Repositorio
{
    public class RoleRepositorio : IRoleRepositorio
    {
        private DbSession _session;

        public RoleRepositorio(DbSession session)
        {
            _session = session;
        }

        public async Task<Role> BuscarPorId(Guid Id)
        {
            var sql = "SELECT * FROM Role WHERE Id = @Id";
            var response = await _session.Connection.QueryFirstOrDefaultAsync<Role>(sql, new { Id = Id }, _session.Transaction);
            return response;
        }

        public async Task<Role> BuscarRolePorNome(string nome)
        {
            var sql = "SELECT * FROM Role WHERE Nome = @Nome";
            var response = await _session.Connection.QueryFirstOrDefaultAsync<Role>(sql, new { Nome = nome }, _session.Transaction);
            return response;
        }

        public async Task<IEnumerable<Role>> ListarTodas()
        {
            var sql = "SELECT * FROM Role WHERE Nome = 'Cadastro' OR Nome = 'Financeiro' OR Nome = 'Administrador' OR Nome = 'Obra' OR Nome = 'Relatório'";
            var response = await _session.Connection.QueryAsync<Role>(sql, _session.Transaction);
            return response;
        }
    }
}