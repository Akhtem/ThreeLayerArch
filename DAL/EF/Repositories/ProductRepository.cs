using System;
using System.Collections.Generic;
using System.Collections;
using System.Data.Entity;
using System.Linq;
using DAL.DataContext;
using DAL.Entities;
using DAL.Interfaces;

namespace DAL.Repositories
{
    public class ProductRepository:IRepository<Product>
    {
        ProductContext db;

        public ProductRepository(ProductContext context)
        {
            db = context;
        }

        public IEnumerable<Product> GetAll()
        {          
            return db.Products;
        }

        public Product Get(int? id)
        {
            return db.Products.Find(id);
        }

        public void Create(Product product)
        {
            db.Products.Add(product);
        }
        public void Update(Product product)
        {
            db.Entry(product).State = EntityState.Modified;
        }

        public IEnumerable<Product> Find(Func<Product, Boolean> predicate)
        {
            return db.Products.Where(predicate).ToList();
        }

        public void Delete(int? id)
        {
            Product prod = db.Products.Find(id);
            if (prod != null)
                db.Products.Remove(prod);
        }
    }
}
