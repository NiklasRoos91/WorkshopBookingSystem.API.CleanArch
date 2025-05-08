using Microsoft.EntityFrameworkCore;

using WorkshopBooking.Domain.Entities;

namespace WorkshopBooking.Infrastructure.Presistence
{
    public class WorkshopBookingDbContext : DbContext
    {
        public WorkshopBookingDbContext(DbContextOptions<WorkshopBookingDbContext> options) : base(options) {}
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<AvailableSlot> AvailableSlots { get; set; }
        public DbSet<ServiceType> ServiceTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<AvailableSlot>()
              .HasOne(a => a.ServiceType)
              .WithMany()
              .HasForeignKey(a => a.ServiceTypeId)
              .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Booking>()
                .HasOne(b => b.ServiceType)
                .WithMany()
                .HasForeignKey(b => b.ServiceTypeId)
                .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<ServiceType>()
                .Property(s => s.Price)
                .HasColumnType("decimal(18,2)");
        }
    }

}
