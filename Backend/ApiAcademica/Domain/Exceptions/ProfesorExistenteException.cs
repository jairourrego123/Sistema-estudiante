namespace Domain.Exceptions;

public class ProfesorExistenteException : BaseDomainException
{
    public ProfesorExistenteException()
        : base(string.Format(Messages.ProfesorRepetido))
    {
    }
}
