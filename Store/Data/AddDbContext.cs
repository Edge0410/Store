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
        /*modelBuilder.Entity<UserRole>()
            .HasOne(u => u.User)
            .WithMany(uu => uu.UserRoles)
            .HasForeignKey(uuu => uuu.UserId);

        modelBuilder.Entity<UserRole>()
            .HasOne(u => u.Role)
            .WithMany(uu => uu.UserRoles)
            .HasForeignKey(uuu => uuu.RoleId);

        modelBuilder.Entity<OrderList>()
            .HasOne(u => u.Order)
            .WithMany(uu => uu.OrderLists)
            .HasForeignKey(uuu => uuu.OrderId);

        modelBuilder.Entity<OrderList>()
            .HasOne(u => u.Product)
            .WithMany(uu => uu.OrderLists)
            .HasForeignKey(uuu => uuu.ProductId);
        */
    }

    /*    public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<OrderList> OrderLists { get; set; }
    */

    public DbSet<User> Users { get; set; }
    public DbSet<Product> Products { get; set; }

}
