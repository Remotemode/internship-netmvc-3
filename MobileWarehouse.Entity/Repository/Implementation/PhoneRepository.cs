using MobileWarehouse.Entity.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MobileWarehouse.Entity.Repository.Interface;

namespace MobileWarehouse.Entity.Repository.Implementation
{
    public class PhoneRepository : IPhoneRepository
    {
        private readonly ApplicationContext _applicationContext;

        public PhoneRepository(ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext ?? throw new ArgumentNullException(nameof(applicationContext));
        }

        public async Task<List<Phone>> GetPhoneListAsync()
        {
            var phoneList = await _applicationContext.Phones.ToListAsync();
            return phoneList;
        }
    }
}
