using FoodBox.Core;
using FoodBox.Core.Repositories;
using FoodBox.Data.Repository;

namespace FoodBox.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly FoodBoxDbContext _context;
        private EmployeeRepository _employeeRepository;
        private ProductRepository _productRepository;
        private StoreRepository _storeRepository;
        private StoreProductRepository _storeProductRepository;

        public UnitOfWork(FoodBoxDbContext context)
        {
            _context = context;
        }
        public IEmployeeRepository Employees => _employeeRepository = _employeeRepository ?? new EmployeeRepository(_context);
        public IStoreProductRepository StoreProducts => _storeProductRepository ?? new StoreProductRepository(_context);
        public IProductRepository Products => _productRepository = _productRepository ?? new ProductRepository(_context);
        public IStoreRepository Stores => _storeRepository = _storeRepository ?? new StoreRepository(_context);

        public async Task<int> CommitAsync()
        {
            try
            {
                return await _context.SaveChangesAsync();
            }
            catch (Exception ex) 
            {
                throw;
            }
            
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
