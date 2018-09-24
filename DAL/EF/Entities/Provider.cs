using System;
using System.Collections.Generic;


namespace DAL.Entities
{
    public class Provider
    {
        
        public int Id { get; set; }

        public string Name { get; set; }

        public string Adress { get; set; }

        public ICollection<Product> Products { get; set; }

        public Provider()
        {
            Products = new List<Product>();
        }
    }
}
