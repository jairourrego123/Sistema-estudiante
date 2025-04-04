using System.ComponentModel.DataAnnotations;

namespace Application.Dtos.Notificacion;

public class ParamatrosNotificacionDto
{
    [EmailAddress]
    public  required string Destinatario { get; set; }
    public required string Asunto    { get; set; }
    public required string Mensaje { get; set; }

}
