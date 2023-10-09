namespace PocEstrutura.Repositorio.Interface
{
    public interface IRepositorioBase<T> where T : class
    {
        Task Adicionar(T TModel);
        Task Atualizar(T TModel);
        Task<IEnumerable<T>> ListarTodos();
        Task<T> BuscarPorId(Guid Id);
        Task Deletar(T TModel);

    }
}
