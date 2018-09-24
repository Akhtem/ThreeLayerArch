using System;
using System.Collections.Generic;


namespace DAL.TDG.models
{
    public class Provider
    {

        public int Id { get; set; }

        public string Name { get; set; }

        public string Adress { get; set; }

        public ICollection<Product> Products { get; set; }

    }
}
