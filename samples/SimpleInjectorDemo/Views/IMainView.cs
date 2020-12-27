using System;
using System.Collections.Generic;
using SimpleInjectorDemo.Services.DataTransferObjects;
using WinFormsMVP.NET;

namespace SimpleInjectorDemo.Views
{
    public interface IMainView : IView
    {
        event EventHandler ClearFilter;
        event EventHandler<int> OrderFiltered;
        void ClearSelectedFilter();
        void FilterList(IEnumerable<OrderDto> orders);
        void PopulateList(IEnumerable<OrderDto> orders);
        void PopulateOrdersFilter(IEnumerable<int> orderIds);
    }
}

