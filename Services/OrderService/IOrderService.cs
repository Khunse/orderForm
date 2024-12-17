using orderform.Models;

namespace orderform.Services.OrderService;

public interface IOrderService
{
    Task<List<Order>> GetOrders(OrderSearchFilter orderSearchFilter);
    Task<bool> AddOrder(Order order);
}