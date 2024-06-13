using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cwiczenia10.Models;
[Table("Prescription")]
public class Prescription
{
    [Key]
    public int IdPrescription { get; set; }

    public DateTime Date { get; set; }
    
    public DateTime DueDate { get; set; }
    public int idPatient { get; set; }
    public int IdDoctor { get; set; }
    [ForeignKey(nameof(idPatient))]
    public Patient Patient { get; set; } = null;

    [ForeignKey(nameof(IdDoctor))]
    public Doctor Doctor { get; set; } = null;

    public List<Prescription_Medicament> PrescriptionMedicaments = new List<Prescription_Medicament>(); 

}