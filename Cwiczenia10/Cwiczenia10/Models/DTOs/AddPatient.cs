namespace Cwiczenia10.Models.DTOs;

public class AddPatient
{
    public int IdPatient { get; set; }

    public string FirstName { get; set; } = null!;
    
    public string LastName { get; set; } = null!;
    
    public DateTime BirthDate { get; set; }
}