namespace PocEstrutura.Comando
{
    public interface IComandoManipulador<T> where T : IComandoEntrada
    {
        Task<IComandoSaida> manipulador(T comando);
    }

}
