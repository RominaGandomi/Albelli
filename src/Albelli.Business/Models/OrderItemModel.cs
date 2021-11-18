using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Albelli.Business.Models
{
    public class OrderItemModel
    {
        public int Id { get; set; }
        public OrderModel Order { get; set; }
        public int OrderId { get; set; }
        public ProductTypeModel ProductType { get; set; }
        public int ProductTypeId { get; set; }
        public int Quantity { get; set; }
    }
}
