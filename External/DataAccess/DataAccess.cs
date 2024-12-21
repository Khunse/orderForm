using Microsoft.EntityFrameworkCore;
using orderform.External.Data;
using orderform.External.DataAccess;
using orderform.Models;

namespace orderform.External.DataAccess;

public class DataAccess : IDataAccess
{
    private readonly DataContext _context;

    public DataAccess(DataContext dataContext)
    {
        _context = dataContext;
    }

    public async Task<bool> AddOrder(Order order)
    {
        var IsSave = false;

        try
        {
            var newOrder = new Order
            {
                created_at = DateTime.Now,
                cutting = order.cutting,
                design = order.design,
                due_date = order.due_date,
                IsDelete = false,
                order_date = DateTime.Now,
                order_name = order.order_name,
                packaging = order.packaging,
                printing = order.printing,
                qc = order.qc,
                sewing = order.sewing
            };

            _context.Orders.Add(newOrder);

            var saveData = await  _context.SaveChangesAsync();

            if( saveData > 0 ) IsSave = true;
            
        }
        catch (Exception ex)
        {

        }

        return IsSave;
    }

    public async Task<List<Order>> GetOrders(OrderSearchFilter orderSearchFilter)
    {
        var dataList = new List<Order>();

        try
        {

            var orders =  _context.Orders.AsNoTracking();

            if( orderSearchFilter.order_date is not null)
            {
                var orderda = orderSearchFilter.order_date.Value.Date;
                orders = orders.Where( x => x.order_date.Date.Equals(orderSearchFilter.order_date.Value.Date));
            }

            if( orderSearchFilter.due_date is not null )
            {
                // var orderda = orderSearchFilter.due_date.Value.Date;

                orders = orders.Where( x => x.due_date.Date.Equals(orderSearchFilter.due_date.Value.Date));
            }

            dataList = await orders.ToListAsync();

        }
        catch (Exception ex)
        {

        }

        return dataList;
    }


}