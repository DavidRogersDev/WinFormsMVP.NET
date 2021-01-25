using System.Collections.Generic;
using AutofacDemo.Services.DataTransferObjects;

namespace AutofacDemo.Services
{
    public interface IOrdersService
    {
        bool AddOrder(AddProductDto dto);
        IEnumerable<OrderDto> GetOrders();
        IEnumerable<OrderDto> GetOrdersById( int orderId);
    }
}