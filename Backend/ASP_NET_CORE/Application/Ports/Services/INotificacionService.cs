using Application.Dtos.Notificacion;

namespace Application.Ports.Services;

public interface INotificacionService
{
    Task EnviarNotificacionEmailAsync(ParamatrosNotificacionDto paramatrosNotificacionDto);
}
