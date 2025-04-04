using Microsoft.Extensions.Configuration;
using System.Net.Mail;
using System.Net;
using Application.Dtos.Notificacion;

namespace Infrastructure.Adapters;

public class NotificacionRepository
{
    private readonly IConfiguration _configuration;
    private string smtpHost;
    private string smtpPort;
    private string usuario;
    private string contrasena;

    public NotificacionRepository(IConfiguration configuration)
    {
        _configuration = configuration;
        ObtenerParametrosIniciales(configuration);
    }

    private void ObtenerParametrosIniciales(IConfiguration configuration)
    {
        smtpHost = _configuration["Email:SmtpHost"] ?? throw new ArgumentNullException(nameof(smtpHost));
        smtpPort = _configuration["Email:SmtpPort"] ?? throw new ArgumentNullException(nameof(smtpPort));
        usuario = _configuration["Email:Usuario"] ?? throw new ArgumentNullException(nameof(usuario));
        contrasena = _configuration["Email:SmtpHost"] ?? throw new ArgumentNullException(nameof(contrasena));
    }
    public async Task EnviarCorreoAsync(ParamatrosNotificacionDto paramatrosNotificacion)
    {

        SmtpClient smtpClient = new()
        {
            Port = int.Parse(smtpHost),
            Credentials = new NetworkCredential(usuario, contrasena),
            EnableSsl = true
        };

        MailMessage mailMessage = new()
        {
            From = new MailAddress(usuario),
            Subject = paramatrosNotificacion.Asunto,
            Body = paramatrosNotificacion.Mensaje,
            IsBodyHtml = true
        };
        mailMessage.To.Add(paramatrosNotificacion.Destinatario);

        await smtpClient.SendMailAsync(mailMessage);
    }


}
