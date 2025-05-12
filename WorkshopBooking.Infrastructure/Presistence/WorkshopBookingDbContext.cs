using Microsoft.EntityFrameworkCore;

using WorkshopBooking.Domain.Entities;

namespace WorkshopBooking.Infrastructure.Presistence
{
    public class WorkshopBookingDbContext : DbContext
    {
        public WorkshopBookingDbContext(DbContextOptions<WorkshopBookingDbContext> options) : base(options) {}
        public DbSet<User> Users { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<AvailableSlot> AvailableSlots { get; set; }
        public DbSet<ServiceType> ServiceTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Relation mellan User och Customer
            modelBuilder.Entity<User>()
                .HasOne(u => u.Customer)
                .WithOne(c => c.User)
                .HasForeignKey<Customer>(c => c.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Relation mellan User och Employee
            modelBuilder.Entity<User>()
                .HasOne(u => u.Employee)
                .WithOne(e => e.User)
                .HasForeignKey<Employee>(e => e.UserId)
                .OnDelete(DeleteBehavior.Restrict);


            // Relation mellan AvailableSlot och ServiceType
            modelBuilder.Entity<AvailableSlot>()
                .HasOne(a => a.ServiceType)
                .WithMany()
                .HasForeignKey(a => a.ServiceTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            // Relation mellan Booking och ServiceType
            modelBuilder.Entity<Booking>()
                .HasOne(b => b.ServiceType)
                .WithMany()
                .HasForeignKey(b => b.ServiceTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            // Relation mellan Booking och Employee
            modelBuilder.Entity<Booking>()
                .HasOne(b => b.Employee)
                .WithMany()
                .HasForeignKey(b => b.EmployeeId)
                .OnDelete(DeleteBehavior.Restrict);

            // Relation mellan Booking och Customer
            modelBuilder.Entity<Booking>()
                .HasOne(b => b.Customer)
                .WithMany()
                .HasForeignKey(b => b.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);

            // Definiera prisformat för ServiceType 
            modelBuilder.Entity<ServiceType>()
                .Property(s => s.Price)
                .HasColumnType("decimal(18,2)");
        }
    }
}
