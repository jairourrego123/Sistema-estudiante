namespace Application.Ports.Services;

public interface IEmailTemplateService
{
    string RenderizarPlantilla(string plantillaNombre, Dictionary<string, string> parametros);
}
