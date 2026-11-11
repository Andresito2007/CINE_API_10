using API_CINE.Dtos;
using API_CINE.Models;
using API_CINE.Services;
using Microsoft.AspNetCore.Mvc;

namespace API_CINE.Controllers;

[ApiController]
[Route("api/tickets")]

public class TicketsController : ControllerBase
{
    private ITicketService _service;

    // INYECCION DE DEPENDENCIAS
    public TicketsController(ITicketService service)
    {
        _service = service;
    }

    [HttpGet]
    public ActionResult<List<Ticket>> ObtenerTodos()
    {
        return Ok(_service.ObtenerTodos()); // ok=200
    }

    [HttpGet("{id}")]
    public ActionResult<Ticket> ObtenerPorId(int id)
    {
        return Ok(_service.ObtenerPorId(id));
    }

    [HttpPost]

    public ActionResult<Ticket> Crear(TicketRequestDto dto)
    {
        Ticket creado = _service.Crear(dto);
        // devuelve el ticket creado y su ruta
        return CreatedAtAction("ObtenerPorId", new { id = creado.Id }, creado); // createdataction=201
    }

    [HttpPut("{id}")]
    public ActionResult<Ticket> Actualizar(int id, TicketRequestDto dto)
    {
        return Ok(_service.Actualizar(id, dto));
    }

    [HttpDelete("{id}")]
    public IActionResult Eliminar(int id)
    {
        _service.Eliminar(id);
        
        return NoContent();
    }
}
