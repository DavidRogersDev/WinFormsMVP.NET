using System;
using System.Collections.Generic;
using Basic.Services.DataTransferObjects;
using WinFormsMVP.NET;

namespace Basic.Views
{
    public interface IMainView : IView
    {

        event EventHandler<int> OrderFiltered;
        void PopulateList(IEnumerable<OrderDto> orders);
        void PopulateOrdersFilter(IEnumerable<int> orderIds);
        void FilterList(int orderId);
    }
}
