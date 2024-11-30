using CoffeeBreak.Application.Common.Exceptions;
using CoffeeBreak.Application.Common.Interfaces.Email;
using CoffeeBreak.Application.Common.Interfaces.Repositories;
using CoffeeBreak.Application.Common.Interfaces.Services;
using CoffeeBreak.Application.DTOs.Request.Order;
using CoffeeBreak.Domain.Entities;

namespace CoffeeBreak.Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IAddressRepository _addressRepository;
        private readonly IProductRepository _productRepository;
        private readonly IProductCartRepository _productCartRepository;
        private readonly IUserRepository _userRepository;
        private readonly IEmailService _emailService;
        private readonly IConfigsRepository _configsRepository;
        public OrderService(IOrderRepository orderRepository, IAddressRepository addressRepository, IProductRepository productRepository, IProductCartRepository productCartRepository, IEmailService emailService, IUserRepository userRepository, IConfigsRepository configsRepository)
        {
            _orderRepository = orderRepository;
            _addressRepository = addressRepository;
            _productRepository = productRepository;
            _productCartRepository = productCartRepository;
            _emailService = emailService;
            _userRepository = userRepository;
            _configsRepository = configsRepository;
        }

        public CoffeeBreak.Application.DTOs.Response.Order.Order CreateOrder(OrderDTO orderDTO, int userId)
        {
            if (orderDTO.IsDelivery && orderDTO.AddressId is null)
            {
                throw new BaseException("Pedidos para entrega devem ter endereço", System.Net.HttpStatusCode.BadRequest);
            }

            var order = _orderRepository.Create(userId);
            if (orderDTO.IsDelivery && orderDTO.AddressId.HasValue)
            {
                var address = _addressRepository.GetByIdByUser(userId, orderDTO.AddressId.Value);
                if (address != null)
                {
                    order.Address = $"{address.Street}, {address.Number}";

                    if (string.IsNullOrWhiteSpace(address.Complement)) {
                        order.Address = $"{order.Address}, {address.Complement}";
                    }
                    order.Address = $"{order.Address} - {address.Neighborhood}, {address.PostalCode}";
                }
            }

            var products = _productRepository.GetProductsById(orderDTO.products.Select(p => p.Id).ToList());
            _productCartRepository.RemoveProductsByUser(orderDTO.products.Select(p => p.Id).ToList(), userId);

            foreach (var item in products)
            {
                var itemQuantity = orderDTO.products.Where(p => p.Id == item.Id).First().Quantity;
                order.Price += (itemQuantity * item.Price);
                item.Quantity = itemQuantity;
            }
            order.IsDelivery = orderDTO.IsDelivery;
            order.Observation = orderDTO.Observation;
            order.OrderProducts = orderDTO.products.Select(p => new OrderProduct() { ProductId = p.Id, ProductCount = p.Quantity }).ToList();
            if (orderDTO.IsDelivery)
            {
                var config = _configsRepository.GetConfig();
                if (config != null)
                {
                    order.Price += config.DeliveryTax;
                }
            }

            _orderRepository.Insert(order, true);

            return new DTOs.Response.Order.Order()
            {
                Id = order.Id,
                Code = order.Code,
                Address = order.Address,
                Observation = order.Observation,
                CreatedDate = order.CreatedDate,
                Delivered = order.Delivered,
                Disapproved = order.Disapproved,
                InDelivery = order.InDelivery,
                IsDelivery = order.IsDelivery,
                products = products,
                Preparing = order.Preparing,
                Price = order.Price,
                Ready = order.Ready,
                UserId = order.UserId,
                OrderProducts = order.OrderProducts
            };
        }

        public CoffeeBreak.Application.DTOs.Response.Order.Order GetById(int orderId)
        {
            var order = _orderRepository.GetById(orderId);
            if (order == null)
            {
                throw new BaseException("Pedido não encontrado", System.Net.HttpStatusCode.NotFound);
            }

            var products = _orderRepository.GetItemsByOrder(orderId);

            return new DTOs.Response.Order.Order()
            {
                Id = order.Id,
                Code = order.Code,
                Address = order.Address,
                Observation = order.Observation,
                CreatedDate = order.CreatedDate,
                Delivered = order.Delivered,
                Disapproved = order.Disapproved,
                InDelivery = order.InDelivery,
                IsDelivery = order.IsDelivery,
                products = products,
                Preparing = order.Preparing,
                Price = order.Price,
                Ready = order.Ready,
                UserId = order.UserId,
                OrderProducts = order.OrderProducts,
                DeliveryManId = order.DeliveryManId
            };
        }

        public List<CoffeeBreak.Application.DTOs.Response.Order.Order> GetAll()
        {
            return _orderRepository.GetAll().Select(p => new DTOs.Response.Order.Order()
            {
                Id = p.Id,
                Code = p.Code,
                Address = p.Address,
                CreatedDate = p.CreatedDate,
                Delivered = p.Delivered,
                Disapproved = p.Disapproved,
                InDelivery = p.InDelivery,
                IsDelivery = p.IsDelivery,
                Observation = p.Observation,
                OrderProducts = p.OrderProducts,
                Preparing = p.Preparing,
                Price = p.Price,
                Ready = p.Ready,
                UserId = p.UserId,
                DeliveryManId = p.DeliveryManId
            }).OrderByDescending(order => order.CreatedDate).ToList();
        }

        public List<CoffeeBreak.Application.DTOs.Response.Order.Order> GetAll(int userId)
        {
            return _orderRepository.GetAllByUser(userId).Select(p => new DTOs.Response.Order.Order()
            {
                Id = p.Id,
                Code = p.Code,
                Address = p.Address,
                CreatedDate = p.CreatedDate,
                Delivered = p.Delivered,
                Disapproved = p.Disapproved,
                InDelivery = p.InDelivery,
                IsDelivery = p.IsDelivery,
                Observation = p.Observation,
                OrderProducts = p.OrderProducts,
                Preparing = p.Preparing,
                Price = p.Price,
                Ready = p.Ready,
                UserId = p.UserId
            }).OrderByDescending(order => order.CreatedDate).ToList();
        }

        public List<CoffeeBreak.Application.DTOs.Response.Order.Order> GetDeliveries(int? userId)
        {
            return _orderRepository.GetDeliveries(userId).OrderBy(order => order.Delivered).ThenBy(order => order.CreatedDate).ToList();
        }

        public List<CoffeeBreak.Application.DTOs.Response.Order.Order> GetPending()
        {
            return _orderRepository.GetPending().OrderBy(order => order.CreatedDate).ToList();
        }
        
        public CoffeeBreak.Application.DTOs.Response.Order.Order Approve(int orderId)
        {
            var order = _orderRepository.Approve(orderId);
            _SendUpdateOrderEmail(order);

            return order;
        }
        public CoffeeBreak.Application.DTOs.Response.Order.Order Disapprove(int orderId)
        {
            var order = _orderRepository.Disapprove(orderId);
            _SendUpdateOrderEmail(order);

            return order;
        }
        public CoffeeBreak.Application.DTOs.Response.Order.Order MakeReady(int orderId)
        {
            var order = _orderRepository.MakeReady(orderId);
            _SendUpdateOrderEmail(order);

            return order;
        }
        public CoffeeBreak.Application.DTOs.Response.Order.Order Sent(int orderId)
        {
            var order = _orderRepository.Sent(orderId);
            _SendUpdateOrderEmail(order);

            return order;
        }
        public CoffeeBreak.Application.DTOs.Response.Order.Order Deliver(int orderId)
        {
            var order = _orderRepository.Deliver(orderId);
            _SendUpdateOrderEmail(order);

            return order;
        }
        public CoffeeBreak.Application.DTOs.Response.Order.Order UpdateDeliveryMan(int orderId, int? deliveryManId)
        {
            var order = _orderRepository.GetById(orderId);
            if (order == null)
            {
                throw new BaseException("Pedido não encontrado", System.Net.HttpStatusCode.NotFound);
            }

            order.DeliveryManId = deliveryManId;
            _orderRepository.Commit();
            return new DTOs.Response.Order.Order()
            {
                Id = order.Id,
                Code = order.Code,
                Address = order.Address,
                Observation = order.Observation,
                CreatedDate = order.CreatedDate,
                Delivered = order.Delivered,
                Disapproved = order.Disapproved,
                InDelivery = order.InDelivery,
                IsDelivery = order.IsDelivery,
                Preparing = order.Preparing,
                Price = order.Price,
                Ready = order.Ready,
                UserId = order.UserId
            };
        }

        private void _SendUpdateOrderEmail(CoffeeBreak.Application.DTOs.Response.Order.Order order)
        {
            var user = _userRepository.GetById(order.UserId);

            var message = $"Situação do pedido: {order.Status}.";
            if (!order.IsDelivery)
            {
                var config = _configsRepository.GetConfig();
                if (config != null)
                {
                    message = $"{message}{Environment.NewLine} Retirar no endereço: {config.Address}";
                }
            }

            var EmailMessage = new CoffeeBreak.Application.DTOs.Request.Email.Email
            {
                RecipientEmail = user.Email,
                Subject = $"Atualizacao do pedido #{order.Code}",
                Message = message
            };

            _emailService.SendEmail(EmailMessage);
        }
    }
}
