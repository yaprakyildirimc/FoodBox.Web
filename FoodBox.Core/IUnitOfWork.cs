﻿using FoodBox.Core.Repositories;

namespace FoodBox.Core
{
    public interface IUnitOfWork : IDisposable
    {
        IEmployeeRepository Employees { get; }

        IProductRepository Products { get; }
        IStoreProductRepository StoreProducts { get; }
        IStoreRepository Stores { get; }
        IStoreUserRepository StoreUsers { get; }
        Task<int> CommitAsync();
    }
}
