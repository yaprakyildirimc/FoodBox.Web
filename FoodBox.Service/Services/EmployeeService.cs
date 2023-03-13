using FoodBox.Core;
using FoodBox.Core.Models;
using FoodBox.Core.Services;
using Microsoft.AspNetCore.Identity;
using System.Linq.Expressions;

namespace FoodBox.Service.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IUnitOfWork _unitOfWork;
		private readonly UserManager<Employee> _userManager;
		public EmployeeService(IUnitOfWork unitOfWork, UserManager<Employee> userManager)
        {
            _unitOfWork = unitOfWork;
			_userManager = userManager;
		}

        public async Task<Employee> Create(Employee entity)
        {
			string password = entity.PasswordHash;
            entity.PasswordHash = null;
			var result = await _userManager.CreateAsync(entity, password);
			//await _unitOfWork.Users.AddAsync(entity);

			//await _unitOfWork.CommitAsync();

			return entity;
		}

        public async Task Delete(Employee entity)
        {
            _unitOfWork.Employees.Remove(entity);
            await _unitOfWork.CommitAsync();
        }

        public async Task<IEnumerable<Employee>> GetAll()
        {
            return await _unitOfWork.Employees.GetAllAsync();
        }

        public async Task<IEnumerable<Employee>> GetAll(Expression<Func<Employee, bool>> predicate)
        {
            return await _unitOfWork.Employees.Find(predicate);
        }

        public async Task<Employee> GetById(Guid id)
        {
            string _Id = id.ToString();
            var result = await _unitOfWork.Employees.Find(x => x.Id == _Id);

            return result.FirstOrDefault();
        }

        public List<Employee> List()
        {
            throw new NotImplementedException();
        }

        public List<Employee> List(Expression<Func<Employee, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public async Task Update(Employee entity)
        {
            string _Id = entity.Id.ToString();
            var user =await _unitOfWork.Employees.Find(x=>x.Id == _Id);

            var currentUser=user.FirstOrDefault();

            if (currentUser != null)
            {
                currentUser.FirstName = entity.FirstName;
                currentUser.LastName = entity.LastName;
                currentUser.Email = entity.Email;

                _unitOfWork.Employees.Update(currentUser);
                await _unitOfWork.CommitAsync();
            }

        }
    }
}
