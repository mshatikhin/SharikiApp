using SharikiApp.Models;
using SharikiApp.Models.Cache;
using SharikiApp.Models.Mailer;
using SharikiApp.Models.Users;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(SharikiApp.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(SharikiApp.App_Start.NinjectWebCommon), "Stop")]

namespace SharikiApp.App_Start
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;

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
            kernel.Bind<IUserRepository>().To<UserRepository>();
            kernel.Bind<IBalloonRepository>().To<BalloonRepository>();
            kernel.Bind<IBasketRepository>().To<BasketRepository>();
            kernel.Bind<INewsRepository>().To<NewsRepository>();

            kernel.Bind<IBalloonService>().To<BalloonService>();
            kernel.Bind<IMailer>().To<Mailer>();

            kernel.Bind<IBalloonProvider>().To<BalloonProvider>().InSingletonScope();
            kernel.Bind<IDataCache>().To<DataCache>().InSingletonScope();
        }
    }
}