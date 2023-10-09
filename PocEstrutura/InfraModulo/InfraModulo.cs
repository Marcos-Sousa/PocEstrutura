using PocEstrutura.Manipuladores;
using PocEstrutura.Repositorio.Interface;
using PocEstrutura.Repositorio.Repositorio;
using PocEstrutura.UnitOfWork;

namespace PocEstrutura.InfraModulo
{
    public static class InfraModulo
    {
        public static IServiceCollection AddApplicationInfra(this IServiceCollection services)
        {
            services
                .AddApplicationServices();

            return services;
        }

        private static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddTransient<IUnitOfWork, UnitOfWork.UnitOfWork>();
            services.AddTransient<IPerfilRepositorio, PerfilRepositorio>();
            services.AddTransient<IPerfilRoleRepositorio, PerfilRoleRepositorio>();
            services.AddTransient<IRoleRepositorio, RoleRepositorio>();
            services.AddTransient<ComandoManipuladorPerfil, ComandoManipuladorPerfil>();

            return services;
        }
    }
}
