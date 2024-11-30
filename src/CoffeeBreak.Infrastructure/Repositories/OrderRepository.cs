using CoffeeBreak.Application.Common.Exceptions;
using CoffeeBreak.Application.Common.Interfaces.Repositories;
using CoffeeBreak.Application.DTOs.Response.Product;
using CoffeeBreak.Domain.Entities;
using CoffeeBreak.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CoffeeBreak.Infrastructure.Repositories
{
    public class OrderRepository : BaseRepository<Order>, IOrderRepository
    {
        private AppDbContext _context;
        public OrderRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public Order Create(int userId)
        {
            Random Random = new Random();
            int RandomNumber = Random.Next(1000000);
            string sixDigitNumber = RandomNumber.ToString("D6");

            return new Order()
            {
                CreatedDate = DateTime.Now,
                Code = sixDigitNumber,
                UserId = userId,
                Address = string.Empty
            };
        }

        public Order? GetByIdByUserId(int userId, int orderId)
        {
            return _context.Orders.Where(order => order.Id == orderId && order.UserId == userId).AsNoTracking().FirstOrDefault();
        }

        public List<ProductResponse> GetItemsByOrder(int orderId)
        {
            return (
                from orderProduct in _context.OrderProducts
                join product in _context.Products on orderProduct.ProductId equals product.Id
                where
                    orderProduct.OrderId == orderId
                select new ProductResponse()
                {
                    Id = product.Id,
                    Description = product.Description,
                    ImageUrl = product.ImageUrl,
                    Name = product.Name,
                    Price = product.Price,
                    Quantity = orderProduct.ProductCount,
                }

            ).ToList();
        }

        public List<Order> GetAllByUser(int userId)
        {
            return _context.Orders.Where(p => p.UserId == userId).AsNoTracking().ToList();
        }

        public List<CoffeeBreak.Application.DTOs.Response.Order.Order> GetDeliveries(int? userId)
        {
            return (
                from order in _context.Orders
                where
                    order.IsDelivery &&
                    (userId.HasValue || order.DeliveryManId == userId) &&
                    (
                        order.InDelivery.HasValue ||
                        order.Ready.HasValue
                    )
                select new CoffeeBreak.Application.DTOs.Response.Order.Order
                {
                    Id = order.Id,
                    DeliveryManId = order.DeliveryManId,
                    Ready = order.Ready,
                    Delivered = order.Delivered,
                    InDelivery = order.InDelivery,
                    Address = order.Address,
                    Code = order.Code,
                    CreatedDate = order.CreatedDate,
                    Observation = order.Observation,
                    Price = order.Price,
                    Disapproved = order.Disapproved,
                    IsDelivery = order.IsDelivery,
                    OrderProducts = order.OrderProducts,
                    Preparing = order.Preparing
                }
            ).ToList();
        }

        public List<CoffeeBreak.Application.DTOs.Response.Order.Order> GetPending()
        {
            return (
                from order in _context.Orders
                where
                    !order.Preparing.HasValue &&
                    !order.Disapproved.HasValue
                select new CoffeeBreak.Application.DTOs.Response.Order.Order
                {
                    Id = order.Id,
                    DeliveryManId = order.DeliveryManId,
                    Ready = order.Ready,
                    Delivered = order.Delivered,
                    InDelivery = order.InDelivery,
                    IsDelivery = order.IsDelivery,
                    Address = order.Address,
                    Code = order.Code,
                    CreatedDate = order.CreatedDate,
                    Observation = order.Observation,
                    Price = order.Price
                }
            ).ToList();
        }

        public CoffeeBreak.Application.DTOs.Response.Order.Order Approve(int orderId)
        {
            var order = GetById(orderId);

            if (order == null)
            {
                throw new BaseException("Pedido não encontrado");
            }

            order.Preparing = DateTime.Now;
            _context.Update(order);
            _context.SaveChanges();
            return new Application.DTOs.Response.Order.Order
            {
                Id = order.Id,
                UserId = order.UserId,
                DeliveryManId = order.DeliveryManId,
                Ready = order.Ready,
                Delivered = order.Delivered,
                InDelivery = order.InDelivery,
                Address = order.Address,
                Code = order.Code,
                CreatedDate = order.CreatedDate,
                Disapproved = order.Disapproved,
                IsDelivery = order.IsDelivery,
                Observation = order.Observation,
                OrderProducts = order.OrderProducts,
                Preparing = order.Preparing,
                Price = order.Price
            };
        }

        public CoffeeBreak.Application.DTOs.Response.Order.Order Disapprove(int orderId)
        {
            var order = GetById(orderId);

            if (order == null)
            {
                throw new BaseException("Pedido não encontrado");
            }

            order.Disapproved = DateTime.Now;
            _context.Update(order);
            _context.SaveChanges();
            return new Application.DTOs.Response.Order.Order
            {
                Id = order.Id,
                UserId = order.UserId,
                DeliveryManId = order.DeliveryManId,
                Ready = order.Ready,
                Delivered = order.Delivered,
                InDelivery = order.InDelivery,
                Address = order.Address,
                Code = order.Code,
                CreatedDate = order.CreatedDate,
                Disapproved = order.Disapproved,
                IsDelivery = order.IsDelivery,
                Observation = order.Observation,
                OrderProducts = order.OrderProducts,
                Preparing = order.Preparing,
                Price = order.Price
            };
        }

        public CoffeeBreak.Application.DTOs.Response.Order.Order MakeReady(int orderId)
        {
            var order = GetById(orderId);

            if (order == null)
            {
                throw new BaseException("Pedido não encontrado");
            }

            order.Ready = DateTime.Now;
            _context.Update(order);
            _context.SaveChanges();
            return new Application.DTOs.Response.Order.Order
            {
                Id = order.Id,
                UserId = order.UserId,
                DeliveryManId = order.DeliveryManId,
                Ready = order.Ready,
                Delivered = order.Delivered,
                InDelivery = order.InDelivery,
                Address = order.Address,
                Code = order.Code,
                CreatedDate = order.CreatedDate,
                Disapproved = order.Disapproved,
                IsDelivery = order.IsDelivery,
                Observation = order.Observation,
                OrderProducts = order.OrderProducts,
                Preparing = order.Preparing,
                Price = order.Price
            };
        }
        public CoffeeBreak.Application.DTOs.Response.Order.Order Sent(int orderId)
        {
            var order = GetById(orderId);

            if (order == null)
            {
                throw new BaseException("Pedido não encontrado");
            }

            order.InDelivery = DateTime.Now;
            _context.Update(order);
            _context.SaveChanges();
            return new Application.DTOs.Response.Order.Order
            {
                Id = order.Id,
                UserId = order.UserId,
                DeliveryManId = order.DeliveryManId,
                Ready = order.Ready,
                Delivered = order.Delivered,
                InDelivery = order.InDelivery,
                Address = order.Address,
                Code = order.Code,
                CreatedDate = order.CreatedDate,
                Disapproved = order.Disapproved,
                IsDelivery = order.IsDelivery,
                Observation = order.Observation,
                OrderProducts = order.OrderProducts,
                Preparing = order.Preparing,
                Price = order.Price
            };
        }
        public CoffeeBreak.Application.DTOs.Response.Order.Order Deliver(int orderId)
        {
            var order = GetById(orderId);

            if (order == null)
            {
                throw new BaseException("Pedido não encontrado");
            }

            order.Delivered = DateTime.Now;
            _context.Update(order);
            _context.SaveChanges();
            return new Application.DTOs.Response.Order.Order
            {
                Id = order.Id,
                UserId = order.UserId,
                DeliveryManId = order.DeliveryManId,
                Ready = order.Ready,
                Delivered = order.Delivered,
                InDelivery = order.InDelivery,
                Address = order.Address,
                Code = order.Code,
                CreatedDate = order.CreatedDate,
                Disapproved = order.Disapproved,
                IsDelivery = order.IsDelivery,
                Observation = order.Observation,
                OrderProducts = order.OrderProducts,
                Preparing = order.Preparing,
                Price = order.Price
            };
        }
    }
}
