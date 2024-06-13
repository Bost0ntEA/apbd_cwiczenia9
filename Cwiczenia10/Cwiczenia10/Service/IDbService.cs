using Cwiczenia10.Models;
using Cwiczenia10.Models.DTOs;

namespace Cwiczenia10.Service;

public interface IDbService
{
    Task<Patient?> DoesPatientExist(int id);

    Task<bool> DoesMedicamentExist(List<MedDTO> medicaments);
}