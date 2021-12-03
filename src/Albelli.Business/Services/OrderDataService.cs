using Albelli.Business.Models;
using Albelli.Business.Models.Dto;
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
        public async Task<GetOrderOutput> GetOrder(int orderId)
        {
            try
            {
                var order = await _orderService.FindAsync(x => x.Id == orderId);
                var items = _orderItemService.GetAllIncluding(x => x.ProductType).Where(x=>x.OrderId==order.Id);
                
                foreach (var item in items)
                    order.Items.Add(item);

                var model = _mapper.Map<GetOrderOutput>(order);
                return model;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<List<ProductTypeModel>> GetProductTypes()
        {
            try
            {
                var products = await _productTypeService.GetAllAsync();

                var mappedModel = new List<ProductTypeModel>();

                foreach (var item in products)
                {
                    switch (Enum.Parse(typeof(ProductCategory), item.Name))
                    {
                        case ProductCategory.Mug:
                            mappedModel.Add(new Mug() { Id = item.Id, Name = item.Name, Width = item.Width });
                            break;
                        case ProductCategory.Cards:
                            mappedModel.Add(new Cards() { Id = item.Id, Name = item.Name, Width = item.Width });
                            break;
                        case ProductCategory.Canvas:
                            mappedModel.Add(new Canvas() { Id = item.Id, Name = item.Name, Width = item.Width });
                            break;
                        case ProductCategory.Calendar:
                            mappedModel.Add(new Calendar() { Id = item.Id, Name = item.Name, Width = item.Width });
                            break;
                        case ProductCategory.Photobook:
                            mappedModel.Add(new PhotoBook() { Id = item.Id, Name = item.Name, Width = item.Width });
                            break;
                        default:
                            break;
                    }
                }

                return mappedModel;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public async Task<OrderModel> SaveOrder(OrderModel model)
        {
            try
            {
                var mappedOrder = _mapper.Map<Order>(model);
                var order = await _orderService.AddAsync(mappedOrder);
                await _orderService.SaveAsync();

                model.Id = order.Id;
                return model;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
