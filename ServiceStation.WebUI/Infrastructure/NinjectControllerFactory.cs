using System;
using System.Web.Mvc;
using System.Web.Routing;
using Ninject;

using ServiceStation.Domain.Abstract;
using ServiceStation.Domain.Implementation;
using ServiceStation.WebUI.Infrastructure.Abstract;
using ServiceStation.WebUI.Infrastructure.Concrete;

namespace ServiceStation.WebUI.Infrastructure
{
    public class NinjectControllerFactory : DefaultControllerFactory 
    {
        private IKernel ninjectKernel;

        public NinjectControllerFactory() 
        {
            ninjectKernel = new StandardKernel();
            AddBindings();
        }

        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType) 
        {
            return controllerType == null
            ? null
            : (IController)ninjectKernel.Get(controllerType);
        }

        private void AddBindings() 
        {
            ninjectKernel.Bind<IDBAccessor>().To<MySqlDBAccessor>().InSingletonScope();

            ninjectKernel.Bind<EFDbContext>().ToSelf().InSingletonScope();
            ninjectKernel.Bind<ICarRepository>().To<EFCarRepository>();
            ninjectKernel.Bind<IClientRepository>().To<EFClientRepository>();
            ninjectKernel.Bind<IOrderRepository>().To<EFOrderRepository>();
            ninjectKernel.Bind<IAuthProvider>().To<DBAuthProvider>();
        }
    }
}