using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SqlTypes;
using Microsoft.EntityFrameworkCore;

namespace PrzygotowanieDoKolosa2.Models;
[Table("Sailboat")]
public class SailBoat
{
    [Key]
    public int IdSailboat { get; set; }
    [Required]
    [MaxLength(100)]
    public string Name { get; set; }
    [Required]
    public int Capacity { get; set; }
    [Required]
    [MaxLength(100)]
    public string Description { get; set; }
    public int IdBoatStandard { get; set; }
    [Required]
    [Precision(2)]
    public double Price { get; set; }
    [ForeignKey(nameof(IdBoatStandard))]
    public BoatStandard BoatStandardNavigation { get; set; }

    public List<Sailboat_Reservation> SailboatReservationsNavigation { get; set; }
}