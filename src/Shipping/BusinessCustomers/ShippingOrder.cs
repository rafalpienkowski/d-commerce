using System;
using System.ComponentModel.DataAnnotations;
using Shipping.Framework;

namespace Shipping.BusinessCustomers
{
    public class ShippingOrder : Entity
    {
        public string UserId { get; private set; }
        [Key]
        public Guid OrderId { get; private set; }
        public string ShippingTypeId { get; private set; }
        public Status Status { get; private set; }

        private ShippingOrder()
        {
        }

        public static ShippingOrder Create(string userId, Guid orderId, string shippingTypId)
        {
            return new ShippingOrder
            {
                OrderId = orderId,
                UserId = userId,
                ShippingTypeId = shippingTypId,
                Status = Status.Created
            };
        }

        public void UpdateShippingStatus(ShippingConfirmation shippingConfirmation)
        {
            if (shippingConfirmation.Status == ShippingStatus.Success)
            {
                Status = Status.Arranged;
                PendingEvents.Add(new ShippingArranged
                {
                    OrderId = OrderId
                });
            }
            else
            {
                Status = Status.Postponed;
            }
        }
    }

    public enum Status
    {
        Created = 1,
        Arranged = 2,
        Postponed = 3
    }
}