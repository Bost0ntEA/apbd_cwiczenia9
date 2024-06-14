using Microsoft.AspNetCore.Mvc;
using PrzygotowanieDoKolosa2.Service;

namespace PrzygotowanieDoKolosa2.Controller;
[ApiController]
public class ShipController: ControllerBase
{
    private readonly IDBService _service;

    public ShipController(IDBService service)
    {
        _service = service;
    }
    [HttpGet]
    [Route("api/")]
}