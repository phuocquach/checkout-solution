using MediatR;
using System.Reflection;

namespace CheckoutService.Extension
{
    public static class ServiceExtension
    {
        public static void RegisterServicesAsScoped(this IServiceCollection services, params Assembly[] assemblies)
        {
            var allPublicTypes = assemblies.SelectMany(x => x.GetExportedTypes())
                .Where(y => !y.IsAbstract && y.IsClass).ToHashSet();
            foreach (var item in allPublicTypes)
            {
                var implementedInteface = item.GetTypeInfo().ImplementedInterfaces.FirstOrDefault();
                if (implementedInteface != null && !implementedInteface.IsGenericType)
                {
                    services.Add(new ServiceDescriptor(implementedInteface, item, ServiceLifetime.Scoped));
                }
            }
        }
    }
}
