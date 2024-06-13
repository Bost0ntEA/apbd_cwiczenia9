using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;

namespace Cwiczenia10.Models;

[Table("Prescription_Medicament")]
[PrimaryKey(nameof(IdMedicament),nameof(IdPrescription))]
public class Prescription_Medicament
{
    public int IdMedicament { get; set; }
    
    public int IdPrescription { get; set; }
    
    public int? Dose { get; set; }
    [MaxLength(100)]
    public String Details { get; set; }
    [ForeignKey(nameof(IdMedicament))]
    public Medicament Medicaments { get; set; }
    [ForeignKey(nameof(IdPrescription))]
    public Prescription Prescription { get; set; }
}