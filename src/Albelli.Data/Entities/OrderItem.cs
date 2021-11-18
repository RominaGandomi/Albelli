using Albelli.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Albelli.Data.Entities
{
    [Table("OrderItem")]
    public class OrderItem : IBaseEntity
    {
        public int Id { get; set; }
        public virtual Order Order { get; set; }
        public int OrderId { get; set; }
        public virtual ProductType ProductType { get; set; }
        public int ProductTypeId { get; set; }
        public int Quantity { get; set; }
    }
}
