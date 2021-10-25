using Microsoft.Extensions.DependencyInjection;
using MobileWarehouse.Entity.Repository.Implementation;
using MobileWarehouse.Entity.Repository.Interface;

namespace MobileWarehouse.Entity.Extensions
{
    public static class ServiceCollectionExtentions
    {
        public static void AddEFModule(this IServiceCollection service)
        {
            service.AddTransient<IUserRepository, UserRepository>();
        }
    }
}
