namespace Cwiczenia10.Models.DTOs;

public class PresciptionDTO
{
    public AddPatient Patient { get; set; } = null!;

    public List<MedDTO> Medicaments { get; set; } = new List<MedDTO>();

    public DateTime Date { get; set; }

    public DateTime DueDate { get; set; }
}