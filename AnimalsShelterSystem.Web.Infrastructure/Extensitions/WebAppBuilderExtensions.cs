

namespace AnimalsShelterSystem.Web.Infrastructure.Extensitions
{
    using System.Reflection;
    using Microsoft.Extensions.DependencyInjection;
    public static class WebAppBuilderExtensions
    {
        public static void AddAppService(this IServiceCollection service,Type typeService)
        {
            Assembly? serviceAssembly = Assembly.GetAssembly(typeService);
            if(serviceAssembly == null)
            {
                throw new InvalidOperationException("Invalid type");
            }
            Type[] serviceTypes= serviceAssembly.GetTypes().Where(s=>s.Name.EndsWith("Service")&& !s.IsInterface)
                .ToArray();

            foreach(var type in serviceTypes)
            {
                Type? interfaceType = type.GetInterface($"I{type.Name}");
                if(interfaceType==null)
                {
                    throw new InvalidOperationException("Invalid interface");
                }
                service.AddScoped(interfaceType, type);
            }
          
        }
    }
}
