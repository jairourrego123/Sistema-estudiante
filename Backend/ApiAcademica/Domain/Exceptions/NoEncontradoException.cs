namespace Domain.Exceptions;

public class NoEncontradoException : BaseDomainException
{
    public NoEncontradoException(string valor )
        : base(string.Format(Messages.NoExiste,valor))
    {
    }
}
