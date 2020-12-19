using System;
using System.Collections.Generic;
using AttributeBinding.Models;
using AttributeBinding.Services.DataTransferObjects;
using WinFormsMVP.NET;

namespace AttributeBinding.Views
{
    public interface IMainView : IView<MainModel>
    {
        event EventHandler ClearFilter;
        event EventHandler<int> OrderFiltered;
        void PopulateList(IEnumerable<OrderDto> orders);
        void PopulateOrdersFilter(IEnumerable<int> orderIds);
        void FilterList(int orderId);
    }
}
