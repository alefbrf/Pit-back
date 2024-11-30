using CoffeeBreak.Application.DTOs.Response.Product;
using CoffeeBreak.Domain.Entities;

namespace CoffeeBreak.Application.Common.Interfaces.Repositories
{
    public interface IOrderRepository : IBaseRepository<Order>
    {
        Order Create(int userId);
        Order? GetByIdByUserId(int userId, int orderId);
        List<ProductResponse> GetItemsByOrder(int orderId);
        List<Order> GetAllByUser(int userId);
        List<CoffeeBreak.Application.DTOs.Response.Order.Order> GetDeliveries(int? userId);
        List<CoffeeBreak.Application.DTOs.Response.Order.Order> GetPending();
        CoffeeBreak.Application.DTOs.Response.Order.Order Approve(int orderId);
        CoffeeBreak.Application.DTOs.Response.Order.Order Disapprove(int orderId);
        CoffeeBreak.Application.DTOs.Response.Order.Order MakeReady(int orderId);
        CoffeeBreak.Application.DTOs.Response.Order.Order Sent(int orderId);
        CoffeeBreak.Application.DTOs.Response.Order.Order Deliver(int orderId);
    }
}
