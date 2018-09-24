using System;
using System.Collections.Generic;

namespace DAL.TDG.models
{
    public class Product
    {

        public int Id { get; set; }

        public string Name { get; set; }

        public double Price { get; set; }

        public int? CategoryId { get; set; }

        public ICollection<Provider> Providers { get; set; }

    }
}
