[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(TestAttrIoC.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(TestAttrIoC.App_Start.NinjectWebCommon), "Stop")]

namespace TestAttrIoC.App_Start
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;
    using Ninject.Web.WebApi.FilterBindingSyntax;
    using TestAttrIoC.Controllers.Filters;
using Ninject.Modules;

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
            var kernel = new StandardKernel(new MyModule());
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

        }   
     
    }

    public class MyModule : NinjectModule
    {
        public override void Load()
        {
            Bind<DbContext>().ToSelf().InRequestScope();
            Bind<IFirmRepository>().To<FirmRepository>().InRequestScope();
            //Bind<AuthorizeFirmFilterAttribute>().ToSelf().InRequestScope();

            this.BindHttpFilter<AuthorizeFirmFilterAttribute>(System.Web.Http.Filters.FilterScope.Controller)
                .WhenControllerHas<AuthorizeFirmAttribute>()
                .InRequestScope()
                .WithConstructorArgument("repository", (context) =>
                {
                    return context.Kernel.Get<IFirmRepository>();
                });
        }
    }
}
