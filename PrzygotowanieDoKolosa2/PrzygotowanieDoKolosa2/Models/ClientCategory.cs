using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PrzygotowanieDoKolosa2.Models;

[Table("ClientCategory")]
public class ClientCategory
{
    [Key]
    public int IdClientCategory { get; set; }
    [Required]
    [MaxLength(100)]
    public string Name { get; set; }
    [Required]
    public int DiscountPerc { get; set; }
    public List<Client> ClientNavigation { get; set; }
}