using System;
using System.Collections.Generic;


namespace BLL.DTO
{
    public class ProviderDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Adress { get; set; }

        public ICollection<ProductDTO> Products { get; set; }
    }
}
