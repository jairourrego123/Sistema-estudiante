using Microsoft.Extensions.Configuration;
using System.Net.Mail;
using System.Net;
using Application.Dtos.Notificacion;
using Infrastructure.Adapters.GenericRepository;
using Application.Ports.Services;

namespace Infrastructure.Adapters.Notificaciones;

[Repository]
public class NotificacionService : INotificacionService
{
    private readonly IConfiguration _configuration;
    private readonly IEmailTemplateService _emailTemplateService;
    private string smtpHost;
    private string smtpPort;
    private string usuario;
    private string remitente;
    private string contrasena;

    public NotificacionService(IConfiguration configuration, IEmailTemplateService emailTemplateService)
    {
        _configuration = configuration;
        _emailTemplateService = emailTemplateService;
        ObtenerParametrosIniciales(configuration);
    }

    private void ObtenerParametrosIniciales(IConfiguration configuration)
    {
        smtpHost = _configuration["Smtp:Host"] ?? throw new ArgumentNullException(nameof(smtpHost));
        smtpPort = _configuration["Smtp:Port"] ?? throw new ArgumentNullException(nameof(smtpPort));
        usuario = _configuration["Smtp:Username"] ?? throw new ArgumentNullException(nameof(usuario));
        contrasena = _configuration["Smtp:Password"] ?? throw new ArgumentNullException(nameof(contrasena));
        remitente = _configuration["Smtp:From"] ?? throw new ArgumentNullException(nameof(remitente)); // <- CORRECTO
    }

    public async Task EnviarNotificacionEmailAsync(ParamatrosNotificacionDto paramatrosNotificacion)
    {


        SmtpClient smtpClient = new()
        {
            Host = smtpHost, 
            Port = int.Parse(smtpPort),
            Credentials = new NetworkCredential(usuario, contrasena),
            EnableSsl = true
        };

        string mensaje = _emailTemplateService.RenderizarPlantilla(paramatrosNotificacion.Plantilla, paramatrosNotificacion.Parametros);

        MailMessage mailMessage = new()
        {
            From = new MailAddress(remitente),
            Subject = paramatrosNotificacion.Asunto,
            Body = mensaje,
            IsBodyHtml = true
        };
        mailMessage.To.Add(paramatrosNotificacion.Destinatario);

        await smtpClient.SendMailAsync(mailMessage);
    }


}
