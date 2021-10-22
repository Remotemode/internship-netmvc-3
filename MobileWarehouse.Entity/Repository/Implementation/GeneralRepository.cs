using MobileWarehouse.Entity.Models;
using System;
using System.Threading.Tasks;


namespace MobileWarehouse.Entity.Repository.Implementation
{
    public class GeneralRepository
    {
        private readonly ApplicationContext _applicationContext;

        public GeneralRepository(ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext ?? throw new ArgumentNullException(nameof(applicationContext));
        }

        public async Task<T> CreateAsync<T>(T entity) where T : class
        {
            var entityModel = await _applicationContext.Set<T>().AddAsync(entity);
            await _applicationContext.SaveChangesAsync();

            return entityModel.Entity;
        }

        public async Task<T> UpdateAsync<T>(T entity) where T : class
        {
            var entityModel = await Task.Run(() => _applicationContext.Set<T>().Update(entity));
            await _applicationContext.SaveChangesAsync();

            return entityModel.Entity;
        }

        public async Task DeleteAsync<T>(T entity) where T : class
        {
            await Task.Run(() => _applicationContext.Set<T>().Remove(entity));
            await _applicationContext.SaveChangesAsync();
        }
    }
}
