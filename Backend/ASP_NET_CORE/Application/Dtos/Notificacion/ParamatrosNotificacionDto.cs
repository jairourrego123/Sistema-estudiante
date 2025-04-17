namespace Application.Dtos.Notificacion;

public class ParamatrosNotificacionDto
{
    public  required string Destinatario { get; set; }
    public required string Asunto    { get; set; }
    public required string Mensaje { get; set; }

}
