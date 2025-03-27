using Application.Ports;
using Application.UseCases.Auth;
using Infrastructure.Adapters.GenericRepository;
using Infrastructure.DataSource;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Extensions
{
    public static class AutoLoadServices
    {
        public static IServiceCollection AddDomainServices(this IServiceCollection services)
        {
            services.AddSingleton<ISqlConnectionContext, DapperContext>();
            services.AddScoped<IAuthRepository, AuthService>(); 


            IEnumerable<Type> repositories = AppDomain.CurrentDomain.GetAssemblies()
                .Where(assembly => (assembly.FullName is null) || assembly.FullName.Contains("Infrastructure", StringComparison.InvariantCulture))
                .SelectMany(s => s.GetTypes())
                .Where(p => p.IsClass && p.GetCustomAttributes(typeof(RepositoryAttribute), true).Length > 0);

            // Registra cada repositorio en el contenedor de dependencias
            foreach (Type repo in repositories)
            {
                Type interfaceType = repo.GetInterfaces().Single();
                services.AddTransient(interfaceType, repo);
            }

            return services;
        }
    }
}
