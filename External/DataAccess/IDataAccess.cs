using orderform.Models;

namespace orderform.External.DataAccess;

public interface IDataAccess
{
    Task<List<Order>> GetOrders(OrderSearchFilter orderSearchFilter);
    Task<bool> AddOrder(Order order);
}