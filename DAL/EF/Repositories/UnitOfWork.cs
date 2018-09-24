using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using DAL.DataContext;
using DAL.Entities;
using DAL.Interfaces;

namespace DAL.Repositories
{
    public class UnitOfWork:IUnitOfWork
    {
        private ProductContext db;
        private ProductRepository productRepository;
        private ProviderRepository providerRepository;
        private CategoryRepository categoryRepository;


        public UnitOfWork(string connection)
        {
            db = new ProductContext(connection);
        }


        public IRepository<Product> Products
        {
            get
            {
                if (productRepository == null)
                {
                    productRepository = new ProductRepository(db);
                }

                return productRepository;
            }

        }

        public IRepository<Provider> Providers
        {
            get
            {
                if (providerRepository == null)
                {
                    providerRepository = new ProviderRepository(db);
                }

                return providerRepository;
            }

        }

        public IRepository<Category> Categories
        {
            get
            {
                if (categoryRepository == null)
                {
                    categoryRepository = new CategoryRepository(db);
                }

                return categoryRepository;
            }

        }

        public void Save()
        {
            db.SaveChanges();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

    }
}
