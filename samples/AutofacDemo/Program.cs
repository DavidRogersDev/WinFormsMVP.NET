using System;
using System.Windows.Forms;
using Autofac;
using AutofacDemo.Forms;
using AutofacDemo.Services;
using WinFormsMVP.NET.Autofac;
using WinFormsMVP.NET.Binder;

namespace AutofacDemo
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

            var builder = new ContainerBuilder();

            builder.RegisterType<OrdersService>().As<IOrdersService>().ExternallyOwned();
            builder.RegisterPresenters(typeof(Program).Assembly).ExternallyOwned();

            var container = builder.Build();

            PresenterBinder.Factory = new AutofacPresenterFactory(container);

            Application.Run(new Main());
        }
    }
}
