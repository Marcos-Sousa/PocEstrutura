using Dapper;
using Dapper.Contrib.Extensions;
using PocEstrutura.Database;
using PocEstrutura.Models;
using PocEstrutura.Repositorio.Interface;

namespace PocEstrutura.Repositorio.Repositorio
{
    public class PerfilRepositorio : IPerfilRepositorio
    {
        private DbSession _session;
        public PerfilRepositorio(DbSession session)
        {
            _session = session;
        }
        public async Task Adicionar(Perfil perfil)
        => await _session.Connection.InsertAsync(perfil, _session.Transaction);

        public async Task Atualizar(Perfil perfil)
        => await _session.Connection.UpdateAsync(perfil, _session.Transaction);

        public async Task<Perfil> BuscarPorId(Guid Id)
        => await _session.Connection.GetAsync<Perfil>(Id, _session.Transaction);

        public async Task<IEnumerable<Perfil>> ListarTodos()
        => await _session.Connection.GetAllAsync<Perfil>(_session.Transaction);

        public async Task Deletar(Perfil perfil)
        {
            await _session.Connection.DeleteAsync(perfil, _session.Transaction);
        }

        public async Task<Perfil> BuscarPorEmail(string email)
        {
            var sql = "SELECT * FROM Perfil WHERE Email = @Email";
            var response = await _session.Connection.QueryFirstOrDefaultAsync<Perfil>(sql, new { Email = email }, _session.Transaction);
            return response;
        }

        public async Task AtualizarSenha(Perfil perfil)
        {
            var sql = "UPDATE Perfil SET Senha = @Senha WHERE Id = @Id";
            await _session.Connection.ExecuteAsync(sql, new
            {
                @Senha = perfil.Senha,
                @Id = perfil.Id
            }, _session.Transaction);
        }

        public async Task DeletarPerfilRole(Guid PerfilId)
        {
            var sql = "DELETE FROM PerfilRole WHERE PerfilId = @PerfilId";
            await _session.Connection.ExecuteAsync(sql, new { PerfilId = PerfilId }, _session.Transaction);
        }

        public bool VerificarSeUserExiste(string email)
        {
            var sql = "SELECT * FROM Perfil WHERE Email = @Email";
            var result = _session.Connection.Query(sql, new { @Email = email }, _session.Transaction);
            if (result.Count() != 0)
                return true;
            return false;
        }
    }
}
