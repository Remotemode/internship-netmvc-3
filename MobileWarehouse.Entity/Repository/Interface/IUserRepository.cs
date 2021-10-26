using MobileWarehouse.Common.Models;
using MobileWarehouse.Entity.Models;
using System.Threading.Tasks;

namespace MobileWarehouse.Entity.Repository.Interface
{
    public interface IUserRepository
    {
        Task<User> AddUserToDbAsync(RegisterModel model);

        Task<User> FindUserFromDbAsync(LoginModel model);
    }
}
