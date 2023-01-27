﻿using AltYapi.Core.Repositories;
using AltYapi.Core.Services;
using AltYapi.Core.UnitOfWorks;
using AltYapi.Repository;
using AltYapi.Repository.Repositories;
using AltYapi.Repository.UnitOfWorks;
using AltYapi.Service.Mapping;
using AltYapi.Service.Mongo.Service;
using AltYapi.Service.Services;
using Autofac;
using System.Reflection;
using Module = Autofac.Module;

namespace AltYapi.API.Modules
{
    public class RepoServiceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {

            builder.RegisterGeneric(typeof(GenericRepository<>)).As(typeof(IGenericRepository<>)).InstancePerLifetimeScope();

            builder.RegisterGeneric(typeof(Service<>)).As(typeof(IService<>)).InstancePerLifetimeScope();
            builder.RegisterGeneric(typeof(ServiceWithDto<,>)).As(typeof(IServiceWithDto<,>)).InstancePerLifetimeScope();

            //builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerLifetimeScope();
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerLifetimeScope();
            builder.RegisterType<ProductServiceNoCaching>().As<IProductService>();


            //builder.RegisterType<ProductServiceWithDto>().As<IProductServicesWithDto>().InstancePerLifetimeScope();

            var apiAssembly = Assembly.GetExecutingAssembly();
            //Herhangi bir class ismi yeterli AppDbContext,MapProfile yerine başka bir class de yazabilirdik.
            var repoAssembly = Assembly.GetAssembly(typeof(AppDbContext));
            var serviceAssembly = Assembly.GetAssembly(typeof(MapProfile));


            //Repository ile bitenleri al
            builder.RegisterAssemblyTypes(apiAssembly, repoAssembly, serviceAssembly).Where(x => x.Name.EndsWith("Repository")).AsImplementedInterfaces().InstancePerLifetimeScope();

            //Karşılık gelenler
            //InstancePerLifetimeScope => Scope
            //InstancePerDependency => transient

            //Service ile bitenleri al
            builder.RegisterAssemblyTypes(apiAssembly, repoAssembly, serviceAssembly).Where(x => x.Name.EndsWith("Service")).AsImplementedInterfaces().InstancePerLifetimeScope();

            //builder.RegisterGeneric(typeof(ServiceMongo<,>)).As(typeof(IService<>));

            //builder.RegisterGeneric(typeof(ServiceWithDtoMongo<,>)).As(typeof(IServiceWithDto<,>));

            //builder.RegisterGeneric(typeof(PersonServiceWithDtoMongo)).As(typeof(IPersonServicesWithDto<>));


            builder.RegisterType<PersonServiceMongo>().As<IPersonServicesWithDto>();



        }
    }
}
