using System;
using System.Collections.Generic;
using AutofacDemo.Services.DataTransferObjects;
using WinFormsMVP.NET;

namespace AutofacDemo.Views
{
    public interface IMainView : IView
    {
        event EventHandler CleanUp;
        event EventHandler ClearFilter;
        event EventHandler<int> OrderFiltered;
        void ClearSelectedFilter();
        void FilterList(IEnumerable<OrderDto> orders);
        void PopulateList(IEnumerable<OrderDto> orders);
        void PopulateOrdersFilter(IEnumerable<int> orderIds);

    }
}
