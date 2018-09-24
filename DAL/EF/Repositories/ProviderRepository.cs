using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using DAL.DataContext;
using DAL.Entities;
using DAL.Interfaces;

namespace DAL.Repositories
{
    public class ProviderRepository:IRepository<Provider>
    {
        private ProductContext db;

        public ProviderRepository(ProductContext db)
        {
            this.db = db;
        }

        public IEnumerable<Provider> GetAll()
        {
            return db.Providers;
        }

        public Provider Get(int? id)
        {
            return db.Providers.Find(id);
        }

        public void Create(Provider order)
        {
            db.Providers.Add(order);
        }

        public void Update(Provider order)
        {
            db.Entry(order).State = EntityState.Modified;
        }
        public IEnumerable<Provider> Find(Func<Provider, Boolean> predicate)
        {
            return db.Providers.Include(o => o.Products).Where(predicate).ToList();
        }
        public void Delete(int? id)
        {
            Provider order = db.Providers.Find(id);
            if (order != null)
                db.Providers.Remove(order);
        }
    }
}
