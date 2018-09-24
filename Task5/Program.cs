using System;
using System.Collections.Generic;
using Ninject;
using Ninject.Modules;
using BLL.Infrastructure;
using BLL.Services;
using DAL.Interfaces;
using System.Web.Mvc;
using Ninject.Web.Mvc;

namespace Task5
{
    class Program
    {
        static void Main(string[] args)
        {
            NinjectModule serviceModule = new ServiceModule("DefaultConnection");
            var kernel = new StandardKernel(serviceModule);
            DependencyResolver.SetResolver(new NinjectDependencyResolver(kernel));
            ProductService service = new ProductService(kernel.Get<IUnitOfWork>());
            ProviderService serviceProvider = new ProviderService(kernel.Get<IUnitOfWork>());
            Console.WriteLine("All Products");
             GetProducts(service);
            //Console.WriteLine("All Providers");
            //GetProviders(serviceProvider);
            //Console.WriteLine("Products By Category");
            //GetProductsByCategory(service, 1);
            Console.ReadKey();
        }


        static void GetProducts(ProductService serv)
        {
            ProductService service = serv;
            foreach (var i in service.GetProducts())
            {
                Console.WriteLine("ID - {0}  ; Name - {1}  ; Price -  {2} ***{3}.",i.Id, i.Name, i.Price,i.Providers.Count);
                
                
            }
        }
        

        static void GetProductsByCategory(ProductService serv,int? id)
        {
            ProductService service = serv;
            foreach (var i in service.GetProductsByCategory(id))
            {
                Console.WriteLine("ID - {0}  ; Name - {1}  ; Price -  {2} .", i.Id, i.Name, i.Price);
            }
        }

        static void GetProductsByProvider(ProductService serv, int? id)
        {
            ProductService service = serv;
            foreach (var i in service.GetProductsByProvider(id))
            {
                Console.WriteLine("ID - {0}  ; Name - {1}  ; Price -  {2} .", i.Id, i.Name, i.Price);
            }
        }

        static void GetProviders(ProviderService serv)
        {
            ProviderService service = serv;
            foreach (var i in service.GetProviders())
            {
                Console.WriteLine("ID - {0}  ; Name - {1}  ; Adress -  {2} .", i.Id, i.Name, i.Adress);

            }
        }

    }
}
