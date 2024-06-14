using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PrzygotowanieDoKolosa2.Models;
[Table("BoatStandard")]
public class BoatStandard
{
    [Key]
    public int IdBoatStandard { get; set; }
    [Required]
    [MaxLength(100)]
    public string Name { get; set; }
    [Required]
    public int Level { get; set; }
    public List<Reservation> ReservationsNavigation { get; set; }
    public List<SailBoat> SailBoatsNavigation { get; set; }
}