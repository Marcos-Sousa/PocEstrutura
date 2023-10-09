using Dapper.Contrib.Extensions;

namespace PocEstrutura.Models
{
    public abstract class Entidade
    {
        protected Entidade()
        {
            Id = Guid.NewGuid();
        }

        [ExplicitKey]
        public Guid Id { get; set; }
    }
}
