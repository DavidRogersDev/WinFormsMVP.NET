using System;

namespace AttributeBinding.Services.DataTransferObjects
{
    public class OrderDto
    {
        public int OrderId { get; set; }
        public decimal Discount { get; set; }
        public decimal Freight { get; set; }
        public DateTimeOffset OrderDate { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
