using Microsoft.EntityFrameworkCore;
using orderform.Models;

namespace orderform.External.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {

    }

    public DbSet<Order> Orders { get; set; }
}