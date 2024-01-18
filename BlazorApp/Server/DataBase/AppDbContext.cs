using BlazorApp.Server.Models;

using Microsoft.EntityFrameworkCore;

namespace BlazorApp.Server.DataBase
{
    public class AppDbContext : DbContext
    {
        public DbSet<Order> Orders { get; set; }
        public DbSet<Window> Windows { get; set; }
        public DbSet<SubElement> Elements { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.Property(e => e.Name).HasMaxLength(200);
                entity.Property(e => e.State).HasMaxLength(200);
            });
            modelBuilder.Entity<Window>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.Property(e => e.Name).HasMaxLength(200);
                entity.Property(e => e.OrderId).IsRequired();
                entity.HasOne(u => u.Order)
                    .WithMany(c => c.Windows)
                    .OnDelete(DeleteBehavior.Cascade);
            });
            modelBuilder.Entity<SubElement>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.Property(e => e.Type).HasMaxLength(200);
                entity.Property(e => e.WindowId).IsRequired();
                entity.HasOne(u => u.Window)
                    .WithMany(c => c.Elements)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            var order1 = new Order
            {
                Id = 1,
                Name = "New York Building 1",
                State = "NY",
            };
            var order2 = new Order
            {
                Id = 2,
                Name = "California Hotel AJK",
                State = "CA",
            };

            var window1 = new Window()
            {
                Id = 1,
                Name = "A51",
                QuantityOfWindows = 4,
                OrderId = order1.Id,
            };
            var window2 = new Window()
            {
                Id = 2,
                Name = "C Zone 5",
                QuantityOfWindows = 2,
                OrderId = order1.Id,
            };
            var window3 = new Window()
            {
                Id = 3,
                Name = "GLB",
                QuantityOfWindows = 3,
                OrderId = order2.Id,
            };
            var window4 = new Window()
            {
                Id = 4,
                Name = "OHF",
                QuantityOfWindows = 10,
                OrderId = order2.Id
            };

            var subElement1 = new SubElement()
            {
                Id = 1,
                Element = 1,
                Type = "Doors",
                Width = 1200,
                Height = 1850,
                WindowId = window1.Id
            };
            var subElement2 = new SubElement()
            {
                Id = 2,
                Element = 2,
                Type = "Window",
                Width = 800,
                Height = 1850,
                WindowId = window1.Id
            };
            var subElement3 = new SubElement()
            {
                Id = 3,
                Element = 3,
                Type = "Window",
                Width = 700,
                Height = 1850,
                WindowId = window1.Id
            };
            var subElement4 = new SubElement()
            {
                Id = 4,
                Element = 1,
                Type = "Window",
                Width = 1500,
                Height = 2000,
                WindowId = window2.Id
            };
            var subElement5 = new SubElement()
            {
                Id = 5,
                Element = 1,
                Type = "Doors",
                Width = 1400,
                Height = 2200,
                WindowId = window3.Id
            };
            var subElement6 = new SubElement()
            {
                Id = 6,
                Element = 2,
                Type = "Window",
                Width = 600,
                Height = 2200,
                WindowId = window3.Id
            };
            var subElement7 = new SubElement()
            {
                Id = 7,
                Element = 1,
                Type = "Window",
                Width = 1400,
                Height = 2200,
                WindowId = window4.Id
            };
            var subElement8 = new SubElement()
            {
                Id = 8,
                Element = 1,
                Type = "Window",
                Width = 1400,
                Height = 2200,
                WindowId = window4.Id
            };

            modelBuilder.Entity<Order>().HasData(order1, order2);
            modelBuilder.Entity<Window>().HasData(window1, window2, window3, window4);
            modelBuilder.Entity<SubElement>().HasData(
                subElement1,
                subElement2,
                subElement3,
                subElement4,
                subElement5,
                subElement6,
                subElement7,
                subElement8);
        }
    }
}
