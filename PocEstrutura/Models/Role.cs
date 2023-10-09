using Dapper.Contrib.Extensions;

namespace PocEstrutura.Models
{
    [Table("[Role]")]

    public class Role
    {
        public Role(Guid id, string nome)
        {
            Id = id;
            Nome = nome;
        }

        public Guid Id { get; set; }
        public string Nome { get; set; }
    }
}
