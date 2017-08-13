using System.Web.Mvc;
using Microsoft.Practices.Unity;

using Unity.Mvc5;

using Domain.Model;
using Domain.Repositories;
using Persistence;

namespace SanaAssessment
{
    public static class UnityConfig
    {
        public static void RegisterComponents(string storageType = "mem")
        {
			var container = new UnityContainer();

            if (storageType == "xml")
                container.RegisterType<IProductRepository, Persistence.XML.ProductRepository>();
            else
                container.RegisterType<IProductRepository, Persistence.Memory.ProductRepository>(new ContainerControlledLifetimeManager());
            
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}