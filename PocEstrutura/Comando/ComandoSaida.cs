namespace PocEstrutura.Comando
{
    public class ComandoSaida : IComandoSaida
    {
        public ComandoSaida(bool sucesso, string mensagem, object data)
        {
            Sucesso = sucesso;
            Mensagem = mensagem;
            Data = data;
        }

        public bool Sucesso { get; set; }
        public string Mensagem { get; set; }
        public object Data { get; set; }

        public void Validate()
        {
            throw new NotImplementedException();
        }
    }
}
