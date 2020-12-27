using System;
using System.Collections.Generic;
using SimpleInjectorDemo.Services.DataTransferObjects;
using WinFormsMVP.NET;

namespace SimpleInjectorDemo.Views
{
    public interface IMainView : IView
    {
        event EventHandler<int> OrderFiltered;
        void PopulateList(IEnumerable<OrderDto> orders);
        void PopulateOrdersFilter(IEnumerable<int> orderIds);
        void FilterList(IEnumerable<OrderDto> orders);
    }
}

