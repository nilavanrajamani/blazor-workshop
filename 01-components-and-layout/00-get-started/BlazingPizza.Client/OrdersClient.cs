using System.Net.Http.Json;

namespace BlazingPizza.Client
{
    public class OrdersClient
    {
        private HttpClient httpClient;

        public OrdersClient(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<IEnumerable<OrderWithStatus>> GetOrders()
        {
            return await httpClient.GetFromJsonAsync("orders", OrderContext.Default.ListOrderWithStatus) ?? new();
        }

        public async Task<OrderWithStatus> GetOrder(int orderId) =>
         await httpClient.GetFromJsonAsync($"orders/{orderId}", OrderContext.Default.OrderWithStatus) ?? new();

        public async Task<int> PlaceOrder(Order order)
        {
            var response = await httpClient.PostAsJsonAsync("orders", order, OrderContext.Default.Order);
            response.EnsureSuccessStatusCode();
            var orderId = await response.Content.ReadFromJsonAsync<int>();
            return orderId;
        }
    }
}
