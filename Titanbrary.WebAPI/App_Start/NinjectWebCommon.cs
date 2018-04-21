[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(Titanbrary.WebAPI.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(Titanbrary.WebAPI.App_Start.NinjectWebCommon), "Stop")]

namespace Titanbrary.WebAPI.App_Start
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;
    using Titanbrary.Common.Interfaces.BusinessObjects;
    using Titanbrary.Common.Interfaces.Data;
    using Titanbrary.BusinessObjects;
    using Titanbrary.Data.DACs;
    using System.Web.Http;
    using Ninject.Web.WebApi;

    public static class NinjectWebCommon 
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }
        
        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }
        
        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                RegisterServices(kernel);
                GlobalConfiguration.Configuration.DependencyResolver = new NinjectDependencyResolver(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            //Biz
            kernel.Bind<IBook>().To<BusinessObjects.BookManager>().InRequestScope();
            kernel.Bind<IAccount>().To<BusinessObjects.AccountManager>().InRequestScope();
            kernel.Bind<ICart>().To<BusinessObjects.CartManager>().InRequestScope();

            //Dac
            kernel.Bind<IBookDAC>().To<Data.DACs.BookDAC>().InRequestScope();
            kernel.Bind<IAccountDAC>().To<Data.DACs.AccountDAC>().InRequestScope();
            kernel.Bind<ICartDAC>().To<Data.DACs.CartDAC>().InRequestScope();

        }        
    }
}
