using System;
using System.Collections.Generic;

namespace DAL.Entities
{
    public class Product
    {

        public int Id { get; set; }

        public string Name { get; set; }

        public double Price { get; set; }

        public int? CategoryId { get; set; }
        public Category Category { get; set; }




        public ICollection<Provider> Providers { get; set; }

        public Product()
        {
            Providers = new List<Provider>();
        }

 
    }
}
