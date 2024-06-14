using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using PrzygotowanieDoKolosa2.Models;

namespace PrzygotowanieDoKolosa2.Context;

public class ReservationContext: DbContext
{

    public DbSet<ClientCategory> ClientCategory { get; set; }

    public  DbSet<Client> Client { get; set; }

    public  DbSet<Reservation> Reservation { get; set; }
        
    public  DbSet<SailBoat> SailBoat { get; set; }
    
    public  DbSet<BoatStandard> BoatStandards { get; set; }
    
    protected ReservationContext()
    {
    }
    public ReservationContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<ClientCategory>().HasData(
            new ClientCategory() {IdClientCategory = 1,Name = "Vip",DiscountPerc = 30},
            new ClientCategory() { IdClientCategory = 2,Name = "Standard",DiscountPerc = 0 },
            new ClientCategory() { IdClientCategory = 3,Name = "Svip",DiscountPerc = 50 }
        );
        modelBuilder.Entity<Client>().HasData(
            new Client() {IdClient = 1,Name = "Juan",LastName = "Jun",Birthday = new DateTime(1999,10,1),Pesel = "23578629532",Email = "Juan@gmail.com",IdClientCategory = 2},
            new Client() { IdClient = 2,Name = "Palmosz",LastName = "Palmowski",Birthday = new DateTime(2000,1,10),Pesel = "97679654",Email = "PalmiXXX@onet.com",IdClientCategory = 3 }
        );
        modelBuilder.Entity<BoatStandard>().HasData(
            new BoatStandard() { IdBoatStandard = 1, Name = "Luxury",Level = 3},
            new BoatStandard() { IdBoatStandard = 2, Name = "Melina", Level = 1}
        );
        modelBuilder.Entity<Reservation>().HasData(
            new Reservation() {IdReservation = 1,IdClient = 1,DateFrom = DateTime.Now, DateTo = new DateTime(2024,10,1), IdBoatStandard = 2, Capacity = 5, NumOfBoats = 2, Fulfilled = 0, Price = null, CancelReason = null},
            new Reservation() { IdReservation = 2,IdClient = 1,DateFrom = DateTime.Now, DateTo = new DateTime(2024,10,1), IdBoatStandard = 2, Capacity = 2, NumOfBoats = 1, Fulfilled = 0, Price = null, CancelReason = null }
        );
        modelBuilder.Entity<SailBoat>().HasData(
            new SailBoat() { IdSailboat = 1, Name = "Milenials 1980", Capacity = 10, Description = "staroc i zlom", IdBoatStandard = 2, Price = 200 },
            new SailBoat() { IdSailboat = 2, Name = "Golec Ship", Capacity = 4, Description = "nowiutkie jak glowa bobasa", IdBoatStandard = 1, Price = 2000}
        );
        modelBuilder.Entity<Sailboat_Reservation>().HasData(
            new Sailboat_Reservation() { IdReservation = 1,IdSailboat = 1},
            new Sailboat_Reservation() { IdReservation = 1,IdSailboat = 2},
            new Sailboat_Reservation() { IdReservation = 2,IdSailboat = 2}
        );
        
        modelBuilder.Entity<Sailboat_Reservation>()
            .HasOne(sr => sr.SailBoatNavigation)
            .WithMany(s => s.SailboatReservationsNavigation)
            .HasForeignKey(sr => sr.IdSailboat)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<Sailboat_Reservation>()
            .HasOne(sr => sr.ReservationNavigation)
            .WithMany(r => r.SailboatReservationsNavigation)
            .HasForeignKey(sr => sr.IdReservation)
            .OnDelete(DeleteBehavior.NoAction);
    }


}