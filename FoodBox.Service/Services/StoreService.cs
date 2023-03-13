using FoodBox.Core;
using FoodBox.Core.Models;
using FoodBox.Core.Services;
using System.Linq.Expressions;

namespace FoodBox.Service.Services
{
    public class StoreService : IStoreService
    {
        private readonly IUnitOfWork _unitOfWork;
        public StoreService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Store> Create(Store entity)
        {
            await _unitOfWork.Stores.AddAsync(entity);
            await _unitOfWork.CommitAsync();
            return entity;
        }

        public async Task Delete(Store entity)
        {
            _unitOfWork.Stores.Remove(entity);
            await _unitOfWork.CommitAsync();
        }

        public async Task<IEnumerable<Store>> GetAll()
        {
            return await _unitOfWork.Stores.GetAllAsync();
        }

        public async Task<IEnumerable<Store>> GetAll(Expression<Func<Store, bool>> predicate)
        {
            return await _unitOfWork.Stores.Find(predicate);
        }

        public async Task<Store> GetById(Guid id)
        {
            return await _unitOfWork.Stores.GetByIdAsync(id);
        }

        public List<Store> List()
        {
            throw new NotImplementedException();
        }

        public List<Store> List(Expression<Func<Store, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public async Task Update(Store entity)
        {
            _unitOfWork.Stores.Update(entity);
            await _unitOfWork.CommitAsync();
        }
    }
}
