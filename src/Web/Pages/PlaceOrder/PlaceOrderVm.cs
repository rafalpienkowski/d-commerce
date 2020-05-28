using System.ComponentModel.DataAnnotations;

namespace Web.Pages.PlaceOrder
{
    public class PlaceOrderVm
    {
        [Required]
        public string ProductIds { get; set; }
        [Required]
        public string ShippingTypeId { get; set; }
    }
}