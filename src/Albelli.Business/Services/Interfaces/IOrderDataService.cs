using Albelli.Business.Models;
using Albelli.Business.Models.Dto;
using Albelli.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Albelli.Business.Services.Interfaces
{
    public interface IOrderDataService
    {
        public Task<GetOrderOutput> GetOrder(int orderId);
        public Task<OrderModel> SaveOrder(OrderModel model);
        public Task<List<ProductTypeModel>> GetProductTypes();
    }
}
