using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using Castle.DynamicProxy;
using roommate_app.Other.Logger;

namespace roommate_app.Interceptors;
[ExcludeFromCodeCoverage]
public class DurationInterceptor : IInterceptor
{
    private readonly IDurationLogger _logger;

    public DurationInterceptor(IDurationLogger logger)
    {
        _logger = logger;
    }

    public void Intercept(IInvocation invocation)
    {
        var sw = Stopwatch.StartNew();
        try
        {
            invocation.Proceed();
        }
        finally
        {
            sw.Stop();
            _logger.Log(
                String.Format(
                    "{0} took {1}ms",
                    invocation.Method.Name,
                    sw.ElapsedMilliseconds
                )
            );
        }
    }
}

