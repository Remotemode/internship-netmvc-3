using Microsoft.EntityFrameworkCore;
using MobileWarehouse.Common.Models;
using MobileWarehouse.Entity.Models;
using MobileWarehouse.Entity.Repository.Interface;
using System;
using System.Threading.Tasks;

namespace MobileWarehouse.Entity.Repository.Implementation
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationContext _applicationContext;

        public UserRepository(ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext ?? throw new ArgumentNullException(nameof(applicationContext));
        }

        public async Task<User> AddUserToDbAsync(RegisterModel model)
        {
            var user = await _applicationContext.Users.FirstOrDefaultAsync(u => u.Email == model.Email);
            if (user == null)
            {
                user = new User { Email = model.Email, Password = model.Password };
                var userRole = await _applicationContext.Roles.FirstOrDefaultAsync(r => r.Name == "user");
                if (userRole != null)
                    user.Role = userRole;

                await _applicationContext.Users.AddAsync(user);
                await _applicationContext.SaveChangesAsync();
            }
            return user;
        }

        public async Task<User> GetUserFromDbAsync(LoginModel model)
        {
            var user = await _applicationContext.Users
                    .Include(u => u.Role)
                    .FirstOrDefaultAsync(u => u.Email == model.Email && u.Password == model.Password);
            return user;
        }

    }
}
