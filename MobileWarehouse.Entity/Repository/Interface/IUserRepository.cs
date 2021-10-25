using MobileWarehouse.Common.Models;
using MobileWarehouse.Entity.Models;
using System.Threading.Tasks;

namespace MobileWarehouse.Entity.Repository.Interface
{
    public interface IUserRepository
    {
        Task<User> AddUserToDb(RegisterModel model);

        Task<User> FindUserFromDb(LoginModel model);
    }
}
