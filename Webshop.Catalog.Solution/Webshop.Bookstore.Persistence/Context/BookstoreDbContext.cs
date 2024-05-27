using Microsoft.EntityFrameworkCore;
using Webshop.BookStore.Domain.AggregateRoots;

namespace Webshop.Bookstore.Persistence.Context;

public class BookstoreDbContext : DbContext
{
    public BookstoreDbContext(DbContextOptions<BookstoreDbContext> options) : base(options)
    {
    }

    public DbSet<Book> Books { get; set; }
    public DbSet<BookstoreCustomer> BookstoreCustomers { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
    public DbSet<Order> Orders { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Book>(entity =>
        {
            entity.HasKey(b => b.Id);
            entity.Property(b => b.Title).IsRequired();
            entity.Property(b => b.Author).IsRequired();
            entity.Property(b => b.Price).HasColumnType("decimal(18,2)");
            entity.HasOne(b => b.Seller)
                .WithMany()
                .HasForeignKey(b => b.SellerId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(o => o.Id);
            entity.Property(o => o.TotalAmount).HasColumnType("decimal(18,2)");
            entity.HasOne<BookstoreCustomer>()
                .WithMany()
                .HasForeignKey(o => o.BuyerId);

            entity.HasMany(o => o.OrderItems)
                .WithOne()
                .HasForeignKey(oi => oi.BookId);
        });

        modelBuilder.Entity<OrderItem>(entity =>
        {
            entity.HasKey(oi => oi.Id); // Change primary key to Id
            entity.Property(oi => oi.Price).HasColumnType("decimal(18,2)");

            // Set up the relationship with Order
            entity.HasOne<Order>()
                .WithMany(o => o.OrderItems)
                .HasForeignKey(oi => oi.OrderId);
        });
        modelBuilder.Entity<BookstoreCustomer>(entity =>
        {
            entity.HasKey(bc => bc.Id);
        });

    }
}