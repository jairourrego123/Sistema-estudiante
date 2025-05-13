namespace Domain.Exceptions;

public class LimiteMateriasDictadasExceptions : BaseDomainException
{
    public LimiteMateriasDictadasExceptions(int limite)
        : base(string.Format(Messages.LimiteMateriasADictar, limite))
{
}

}
