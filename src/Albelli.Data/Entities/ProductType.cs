using Albelli.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Albelli.Data.Entities
{
    [Table("ProductType")]
    public class ProductType: IBaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Width { get; set; }
    }
}
