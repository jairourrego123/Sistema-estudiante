
using Application.Dtos.Notificacion;

namespace Application.Ports;

internal interface INotificacionRepository
{
    Task EnviarNotificacionEmail(ParamatrosNotificacionDto paramatrosNotificacionDto);
}
