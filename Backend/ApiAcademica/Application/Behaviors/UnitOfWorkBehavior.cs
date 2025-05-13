using Application.Ports;
using MediatR;

public class UnitOfWorkBehavior<TRequest, TResponse>
    : IPipelineBehavior<TRequest, TResponse>
{
    private readonly IUnidadDeTrabajo _unidadTrabajo;
    public UnitOfWorkBehavior(IUnidadDeTrabajo unidadTrabajo) => _unidadTrabajo = unidadTrabajo;

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        TResponse? response = await next();
        await _unidadTrabajo.CommitAsync();
        return response;
    }
}