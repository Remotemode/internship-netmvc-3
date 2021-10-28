using Microsoft.Extensions.DependencyInjection;
using MobileWarehouse.Repository.Implementation;
using MobileWarehouse.Repository.Interface;

namespace MobileWarehouse.Extensions
{
    public static class ServiceCollectionExtentions
    {
        public static void AddMobileWarehouseModule(this IServiceCollection service)
        {
            service.AddTransient<ITokenRepository, TokenRepository>();
        }
    }
}
