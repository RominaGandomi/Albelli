using Albelli.Data.Entities;
using Albelli.Data.Interfaces.Services;
using Albelli.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Albelli.Data.Services
{
    public class OrderService : Repository<Order>, IOrderService
    {
        public OrderService(AlbelliDbContext context) : base(context)
        {
        }
    }
}
