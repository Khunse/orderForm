using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace orderform.Models;

[Table("Order")]
public class Order
{
    [Key]
    public long? Id { get; set; }
    [Required]
    public string order_name { get; set; } = null!;
    [Required]
    public DateTime order_date { get; set; }
    [Required]
    public DateTime due_date { get; set; }
    [Required]
    public string design { get; set; } = null!;
    [Required]
    public string cutting { get; set; } = null!;
    [Required]
    public string printing { get; set; } = null!;
    [Required]
    public string sewing { get; set; } = null!;
    [Required]
    public string qc { get; set; } = null!;
    [Required]
    public string packaging { get; set; } = null!;
    public bool IsDelete { get; set; }
    public DateTime created_at { get; set; }
    public DateTime? updated_at { get; set; }
}