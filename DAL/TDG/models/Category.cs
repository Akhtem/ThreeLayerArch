using System;
using System.Collections.Generic;


namespace DAL.TDG.models
{
    public class Category
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<Product> Products { get; set; }


        public Category(int id, string name, ICollection<Product> products)
        {
            Id = id;
            Name = name;
            Products = products;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
