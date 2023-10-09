using Dapper.Contrib.Extensions;

namespace PocEstrutura.Models
{
    [Table("[PerfilRole]")]
    public class PerfilRole
    {
        public PerfilRole()
        {

        }
        public PerfilRole(Guid perfilId, Guid roleId)
        {
            PerfilId = perfilId;
            RoleId = roleId;
        }

        public Guid PerfilId { get; private set; }
        public Guid RoleId { get; private set; }
    }
}
