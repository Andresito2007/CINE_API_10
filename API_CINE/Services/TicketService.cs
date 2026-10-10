using API_CINE.Data;
using API_CINE.Dtos;
using API_CINE.Exceptions;
using API_CINE.Models;
using API_CINE.Repository;

namespace API_CINE.Services;

// NUESTRA LOGICA DE NEGOCIO

// IMPLEMENTAMOS ITICKETSERVICE
public class TicketService : ITicketService
{
    // SACAMOS DATOS DEL REPOSITORIO (INYECCION DE DEPENDENCIAS)
    private ITicketRepository _ticketRepository;

    public TicketService(ITicketRepository ticketRepository)
    {
        _ticketRepository = ticketRepository;
    }
    public List<Ticket> ObtenerTodos()
    {
        return _ticketRepository.ObtenerTodos();
    }
    public Ticket ObtenerPorId(int id)
    {
        Ticket? ticket = _ticketRepository.ObtenerPorId(id); // PUEDE SER NULL ?

        if (ticket == null)
        {
            throw new NoEncontradoException($"NO SE ECONTRO EL TICKET CON ID DE {id}");
        }
        return ticket;
    }
    public Ticket Crear(TicketRequestDto dto)
    {
        ValidarDatos(dto, idActual: 0);

        List<Ticket> tickets = _ticketRepository.ObtenerTodos();

        // EL ID ES EL MAYOR QUE HAY + 1 (O 1 SI LA LISTA ESTA VACIA)
        Ticket nuevo = new Ticket{
            // count cuenta cuantos tickes hay en la lista , si es 0 le ponemos 1 , si no le ponemos el  ticket maximo id + 1
            Id = tickets.Count == 0 ? 1 : tickets.Max(t => t.Id) + 1,Estado = "Activo"
            // => lambda
            };

        CopiarDatos(dto, nuevo);

        _ticketRepository.Agregar(nuevo);

        return nuevo;
    }
    public Ticket Actualizar(int id, TicketRequestDto dto)
    {
        Ticket ticket = ObtenerPorId(id);

        ValidarDatos(dto, idActual: id);
        CopiarDatos(dto, ticket);

        return ticket;
    }
    public void Eliminar(int id)
    {
        Ticket ticket = ObtenerPorId(id);
        _ticketRepository.Eliminar(ticket);
    }
    // PRIVATE PORQUE SOLO SE USA DENTRO DE ESTA CLASE
    private void ValidarDatos(TicketRequestDto dto, int idActual)
    {
        // ANY DEVUELVE TRUE SI SE CUMPLE LA CONDICION
        if (!DatosIniciales.Clientes.Any(c => c.Id == dto.ClienteId))
        {
            throw new ReglaNegocioException($"NO EXISTE EL CLIENTE CON ID {dto.ClienteId}");
        }

        if (!DatosIniciales.Peliculas.Any(p => p.Id == dto.PeliculaId))
        {
            throw new ReglaNegocioException($"NO EXISTE LA PELICULA CON ID   {dto.PeliculaId}");
        }
        // EL ASIENTO ESTA OCUPADO SI YA HAY UN TICKET ACTIVO CON LA MISMA PELICULA , FECHA , SALA Y ASIENTO
        // ORDINALIGNORECASE PA QUE "f7" Y "F7" SEAN IGUALES
        bool asientoOcupado = _ticketRepository.ObtenerTodos().Any(t =>
            t.Id != idActual &&
            t.Estado == "Activo" &&
            t.PeliculaId == dto.PeliculaId &&
            t.FechaFuncion == dto.FechaFuncion &&
            t.Sala.Equals(dto.Sala, StringComparison.OrdinalIgnoreCase) &&
            t.Asiento.Equals(dto.Asiento, StringComparison.OrdinalIgnoreCase));

        if (asientoOcupado)
        {
            throw new ConflictoException($"EL ASIENTO {dto.Asiento.ToUpper()} YA ESTA VENDIDO PARA OTRA FUNCION.");
        }
    }
    // LE LLEVA LOS DATOS DEL DTO AL NEW TICKET
    private static void CopiarDatos(TicketRequestDto dto, Ticket ticket)
    {
        ticket.ClienteId = dto.ClienteId;
        ticket.PeliculaId = dto.PeliculaId;
        ticket.FechaFuncion = dto.FechaFuncion!.Value;
        ticket.Sala = dto.Sala;
        ticket.Asiento = dto.Asiento.ToUpper(); // PASARL EN MAYUSCULA
        ticket.Precio = dto.Precio;
    }
}
