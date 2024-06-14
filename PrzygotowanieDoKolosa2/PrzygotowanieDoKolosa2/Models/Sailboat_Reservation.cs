using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PrzygotowanieDoKolosa2.Models;
[Table("Sailboat_Reservation")]
[PrimaryKey(nameof(IdReservation),nameof(IdSailboat))]
public class Sailboat_Reservation
{
    public int IdSailboat { get; set; }
    public int IdReservation { get; set; }
    [ForeignKey(nameof(IdReservation))]
    public Reservation ReservationNavigation { get; set; }
    [ForeignKey(nameof(IdSailboat))]
    public SailBoat SailBoatNavigation { get; set; }
    
}