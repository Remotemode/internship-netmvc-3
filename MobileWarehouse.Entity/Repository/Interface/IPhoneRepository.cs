using MobileWarehouse.Entity.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MobileWarehouse.Entity.Repository.Interface
{
    public interface IPhoneRepository
    {
        Task<List<Phone>> GetPhoneListAsync();
    }
}
