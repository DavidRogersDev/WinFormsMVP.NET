using System.Collections.Generic;
using LamarDemo.Services.DataTransferObjects;

namespace LamarDemo.Services
{
    public interface IOrdersService
    {
        bool AddOrder(AddProductDto dto);
        IEnumerable<OrderDto> GetOrders();
        IEnumerable<OrderDto> GetOrdersById( int orderId);
    }
}