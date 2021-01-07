using System;
using System.Windows.Forms;
using Lamar;
using LamarDemo.Forms;
using LamarDemo.Presenters;
using LamarDemo.Services;
using LamarDemo.Views;
using Microsoft.Extensions.DependencyInjection;
using WinFormsMVP.NET.Binder;
using WinFormsMVP.NET.Lamar;

namespace LamarDemo
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

            IContainer container = new Container(c =>
            {
                c.AddTransient<IOrdersService, OrdersService>();
                c.Injectable<IMainView>();
                //c.For<MainPresenter>().Use<MainPresenter>().Named(typeof(MainPresenter).Name).Scoped();
                c.AddTransient<MainPresenter>();

            });
            PresenterBinder.Factory = new LamarPresenterFactory(container);

            Application.Run(new Main());

            container.Dispose();
        }
    }
}
