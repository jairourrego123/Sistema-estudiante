using Application.Ports.Services;
using Infrastructure.Adapters.GenericRepository;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Adapters;

[Repository]
public class ConfiguracionAppService : IConfiguracionAppService
{
    private readonly IConfiguration _configuration;

    public ConfiguracionAppService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string ObtenerUrlFrontend()
    {
        return _configuration["Frontend:BaseUrl"] ?? throw new ArgumentNullException("Frontend:BaseUrl");
    }
}
