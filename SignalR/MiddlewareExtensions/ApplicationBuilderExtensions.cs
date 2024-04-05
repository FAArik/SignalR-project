using SignalR.SubscribeTableDependencies;

namespace SignalR.MiddlewareExtensions;

public static class ApplicationBuilderExtensions
{
    public static void UseEarningTableDependency(this IApplicationBuilder applicationBuilder)
    {
        var serviceProvider = applicationBuilder.ApplicationServices;
        var service = serviceProvider.GetService<SubscribeEarningTableDependency>();
        service.SubscribeTableDependency();
    }

}
