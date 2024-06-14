using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SqlTypes;
using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace PrzygotowanieDoKolosa2.Models;
[Table("Reservation")]
public class Reservation
{
    [Key]
    public int IdReservation { get; set; }
    [Required]
    public int IdClient { get; set; }
    [Required]
    public DateTime DateFrom { get; set; }
    [Required]
    public DateTime DateTo { get; set; }
    [Required]
    public int IdBoatStandard { get; set; }
    [Required]
    public int Capacity { get; set; }
    [Required]
    public int NumOfBoats { get; set; }
    [Required]
    public byte Fulfilled { get; set; }
    [Precision(2)]
    public double? Price { get; set; }
    [MaxLength(200)]
    public string? CancelReason { get; set; }
    [ForeignKey(nameof(IdClient))]
    public Client ClientNavigation { get; set; }
    [ForeignKey(nameof(IdBoatStandard))]
    public BoatStandard BoatStandardNavigation { get; set; }
    public List<Sailboat_Reservation> SailboatReservationsNavigation { get; set; }
    
}