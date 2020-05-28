using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace Web.Pages.Orders
{
    public class OrdersBase : ComponentBase
    {
        [Inject]
        public IOrdersRepository OrdersRepository { get; set; }
        public IEnumerable<Order> Orders { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await GetOrders();
        }

        protected async Task GetOrders()
        {
            Orders = await OrdersRepository.Get();
        }
    }
}