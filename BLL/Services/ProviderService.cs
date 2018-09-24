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
    public class ProviderService:IDisposable
    {
        IUnitOfWork DataBase { get; set; }

        public ProviderService(IUnitOfWork uow)
        {
            DataBase = uow;
        }

        public IEnumerable<ProviderDTO> GetProviders()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Provider, ProviderDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<Provider>, List<ProviderDTO>>(DataBase.Providers.GetAll());
        }


        public IEnumerable<ProviderDTO> GetProvidersByCategory(int? id)
        {
            if (id == null) throw new ValidationException("There is no category id", "");
            var category = DataBase.Categories.Get(id);
            if (category == null)
            {
                throw new ValidationException("There is no category!", "");
            }
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Provider, ProviderDTO>()).CreateMapper();
            var result = DataBase.Products.GetAll().Where(prod => prod.CategoryId == id).Select(prod => prod.Providers);
            ICollection<Provider> providers = new List<Provider>();
            foreach (var i in result)
            {
                foreach (var j in i)
                {
                    providers.Add(j);
                }
            }
            return mapper.Map<IEnumerable<Provider>, List<ProviderDTO>>
                (providers.Distinct());
        }


        public IEnumerable<ProviderDTO> FindProdiversByCity(string city)
        {
            var providers = DataBase.Providers.Find(prov => prov.Adress == city);
            if (providers == null) return null;

            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Provider, ProviderDTO>()).CreateMapper();

            return mapper.Map<IEnumerable<Provider>, List<ProviderDTO>>(providers);
        }

        public void Dispose()
        {
            DataBase.Dispose();
        }
    }
}
