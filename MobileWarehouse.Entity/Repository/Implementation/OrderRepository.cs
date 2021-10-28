using MobileWarehouse.Entity.Models;
using MobileWarehouse.Entity.Repository.Interface;
using Serilog;
using System;
using System.Threading.Tasks;

namespace MobileWarehouse.Entity.Repository.Implementation
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationContext _applicationContext;

        public OrderRepository(ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext ?? throw new ArgumentNullException(nameof(applicationContext));
        }

        public async Task AddOrderAsync(Order order)
        {
            await _applicationContext.Orders.AddAsync(order);
            await _applicationContext.SaveChangesAsync();
        }
    }
}
