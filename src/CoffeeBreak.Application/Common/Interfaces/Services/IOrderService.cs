using CoffeeBreak.Application.DTOs.Request.Order;

namespace CoffeeBreak.Application.Common.Interfaces.Services
{
    public interface IOrderService
    {
        CoffeeBreak.Application.DTOs.Response.Order.Order CreateOrder(OrderDTO orderDTO, int userId);
        CoffeeBreak.Application.DTOs.Response.Order.Order GetById(int orderId);
        List<CoffeeBreak.Application.DTOs.Response.Order.Order> GetAll();
        List<CoffeeBreak.Application.DTOs.Response.Order.Order> GetAll(int userId);
        List<CoffeeBreak.Application.DTOs.Response.Order.Order> GetDeliveries(int? userId);
        List<CoffeeBreak.Application.DTOs.Response.Order.Order> GetPending();
        CoffeeBreak.Application.DTOs.Response.Order.Order Approve(int orderId);
        CoffeeBreak.Application.DTOs.Response.Order.Order Disapprove(int orderId);
        CoffeeBreak.Application.DTOs.Response.Order.Order MakeReady(int orderId);
        CoffeeBreak.Application.DTOs.Response.Order.Order Sent(int orderId);
        CoffeeBreak.Application.DTOs.Response.Order.Order Deliver(int orderId);
        CoffeeBreak.Application.DTOs.Response.Order.Order UpdateDeliveryMan(int orderId, int? deliveryManId);
    }
}
