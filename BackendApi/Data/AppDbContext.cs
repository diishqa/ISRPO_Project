using Microsoft.EntityFrameworkCore;
using BackendApi.Models ;

namespace BackendApi.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions <AppDbContext> options) : base(options){}
    public DbSet<Booking> Bookings { get;set;}
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Booking>().HasData(
            new Booking
            {
                Id = 1,
                Name = "Жанатпаева Динара" , 
                Phone = "+70000006578" , 
                Date = new DateTime(2026 , 5 , 15 , 19 , 0 , 0),
                TableId = 1 ,
                Status = "потверждено", 
            },
            new Booking
            {
                Id = 2,
                Name = "Кузьмина Диана" , 
                Phone = "+70000006590" , 
                Date = new DateTime(2026 , 5 , 15 , 20 , 0 , 0),
                TableId = 2,
                Status =  "новое",
            }
        );
    }

}