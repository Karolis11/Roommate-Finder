using Castle.DynamicProxy;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Diagnostics.CodeAnalysis;

namespace roommate_app.Interceptors;
[ExcludeFromCodeCoverage]
public static class InterceptionExtensions
{
    public static void AddInterceptedService<TInterface, TImplementation, TInterceptor>(
        this IServiceCollection services)
        where TInterface : class
        where TImplementation : class, TInterface
        where TInterceptor : class, IInterceptor
    {
        services.TryAddScoped<IProxyGenerator, ProxyGenerator>();
        services.AddScoped<TImplementation>();
        services.TryAddTransient<TInterceptor>();
        services.AddScoped(provider =>
        {
            var proxyGenerator = provider.GetRequiredService<IProxyGenerator>();
            var impl = provider.GetRequiredService<TImplementation>();
            var interceptor = provider.GetRequiredService<TInterceptor>();
            return proxyGenerator.CreateInterfaceProxyWithTarget(impl, interceptor);
        });
    }
}

