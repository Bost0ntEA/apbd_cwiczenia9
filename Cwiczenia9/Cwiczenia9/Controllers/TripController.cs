using Cwiczenia9.Data;
using Cwiczenia9.DTOs;
using Cwiczenia9.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Cwiczenia9.Controllers;
[Route("api/[controller]")]
[ApiController]
public class TripController: ControllerBase
{
    private readonly Cwiczenia9apbdContext _context;

    public TripController(Cwiczenia9apbdContext context)
    {
        _context = context;
    }
    
     [HttpGet]
    [Route("api/trips")]
    public async Task<ActionResult> GetTrips(int page = 1, int pageSize = 10)
    {

        var ileTripow = await _context.Trips.CountAsync();
        
        var trips = await _context.Trips
            .Include(trip => trip.IdCountries)
            .Include(trip => trip.ClientTrips).ThenInclude(clientTrip => clientTrip.IdClientNavigation)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        var odp = new
        {
            PageNum = page,
            PageSize = pageSize,
            AllPages = Math.Ceiling(ileTripow / (double)pageSize),
            Trips = trips.Select(trip => new
            {
                trip.Name,
                trip.Description,
                trip.DateFrom,
                trip.DateTo,
                trip.MaxPeople,
                Countries = trip.IdCountries.Select(c => new { c.Name }),
                Clients = trip.ClientTrips.Select(ct => new 
                {
                    ct.IdClientNavigation.FirstName,
                    ct.IdClientNavigation.LastName
                })
            }).OrderByDescending(t => t.DateFrom)
        };

        return Ok(odp);
    }

    [HttpDelete]
    [Route("api/clients/{idClient:int}")]
    public async Task<ActionResult> DeleteClient(int idClient)
    {
        var klient = await _context.Clients.FindAsync(idClient);
        if (klient is null)
        {
            return NotFound($"Klient z id: {idClient} nie istnieje");
        }
        
        var existsTrip = await _context.ClientTrips.AnyAsync(x => x.IdClient == idClient);
        if (existsTrip)
        {
            return NotFound($"Klient z id: {idClient} nie ma żadnych wycieczek");
        }

        _context.Clients.Remove(klient);
        await _context.SaveChangesAsync();

        return Ok($"Klient z id: {idClient}  został usunięty pomyślnie");
    }
    
    [HttpPost]
    [Route("api/trips/{idTrip}/clients")]
    public async Task<ActionResult> PostClient(DaneDTOs clientDTO)
    {
        var czyIstnieje = await _context.Clients.AnyAsync(x => x.Pesel == clientDTO.Pesel);
        if (czyIstnieje)
        {
            return NotFound($"Klient z podanym peselem nie istnieje");
        }

        czyIstnieje = await _context.ClientTrips.AnyAsync(x =>
            x.IdClientNavigation.Pesel == clientDTO.Pesel && x.IdTrip == clientDTO.IdTrip);
        if (czyIstnieje)
        {
            return NotFound($"Klient z podanym peselem jest juz na wycieczce");
        }

        czyIstnieje = await _context.Trips.AnyAsync(x =>
            x.IdTrip == clientDTO.IdTrip && x.DateFrom > DateTime.Now);
        if (czyIstnieje)
        {
            return NotFound("Nie mozna zapisac sie na tą wyciczke");
        }

        Client addClient = new Client()
        {
            FirstName = clientDTO.FirstName,
            LastName = clientDTO.LastName,
            Email = clientDTO.Email,
            Telephone = clientDTO.Telephone,
            Pesel = clientDTO.Pesel
        };
        
        _context.Add(addClient);
        var trip = await _context.Trips.FindAsync(clientDTO.IdTrip);

        _context.Add(new ClientTrip()
        {
            IdTrip = clientDTO.IdTrip,
            IdClientNavigation = addClient,
            IdTripNavigation = trip,
            PaymentDate = clientDTO.PaymentDate,
            RegisteredAt = DateTime.Now
        });
        
        await _context.SaveChangesAsync();

        return Ok($"Client have been added");
    }
}