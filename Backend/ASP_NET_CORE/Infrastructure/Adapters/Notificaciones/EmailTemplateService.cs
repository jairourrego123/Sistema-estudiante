using Application.Ports.Services;
using Infrastructure.Healpers;
using System.Text.RegularExpressions;

namespace Infrastructure.Adapters.Notificaciones;

public class EmailTemplateService : IEmailTemplateService
{
    public string RenderizarPlantilla(string plantillaNombre, Dictionary<string, string> parametros)
    {
        string contenido = TemplateLoaderHelper.CargarContenidoPlantilla(plantillaNombre);

        foreach (var parammetro in parametros)
        {
            contenido = Regex.Replace(contenido, $"{{{{{parammetro.Key}}}}}", parammetro.Value, RegexOptions.IgnoreCase);
        }

        return contenido;
    }
}