using CoffeeBreak.Application.DTOs.Response.Product;
using CoffeeBreak.Application.Common.Extensions;

namespace CoffeeBreak.Application.DTOs.Response.Order
{
    public class Order : CoffeeBreak.Domain.Entities.Order
    {
        public List<ProductResponse> products { get; set; }
        public string Status
        {
            get
            {
                if (this.Disapproved.HasValue)
                {
                    return $"Recusado às {this.Disapproved.Value.ToDateTimeString()}";
                }

                if (this.Delivered.HasValue)
                {
                    return $"Entregue às {this.Delivered.Value.ToDateTimeString()}";
                }

                if (this.InDelivery.HasValue)
                {
                    return $"Saiu para entrega às {this.InDelivery.Value.ToDateTimeString()}";
                }

                if (this.Ready.HasValue)
                {
                    return $"Pronto às {this.Ready.Value.ToDateTimeString()}";
                }

                if (this.Preparing.HasValue)
                {
                    return $"Em preparação desde às {this.Preparing.Value.ToDateTimeString()}";
                }
                
                return "Em analise";
            }
        }

        public string CreatedDateFormated
        {
            get
            {
                return this.CreatedDate.ToDateTimeString();
            }
        }
    }
}
