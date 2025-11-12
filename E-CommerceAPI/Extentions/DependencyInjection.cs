using Autofac;
using E_Commerce.Application;
using E_Commerce.Infrastructure;

namespace E_CommerceAPI.Extentions
{
    public class DependencyInjection : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterGeneric(typeof(Repository<>)).As(typeof(IRepository<>)).InstancePerLifetimeScope();
            //builder.RegisterType<UnitOfWork.UnitOfWork>().As<IUnitOfWork>().InstancePerLifetimeScope();
            //builder.RegisterType<StoreContext>().InstancePerLifetimeScope();

            //builder.RegisterAssemblyTypes(typeof(ILogHistoryService).Assembly).AsImplementedInterfaces().InstancePerLifetimeScope();

            //// MediatR
            //builder.RegisterAssemblyTypes(typeof(IMediator).GetTypeInfo().Assembly)
            //       .AsImplementedInterfaces();

            //builder.Register<ServiceFactory>(ctx =>
            //{
            //    var c = ctx.Resolve<IComponentContext>();
            //    return t => c.Resolve(t);
            //});

            //builder.RegisterAssemblyTypes(typeof(LogEmployeeActionHandler).Assembly)
            //       .AsClosedTypesOf(typeof(IRequestHandler<,>))
            //       .AsImplementedInterfaces();

            //builder.RegisterAssemblyTypes(typeof(LogEmployeeActionHandler).Assembly)
            //       .AsClosedTypesOf(typeof(INotificationHandler<>))
            //       .AsImplementedInterfaces();

        }

    }
}
