﻿using AltYapi.Caching;
using AltYapi.Core.Repositories;
using AltYapi.Core.Repositories.RepositoriesMongo;
using AltYapi.Core.Services;
using AltYapi.Core.UnitOfWorks;
using AltYapi.Repository;
using AltYapi.Repository.Repositories;
using AltYapi.Repository.UnitOfWorks;
using AltYapi.RepositoryMongo;
using AltYapi.RepositoryMongo.Repositories;
using AltYapi.Service.Mapping;
using AltYapi.ServiceMongo.Service;
using AltYapi.Service.Services;
using AltYapi.ServiceMongo.Mapping;
using Autofac;
using System.Reflection;
using Module = Autofac.Module;
using AltYapi.RepositoryMongo.UnitOfWorks;
using UnitOfWork = AltYapi.Repository.UnitOfWorks.UnitOfWork;
using IUnitOfWork = AltYapi.Core.UnitOfWorks.IUnitOfWork;

namespace AltYapi.API.Modules
{
    public class RepoServiceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {

            builder.RegisterGeneric(typeof(GenericRepository<>)).As(typeof(IGenericRepository<>)).InstancePerLifetimeScope();
            builder.RegisterGeneric(typeof(GenericRepositoryMongo<>)).As(typeof(IGenericRepositoryMongo<>)).InstancePerLifetimeScope();

            builder.RegisterGeneric(typeof(Service<>)).As(typeof(IService<>)).InstancePerLifetimeScope();
            builder.RegisterGeneric(typeof(ServiceMongo<,>)).As(typeof(IServiceWithDto<,>)).InstancePerLifetimeScope();


           // builder.RegisterType<UnitOfWork>().As<IUnitOfWork>();
            builder.RegisterType<RepositoryMongo.UnitOfWorks.UnitOfWork>().As<RepositoryMongo.UnitOfWorks.IUnitOfWork>();

            builder.RegisterType<MongoContext>().As<IMongoContext>();


            var apiAssembly = Assembly.GetExecutingAssembly();
            //   Herhangi bir class ismi yeterli AppDbContext, MapProfile yerine başka bir class de yazabilirdik.
            var repoAssembly = Assembly.GetAssembly(typeof(AppDbContext));
            var repoAssemblyMongo = Assembly.GetAssembly(typeof(MongoContext));
            var serviceAssembly = Assembly.GetAssembly(typeof(MapProfile));
            var serviceAssemblyMongo = Assembly.GetAssembly(typeof(MapProfileMongo));


            // Repository ile bitenleri al
            builder.RegisterAssemblyTypes(apiAssembly, repoAssembly, serviceAssembly).Where(x => x.Name.EndsWith("Repository2")).AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterAssemblyTypes(apiAssembly, repoAssemblyMongo, serviceAssemblyMongo).Where(x => x.Name.EndsWith("RepositoryMongo")).AsImplementedInterfaces().InstancePerLifetimeScope();
            //Karşılık gelenler
            //InstancePerLifetimeScope => Scope
            //InstancePerDependency => transient

            //Service ile bitenleri al
            builder.RegisterAssemblyTypes(apiAssembly, repoAssembly, serviceAssembly).Where(x => x.Name.EndsWith("Service")).AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterAssemblyTypes(apiAssembly, repoAssemblyMongo, serviceAssemblyMongo).Where(x => x.Name.EndsWith("ServiceMongo")).AsImplementedInterfaces().InstancePerLifetimeScope();

            //  builder.RegisterType<ProductServiceWithCaching>().As<IProductService>();

            //builder.RegisterType<PersonServiceWithDto>().As<IPersonServicesWithDto>();
            builder.RegisterType<PersonServiceMongoWithDto>().As<IPersonServicesWithDto>();

        }
    }
}
