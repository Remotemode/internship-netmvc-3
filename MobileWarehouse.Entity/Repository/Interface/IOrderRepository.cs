using MobileWarehouse.Entity.Models;
using System.Threading.Tasks;

namespace MobileWarehouse.Entity.Repository.Interface
{
    public interface IOrderRepository
    {
        Task AddOrderAsync(Order order);
    }
}
