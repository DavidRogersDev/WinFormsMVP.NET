using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text.Json;
using AutofacDemo.Services.DataTransferObjects;

namespace AutofacDemo.Services
{
    public class OrdersService : IOrdersService, IDisposable
    {
        private bool _disposed = false;

        public bool AddOrder(AddProductDto dto)
        {
            // This method is just a stub. Order won't actually be added to the json file
            // logic would go here to add an order -- hit database etc. 👍

            return true;
        }

        public IEnumerable<OrderDto> GetOrders()
        {
            ZipFile.ExtractToDirectory(
                Path.Combine("MockData", "orders.zip"), "MockData", true
            );

            var jsonString = File.ReadAllText(Path.Combine("MockData", "orders.json"));
            var orders = JsonSerializer.Deserialize<IEnumerable<OrderDto>>(jsonString);

            return orders;
        }

        public IEnumerable<OrderDto> GetOrdersById( int orderId)
        {
            ZipFile.ExtractToDirectory(
                Path.Combine("MockData", "orders.zip"), "MockData", true
            );

            var jsonString = File.ReadAllText(Path.Combine("MockData", "orders.json"));
            var orders = JsonSerializer.Deserialize<IEnumerable<OrderDto>>(jsonString);

            return orders.Where(o => o.OrderId == orderId).Distinct();
        }

        public void Dispose()
        {
            if (!_disposed)
            {
                // dispose of stuff here. Perhaps an EF context.
                _disposed = true;
            }
        }
    }
}
