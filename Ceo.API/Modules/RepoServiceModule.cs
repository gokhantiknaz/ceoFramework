using Ceo.Core.Repositories;
using Ceo.Core.Services;
using Ceo.Repository;
using Ceo.Repository.Repositories;
using Ceo.Service.Mapping;
using Ceo.Service.Services;
using Autofac;
using System.Reflection;
using Module = Autofac.Module;
using UnitOfWork = Ceo.Repository.UnitOfWorks.UnitOfWork;
using IUnitOfWork = Ceo.Core.UnitOfWorks.IUnitOfWork;

namespace Ceo.API.Modules
{
    public class RepoServiceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {

            builder.RegisterGeneric(typeof(GenericRepository<>)).As(typeof(IGenericRepository<>)).InstancePerLifetimeScope();

            builder.RegisterGeneric(typeof(Service<>)).As(typeof(IService<>)).InstancePerLifetimeScope();
            builder.RegisterGeneric(typeof(ServiceWithDto<,>)).As(typeof(IServiceWithDto<,>)).InstancePerLifetimeScope();



            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>();

            var apiAssembly = Assembly.GetExecutingAssembly();
            //   Herhangi bir class ismi yeterli AppDbContext, MapProfile yerine başka bir class de yazabilirdik.
            var repoAssembly = Assembly.GetAssembly(typeof(AppDbContext));
            var serviceAssembly = Assembly.GetAssembly(typeof(MapProfile));


            // Repository ile bitenleri al
            builder.RegisterAssemblyTypes(apiAssembly, repoAssembly, serviceAssembly).Where(x => x.Name.EndsWith("Repository2")).AsImplementedInterfaces().InstancePerLifetimeScope();
            
            //Karşılık gelenler
            //InstancePerLifetimeScope => Scope
            //InstancePerDependency => transient

            //Service ile bitenleri al
            builder.RegisterAssemblyTypes(apiAssembly, repoAssembly, serviceAssembly).Where(x => x.Name.EndsWith("Service")).AsImplementedInterfaces().InstancePerLifetimeScope();

            //  builder.RegisterType<ProductServiceWithCaching>().As<IProductService>();
            //builder.RegisterType<PersonServiceWithDto>().As<IPersonServicesWithDto>();
        }
    }
}
