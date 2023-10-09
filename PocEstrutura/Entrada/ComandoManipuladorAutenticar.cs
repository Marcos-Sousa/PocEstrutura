using PocEstrutura.Comando;

namespace PocEstrutura.Entrada
{
    public class ComandoManipuladorAutenticar : IComandoEntrada
    {
        public string Usuario { get; set; }
        public string Senha { get; set; }

        public void Validate()
        {
            throw new NotImplementedException();
        }
    }
}
