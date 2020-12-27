using System.Collections.Generic;
using SimpleInjectorDemo.Services.DataTransferObjects;

namespace SimpleInjectorDemo.Services
{
    public interface IOrdersService
    {
        bool AddOrder(AddProductDto dto);
        IEnumerable<OrderDto> GetOrders();
        IEnumerable<OrderDto> GetOrdersById( int orderId);
    }
}