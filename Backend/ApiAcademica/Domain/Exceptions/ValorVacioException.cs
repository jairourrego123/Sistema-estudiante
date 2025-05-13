namespace Domain.Exceptions;

public class ValorVacioException : BaseDomainException
{
    public ValorVacioException(string valor )
        : base(string.Format(Messages.ValorVacio,valor))
    {
    }
}
