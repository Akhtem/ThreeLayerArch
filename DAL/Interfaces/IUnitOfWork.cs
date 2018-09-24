using System;

using DAL.Entities;

namespace DAL.Interfaces
{
    public interface IUnitOfWork:IDisposable
    {
        IRepository<Product> Products { get; }

        IRepository<Category> Categories { get; }

        IRepository<Provider> Providers { get; }

        void Save();
    }
}
