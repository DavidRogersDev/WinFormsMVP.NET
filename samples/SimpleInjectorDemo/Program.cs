using System;
using System.Windows.Forms;
using SimpleInjector;
using SimpleInjector.Lifestyles;
using SimpleInjectorDemo.Forms;
using SimpleInjectorDemo.Presenters;
using SimpleInjectorDemo.Services;
using WinFormsMVP.NET.Binder;
using WinFormsMVP.NET.SimpleInjector;

namespace SimpleInjectorDemo
{
    static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var container = new Container();
            container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();

#if DEBUG
            container.Options.EnableAutoVerification = false;
#endif


            container.Register<IOrdersService, OrdersService>(Lifestyle.Transient);
            container.Register<MainPresenter>(Lifestyle.Transient);
            container.Register<AddProductPresenter>(Lifestyle.Transient);

            using (AsyncScopedLifestyle.BeginScope(container))
            {
                PresenterBinder.Factory = new SimpleInjectorPresenterFactory(container);

                Application.Run(new Main());
            }
        }
    }
}
