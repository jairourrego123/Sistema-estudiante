namespace Application.Dtos.Notificacion;

public class ParamatrosNotificacionDto
{
    public  required string Destinatario { get; set; }
    public required string Asunto    { get; set; }
    public required string Plantilla { get; set; }
    public required Dictionary<string, string> Parametros { get; set; }

}
