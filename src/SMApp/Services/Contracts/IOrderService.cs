using SMApp.Models;

namespace SMApp.Services.Contracts;

public interface IOrderService
{
    Task CreateOrder(OrderModel orderModel);
}
