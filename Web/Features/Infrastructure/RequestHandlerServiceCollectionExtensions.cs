namespace Web.Features.Infrastructure;

public static class RequestHandlerServiceCollectionExtensions
{
    public static IServiceCollection AddConfiguredRequestHandlers(this IServiceCollection services)
    {
        return services.Scan(s =>
        {
            s.FromCallingAssembly()
                .AddClasses(c => c.AssignableToAny(typeof(IRequestHandler<>), 
                    typeof(IRequestHandler<,>) 
                ))
                .AsSelf()
                .WithScopedLifetime();
        });
    }
}
