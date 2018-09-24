using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using DAL.DataContext;
using DAL.Entities;
using DAL.Interfaces;

namespace DAL.Repositories
{
    public class CategoryRepository:IRepository<Category>
    {
        private ProductContext db;

        public CategoryRepository(ProductContext db)
        {
            this.db = db;
        }

        public IEnumerable<Category> GetAll()
        {
            return db.Categories.Include(o => o.Products);
        }

        public Category Get(int? id)
        {
            return db.Categories.Find(id);
        }

        public void Create(Category category)
        {
            db.Categories.Add(category);
        }

        public void Update(Category category)
        {
            db.Entry(category).State = EntityState.Modified;
        }
        public IEnumerable<Category> Find(Func<Category, Boolean> predicate)
        {
            return db.Categories.Include(o => o.Products).Where(predicate).ToList();
        }
        public void Delete(int? id)
        {
            Category category = db.Categories.Find(id);
            if (category != null)
                db.Categories.Remove(category);
        }
    }
}
