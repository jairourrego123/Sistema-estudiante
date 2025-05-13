namespace Domain.Exceptions;

public class LimiteMateriasInscritasException : BaseDomainException
{
    public LimiteMateriasInscritasException(int limite)
        : base(string.Format(Messages.LimiteMaximoDeMaterias,limite))
    {
    }
}
