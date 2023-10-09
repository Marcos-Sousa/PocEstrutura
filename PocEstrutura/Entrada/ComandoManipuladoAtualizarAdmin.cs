using Flunt.Notifications;
using Flunt.Validations;
using PocEstrutura.Comando;

namespace PocEstrutura.Entrada
{
    public class ComandoManipuladoAtualizarAdmin : Notifiable, IComandoEntrada
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public bool Ativo { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }

        public void Validate()
        {
            AddNotifications(
                new Contract()
                    .Requires()
                    .HasMinLen(Nome, 3, "Nome", "O Nome deve conter no miníno 3 carecteres!")
                    .HasMaxLen(Nome, 100, "Nome", "O Nome deve conter no máximo 100 carecteres!")
                    .IsEmail(Email, "Email", "Digite um e-mail valído")
                    .IsEmailOrEmpty(Email, "Email", "E-mail é um campo obrigatório")
                    .IsNotNull(Telefone, "Telefone", "O Telefone é um campo obrigatório")
            );
        }
    }
}
