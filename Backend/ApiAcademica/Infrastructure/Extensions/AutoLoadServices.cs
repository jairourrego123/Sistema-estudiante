using Domain.Entities;
using Domain.Ports.IEstudianteRepository;
using Infrastructure.Adapters.GenericRepository;
using Infrastructure.DataSource;
using Infrastructure.Ports;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared.Const;

namespace Infrastructure.Extensions;

public static class AutoLoadServices
{
    public static IServiceCollection AddDomainServices(this IServiceCollection services)
    {

        services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));

        IEnumerable<Type> _repositories = AppDomain.CurrentDomain.GetAssemblies()
            .Where(assembly => (assembly.FullName is null) || assembly.FullName.Contains(ProjectConst.Infrastructure, StringComparison.InvariantCulture))
            .SelectMany(s => s.GetTypes())
            .Where(p => p.CustomAttributes.Any(x => x.AttributeType == typeof(RepositoryAttribute)));


        // Registra cada repositorio en el contenedor de dependencias
        foreach (Type repo in _repositories)
        {
            Type? interfaceType = repo.GetInterfaces().FirstOrDefault();
            if (interfaceType != null)
            {
                services.AddScoped(interfaceType, repo);
            }
        }

        return services;
    }
}
