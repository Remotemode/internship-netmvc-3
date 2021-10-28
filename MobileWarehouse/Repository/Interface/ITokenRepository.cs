using MobileWarehouse.Common.Models;
using System.Threading.Tasks;

namespace MobileWarehouse.Repository.Interface
{
    public interface ITokenRepository
    {
        Task<object> GetToken(LoginModel model);

        bool ValidateToken(string token);
    }
}
