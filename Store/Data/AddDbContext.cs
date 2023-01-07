using Microsoft.EntityFrameworkCore;
using Store.Models;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
 : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // no unique username and email

        modelBuilder.Entity<User>()
            .HasIndex(u => u.Username)
            .IsUnique();

        modelBuilder.Entity<User>()
            .HasIndex(u => u.Email)
            .IsUnique();

        // 1-M

        modelBuilder.Entity<User>()
            .HasMany(o => o.Orders)
            .WithOne(oo => oo.User);

        // M-M

        modelBuilder.Entity<OrderList>()
            .HasKey(o => new { o.OrderId, o.ProductId, o.Id });

        modelBuilder.Entity<OrderList>()
            .HasOne(o => o.Order)
            .WithMany(oo => oo.OrderList)
            .HasForeignKey(ooo => ooo.OrderId);

        modelBuilder.Entity<OrderList>()
            .HasOne(o => o.Product)
            .WithMany(oo => oo.OrderList)
            .HasForeignKey(ooo => ooo.ProductId);

    }

    public DbSet<User> Users { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderList> OrderLists { get; set; }

}
