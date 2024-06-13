using Cwiczenia10.Data;
using Cwiczenia10.Models;
using Cwiczenia10.Models.DTOs;
using Microsoft.EntityFrameworkCore;

namespace Cwiczenia10.Service;

public class DbService : IDbService
{
    public HospitalContext Context;

    public DbService(HospitalContext context)
    {
        Context = context;
    }

    public async Task<Patient?> DoesPatientExist(int id)
    {
        return await Context.Patients.FindAsync(id);
    }
    

    public async Task<bool> DoesMedicamentExist(List<MedDTO> medicaments)
    {
        foreach(MedDTO medicament in medicaments)
        {
            if (await Context.Medicaments.FindAsync(medicament.IdMedicament) == null)
            {
                return false ;
            }
        }
        return true;
    }
}