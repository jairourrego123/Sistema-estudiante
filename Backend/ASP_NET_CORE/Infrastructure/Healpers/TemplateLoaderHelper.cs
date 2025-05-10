using Shared.Resources;

namespace Infrastructure.Healpers;

public static class TemplateLoaderHelper
{
    private static readonly string BaseTemplatePath = Path.Combine(AppContext.BaseDirectory, "Resources", "EmailTemplates");

    /// <summary>
    /// Resuelve la ruta absoluta de una plantilla HTML dada y valida su existencia.
    /// </summary>
    /// <param name="plantillaNombre">Nombre de la plantilla sin extensión, puede incluir subcarpetas </param>
    /// <returns>Contenido del archivo HTML</returns>
    public static string CargarContenidoPlantilla(string nombrePlantilla)
    {
        string rutaPlantilla = Path.Combine(BaseTemplatePath, nombrePlantilla + ".html");

        if (!File.Exists(rutaPlantilla))
            throw new FileNotFoundException(string.Format(Messages.ArchivoNoEncontrado, nombrePlantilla));

        return File.ReadAllText(rutaPlantilla);
    }
}