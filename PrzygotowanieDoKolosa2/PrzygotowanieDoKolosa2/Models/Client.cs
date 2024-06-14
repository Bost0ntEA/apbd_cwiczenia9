using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.SignalR;

namespace PrzygotowanieDoKolosa2.Models;

[Table("Client")]
public class Client
{
    [Key]
    public int IdClient { get; set; }
    [Required]
    [MaxLength(100)]
    public string Name { get; set; }
    [Required]
    [MaxLength(100)]
    public string LastName { get; set; }
    [Required]
    public DateTime Birthday { get; set; }
    [Required]
    [MaxLength(100)]
    public string Pesel { get; set; }
    [Required]
    [MaxLength(100)]
    [EmailAddress]
    public string Email { get; set; }
    [Required]
    public int IdClientCategory { get; set; }
    
    [ForeignKey(nameof(IdClientCategory))]
    public ClientCategory ClientCategoryNavigation { get; set; }

    public List<Reservation> Reservations { get; set; }
    
}