using Albelli.Business.Models;
using System.Collections.Generic;


namespace Albelli.Business.Services.Interfaces
{
    public interface IOrderDataService
    {
        public OrderModel GetOrder(int orderId);
        public OrderModel SaveOrder(OrderModel model);
        public List<ProductTypeModel> GetProductTypes();
        public bool SaveOrderItems(List<OrderItemModel> model);
    }
}
