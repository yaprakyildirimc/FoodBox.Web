using FoodBox.Core;
using FoodBox.Core.Models;
using FoodBox.Core.Services;
using System.Linq.Expressions;

namespace FoodBox.Service.Services
{
    public class StoreProductService : IStoreProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        public StoreProductService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<StoreProduct> Create(StoreProduct entity)
        {
            await _unitOfWork.StoreProducts.AddAsync(entity);
            await _unitOfWork.CommitAsync();
            return entity;
        }

        public async Task Delete(StoreProduct entity)
        {
            _unitOfWork.StoreProducts.Remove(entity);
            await _unitOfWork.CommitAsync();
        }

        public async Task<IEnumerable<StoreProduct>> GetAll()
        {
            return await _unitOfWork.StoreProducts.GetAllAsync();
        }

        public async Task<IEnumerable<StoreProduct>> GetAll(Expression<Func<StoreProduct, bool>> predicate)
        {
            return await _unitOfWork.StoreProducts.Find(predicate);
        }

        public async Task<StoreProduct> GetById(Guid id)
        {
           return await _unitOfWork.StoreProducts.GetByIdAsync(id);
        }

        public List<StoreProduct> List()
        {
            throw new NotImplementedException();
        }

        public List<StoreProduct> List(Expression<Func<StoreProduct, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public async Task Update(StoreProduct entity)
        {
            _unitOfWork.StoreProducts.Update(entity);
            await _unitOfWork.CommitAsync();
        }
    }
}
