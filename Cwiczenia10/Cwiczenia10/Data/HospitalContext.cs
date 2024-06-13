using Cwiczenia10.Models;
using Microsoft.EntityFrameworkCore;

namespace Cwiczenia10.Data;

public class HospitalContext: DbContext
{
    public DbSet<Doctor> Doctors { get; set; }
    public DbSet<Patient> Patients  { get; set; }
    public DbSet<Medicament> Medicaments { get; set; }
    public DbSet<Prescription> Prescriptions  { get; set; }
    public DbSet<Prescription_Medicament> PrescriptionMedicaments  { get; set; }

    public HospitalContext(DbContextOptions options) : base(options)
    {
    }

    protected HospitalContext()
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Doctor>().HasData(
            new Doctor() { IdDoctor = 1, FirstName = "Jan", LastName = "Pierog", Email = "Jan@gg.com" }
        );
        
        modelBuilder.Entity<Patient>().HasData(
            new Patient(){IdPatient = 1,FirstName = "Kaluch",LastName = "Kapszztyniarz",Birthdate = new DateTime(2000,10,1)}
            );
        
        modelBuilder.Entity<Medicament>().HasData(
            new Medicament(){IdMedicament = 1,Name = "Holochomikonafeloftalenion",Description = "boli", Type = "lecznicze"}
            );
        modelBuilder.Entity<Prescription>().HasData(
            new Prescription(){IdPrescription = 1,
                Date = DateTime.Today,
                DueDate = DateTime.Now.AddDays(5),
                idPatient = 1,
                IdDoctor = 1
            });
        modelBuilder.Entity<Prescription_Medicament>().HasData(
            new Prescription_Medicament
            {
                IdPrescription = 1,
                IdMedicament = 1,
                Dose = 1,
                Details = "jest sobie krowka"
            }
        );
    }

}