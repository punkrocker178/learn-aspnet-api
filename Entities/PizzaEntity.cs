using demo_asp.net.Models;
using Microsoft.EntityFrameworkCore;

namespace demo_asp.net.Entities;
public class PizzaDb : DbContext
{
    public PizzaDb(DbContextOptions options) : base(options) { }
    public DbSet<Pizza> Pizzas { get; set; } = null!;
}
