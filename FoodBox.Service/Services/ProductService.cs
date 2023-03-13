using FoodBox.Core;
using FoodBox.Core.Models;
using FoodBox.Core.Services;
using System.Linq.Expressions;

namespace FoodBox.Service.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        public ProductService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Product> Create(Product entity)
        {
            await _unitOfWork.Products.AddAsync(entity);
            await _unitOfWork.CommitAsync();
            return entity;
        }

        public async Task Delete(Product entity)
        {
            _unitOfWork.Products.Remove(entity);
            await _unitOfWork.CommitAsync();
        }

        public async Task<IEnumerable<Product>> GetAll()
        {
           return await _unitOfWork.Products.GetAllAsync();
        }

        public async Task<IEnumerable<Product>> GetAll(Expression<Func<Product, bool>> predicate)
        {
            return await _unitOfWork.Products.Find(predicate);
        }

        public async Task<Product> GetById(Guid id)
        {
            return await _unitOfWork.Products.GetByIdAsync(id);
        }

        public List<Product> List()
        {
            throw new NotImplementedException();
        }

        public List<Product> List(Expression<Func<Product, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public async Task Update(Product entity)
        {
            _unitOfWork.Products.Update(entity);
            await _unitOfWork.CommitAsync();
        }
    }
}
