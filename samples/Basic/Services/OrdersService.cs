using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Text.Json;
using Basic.Services.DataTransferObjects;

namespace Basic.Services
{
    public class OrdersService
    {
        public IEnumerable<OrderDto> GetOrders()
        {
            ZipFile.ExtractToDirectory(
                Path.Combine("MockData", "orders.zip"), "MockData", true
            );

            var jsonString = File.ReadAllText(Path.Combine("MockData", "orders.json"));
            var orders = JsonSerializer.Deserialize<IEnumerable<OrderDto>>(jsonString);

            return orders;
        }
    }
}
