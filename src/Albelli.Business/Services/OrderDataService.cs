using Albelli.Business.Models;
using Albelli.Business.Services.Interfaces;
using Albelli.Data.Entities;
using Albelli.Data.Interfaces.Services;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Albelli.Business.Services
{
    public class OrderDataService : IOrderDataService
    {
        private readonly IMapper _mapper;
        private readonly IOrderService _orderService;
        private readonly IProductTypeService _productTypeService;
        private readonly IOrderItemService _orderItemService;


        public OrderDataService(IMapper mapper, IOrderService orderService, IOrderItemService orderItemService, IProductTypeService productTypeService)
        {
            _mapper = mapper;
            _orderService = orderService;
            _orderItemService = orderItemService;
            _productTypeService = productTypeService;
        }
        public OrderModel GetOrder(int orderId)
        {
            var order = _orderService.Find(x => x.Id == orderId);
            if (order == null)
                return null;
            var mappedModel = _mapper.Map<OrderModel>(order);
            var details = _orderItemService.GetAllIncluding(x => x.ProductType).Where(x => x.OrderId == order.Id);
            mappedModel.Items = _mapper.Map<List<OrderItemModel>>(details);

            return mappedModel;

        }
        public List<ProductTypeModel> GetProductTypes()
        {
            var products = _productTypeService.GetAll().ToList();
            var mappedModel = _mapper.Map<List<ProductTypeModel>>(products);
            return mappedModel;
        }
        public OrderModel SaveOrder(OrderModel model)
        {
            var mappedOrder = _mapper.Map<Order>(model);
            var order = _orderService.Add(mappedOrder);
            _orderService.Save();
            model.Id = order.Id;
            return model;
        }
        public bool SaveOrderItems(List<OrderItemModel> model)
        {
            var mappedOrder = _mapper.Map<List<OrderItem>>(model);

            for (int i = 0; i < mappedOrder.Count; i++)
                _orderItemService.Add(mappedOrder[i]);

            _orderItemService.Save();
            return true;
        }
    }
}
