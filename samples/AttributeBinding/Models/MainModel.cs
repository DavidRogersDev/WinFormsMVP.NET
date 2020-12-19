using System.Collections.Generic;
using AttributeBinding.Services.DataTransferObjects;

namespace AttributeBinding.Models
{
    public class MainModel
    {
        public IEnumerable<OrderDto> Orders { get; set; }
    }
}
