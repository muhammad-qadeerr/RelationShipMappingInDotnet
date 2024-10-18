using Microsoft.EntityFrameworkCore;

namespace DOTNETRELATIONS.Data.Entities;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
    { }

    public DbSet<Customer> Customer { get; set; }
    public DbSet<CustomerAddresses> CustomerAddresses { get; set;}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
       // Foreign key relationships are defined using EF core fluent API's
        // 1:M relationship between customer and customerAddresses
        modelBuilder.Entity<CustomerAddresses>()
            .HasOne(_ => _.Customer)
            .WithMany(_ => _.CustomerAddresses)
            .HasForeignKey(_ => _.CustomerId);

            base.OnModelCreating(modelBuilder);
    }
}