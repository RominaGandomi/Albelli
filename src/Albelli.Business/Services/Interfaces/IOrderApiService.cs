using Albelli.Business.Models.Dto;

namespace Albelli.Business.Services.Interfaces
{
    public interface IOrderApiService
    {
        public GetOrderOutput GetOrder(int orderId);
        public SubmitOrderOutput SaveOrder(SubmitOrderInput model);
    }
}
