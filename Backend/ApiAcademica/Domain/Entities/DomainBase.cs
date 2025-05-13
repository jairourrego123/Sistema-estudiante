
namespace Domain.Entities;

public abstract class DomainBase
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public DateTime FechaGrabacion { get; set; } = DateTime.UtcNow;
}
