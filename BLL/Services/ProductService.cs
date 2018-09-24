using System;
using System.Linq;
using BLL.DTO;
using DAL.Entities;
using DAL.Interfaces;
using BLL.Infrastructure;
using System.Collections.Generic;
using AutoMapper;

namespace BLL.Services
{
    public class ProductService:IDisposable
    {
        IUnitOfWork DataBase { get; set; }

        public ProductService(IUnitOfWork uow)
        {
            DataBase = uow;
        }

        public IEnumerable<ProductDTO> GetProducts()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Product, ProductDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<Product>,List<ProductDTO>>(DataBase.Products.GetAll());
        }

        public IEnumerable<ProductDTO> GetProductsByCategory(int? id)
        {
            if (id == null) throw new ValidationException("There is no category id","");
            var category = DataBase.Categories.Get(id);
            if (category == null)
            {
                throw new ValidationException("There is no category!", "");
            }
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Product, ProductDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<Product>, List<ProductDTO>>
                (DataBase.Products.GetAll().Where(prod=>prod.CategoryId==id));
        }


        public IEnumerable<ProductDTO> GetProductsByProvider(int? id)
        {
            if (id == null) throw new ValidationException("There is no provider id", "");
            var provider = DataBase.Providers.Get(id);
            if (provider == null)
            {
                throw new ValidationException("There is no Provider!", "");
            }
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Product, ProductDTO>()).CreateMapper();

            return mapper.Map<IEnumerable<Product>, List<ProductDTO>>(DataBase.Providers.Get(id).Products);

        }

        public IEnumerable<ProductDTO> FindProductsByPrice(int price)
        {
            var products = DataBase.Products.Find(prod => prod.Price == price);
            if (products == null) return null;
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Product, ProductDTO>()).CreateMapper();

            return mapper.Map<IEnumerable<Product>, List<ProductDTO>>(products);
        }

        public IEnumerable<ProductDTO> FindProductsByPrice(int min, int max)
        {
            var products = DataBase.Products.Find(prod => prod.Price >= min && prod.Price <= max);
            if (products == null) return null;

            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Product, ProductDTO>()).CreateMapper();

            return mapper.Map<IEnumerable<Product>, List<ProductDTO>>(products);
        }


        public void Dispose()
        {
            DataBase.Dispose();
        }
    }
}
