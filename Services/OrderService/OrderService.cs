using orderform.External.DataAccess;
using orderform.Models;

namespace orderform.Services.OrderService;

public class OrderService : IOrderService
{
    private readonly IDataAccess _dataAccess;
    public OrderService(IDataAccess dataAccess)
    {
        _dataAccess = dataAccess;
    }

    public async Task<bool> AddOrder(Order order)
    {
        var saveData = await _dataAccess.AddOrder(order);

        return saveData;
    }

    public async Task<List<Order>> GetOrders(OrderSearchFilter orderSearchFilter)
    {
        var dataList = await _dataAccess.GetOrders(orderSearchFilter);
        
        return dataList;
    }
}
