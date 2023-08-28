using Dotnet.Web.Dto;
using Dotnet.Web.Models;

namespace Dotnet.Web.Interfaces;

public interface IOrderService
{
    public Task<OrderDto> GetOrder();

    public Task<int> CreateOrder();

    public Task MoveOrderStatus(int orderId, OrderStatus orderStatus);
}