namespace orderform.Models;

public class RequestModel
{
    public Order req { get; set; }
    public OrderSearchFilter orderSearchFilter { get; set; } = null!;
    public string orderId { get; set;}
}


public class OrderSearchFilter
{
    public DateTime? order_date { get; set; }
    public DateTime? due_date { get; set; }

}