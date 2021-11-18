using Albelli.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Albelli.Data.Entities
{
    [Table("Order")]
    public class Order:IBaseEntity
    {
        public Order()
        {
            Items = new List<OrderItem>();
        }
        public int Id { get; set; }
        public double MinimumBinWidth { get; set; }
        public virtual ICollection<OrderItem> Items { get; set; }
    }
}
