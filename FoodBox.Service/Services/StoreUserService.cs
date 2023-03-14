using FoodBox.Core;
using FoodBox.Core.Models;
using FoodBox.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FoodBox.Service.Services
{
    public class StoreUserService : IStoreUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        public StoreUserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<StoreUser> Create(StoreUser entity)
        {
            await _unitOfWork.StoreUsers.AddAsync(entity);

            await _unitOfWork.CommitAsync();

            return entity;
        }

        public async Task Delete(StoreUser entity)
        {
            _unitOfWork.StoreUsers.Remove(entity);
            await _unitOfWork.CommitAsync();
        }

        public async Task<IEnumerable<StoreUser>> GetAll()
        {
            return await _unitOfWork.StoreUsers.GetAllAsync();
        }

        public async Task<IEnumerable<StoreUser>> GetAll(Expression<Func<StoreUser, bool>> predicate)
        {
            return await _unitOfWork.StoreUsers.Find(predicate);
        }

        public async Task<StoreUser> GetById(Guid id)
        {
            return await _unitOfWork.StoreUsers.GetByIdAsync(id);
        }

        public List<StoreUser> List()
        {
            throw new NotImplementedException();
        }

        public List<StoreUser> List(Expression<Func<StoreUser, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public async Task Update(StoreUser entity)
        {
            _unitOfWork.StoreUsers.Update(entity);
            await _unitOfWork.CommitAsync();
        }
    }
}
