
using Dapper.Contrib.Extensions;
using SecureIdentity.Password;

namespace PocEstrutura.Models
{
    [Table("[Perfil]")]
    public class Perfil : Entidade
    {
        public Perfil()
        {

        }
        public Perfil(string nome, string email, bool ativo)
        {
            Nome = nome;
            Email = email;
            Ativo = ativo;
        }

        public void Atualizar(string nome, string email, bool ativo)
        {
            Nome = nome;
            Email = email;
            Ativo = ativo;
        }

        public void AtualizarEmailNome(string email, string nome)
        {
            Email = email;
            Nome = nome;
        }

        public void SetarSenha(string senha)
        {
            Senha = PasswordHasher.Hash(senha);
        }

        public string Nome { get; private set; }
        public string Email { get; private set; }
        public string Senha { get; private set; }
        public bool Ativo { get; private set; }
    }
}
