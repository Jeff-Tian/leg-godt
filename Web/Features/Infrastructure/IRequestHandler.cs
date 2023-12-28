namespace Web.Features.Infrastructure;

public interface IRequestHandler<in TRequest, TResponse>
    where TRequest : notnull
    where TResponse : notnull
{
    Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken);
}

public interface IRequestHandler<TResponse>
    where TResponse : notnull
{
    Task<TResponse> Handle(CancellationToken cancellationToken);
}

