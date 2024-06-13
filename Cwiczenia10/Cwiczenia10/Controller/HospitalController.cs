using System.Transactions;
using Cwiczenia10.Data;
using Cwiczenia10.Models;
using Cwiczenia10.Models.DTOs;
using Cwiczenia10.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Cwiczenia10.Controller;

[ApiController]
public class HospitalController : ControllerBase
{
    public IDbService Service;
    public HospitalContext Context;

    public HospitalController(IDbService service, HospitalContext context)
    {
        Service = service;
        Context = context;
    }

    [HttpPost]
    [Route("prescription")]
    public async Task<IActionResult> AddPrescription(PresciptionDTO dto)
    {
        var pacjent = await Service.DoesPatientExist(dto.Patient.IdPatient);

        if (!await Service.DoesMedicamentExist(dto.Medicaments))
        {
            return NotFound("jakis Med nie istnieje");
        }
        
        if (dto.Medicaments.Count > 10)
        {
            return NotFound("Pres ma wiecej niz 10 med");
        }

        if (dto.DueDate < dto.Date)
        {
            return NotFound("Zla data");
        }

        using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
        {
            var odp = Context.Prescriptions.Add(
                new Prescription()
                {
                    Date = dto.Date,
                    DueDate = dto.DueDate,
                    IdDoctor = 1, 
                    idPatient = dto.Patient.IdPatient
                }
            );
            
            await Context.SaveChangesAsync();

            foreach (var medicament in dto.Medicaments)
                
            {
                Context.PrescriptionMedicaments.Add(
                    new Prescription_Medicament()
                    {
                        IdMedicament = medicament.IdMedicament,
                        IdPrescription = odp.Entity.IdPrescription,
                        Details = medicament.Description,
                        Dose = medicament.Dose
                    }
                );
            }

            if (pacjent == null)
            {
                Context.Patients.Add(
                    new Patient()
                    {
                        Birthdate = dto.Patient.BirthDate,
                        FirstName = dto.Patient.FirstName,
                        LastName = dto.Patient.LastName,
                        IdPatient = dto.Patient.IdPatient
                    }
                );
            }

            await Context.SaveChangesAsync();
            scope.Complete();
        }

        return Ok("dodano Pres");
    }

    
    [HttpGet]
    [Route("patient/{id:int}")]
    public async Task<IActionResult> GetPatient(int id)
    {
        var pacjent = await Context.Patients
            .Include(p => p.Prescriptions)
            .ThenInclude(p => p.PrescriptionMedicaments)
            .ThenInclude(pm => pm.Medicaments)
            .FirstOrDefaultAsync(p => p.IdPatient == id);

        if (pacjent == null)
        {
            return NotFound("Patient doesn't exist");
        }

        var pacjentDTO = new 
        {
            pacjent.IdPatient,
            pacjent.FirstName,
            pacjent.LastName,
            pacjent.Birthdate,
            Prescriptions = pacjent.Prescriptions.Select(p => new 
            {
                p.IdPrescription,
                p.Date,
                p.DueDate,
                PrescriptionMedicaments = p.PrescriptionMedicaments.Select(pm => new 
                {
                    pm.IdMedicament,
                    pm.Dose,
                    pm.Details
                }).ToList()
            }).ToList()
        };

        return Ok(pacjentDTO);
    }
}