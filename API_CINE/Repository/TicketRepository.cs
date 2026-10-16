using API_CINE.Data;
using API_CINE.Models;

namespace API_CINE.Repository;

// REPOSITORIO PARA LAS CONSULTAS A LA BD

// IMPLEMENTA ITICKETREPOSITORY , PARA TENER LOS METODOS DEFINIDOS EN EL CONTRATO
public class TicketRepository : ITicketRepository
{
    private List<Ticket> _tickets = DatosIniciales.Tickets;

    public List<Ticket> ObtenerTodos()
    {
        return _tickets;
    }

    // USANO METODOS LINQ
    public Ticket? ObtenerPorId(int id)
    {
        return _tickets.FirstOrDefault(t => t.Id == id);
    }

    public void Agregar(Ticket ticket)
    {
        _tickets.Add(ticket);
    }

    public void Eliminar(Ticket ticket)
    {
        _tickets.Remove(ticket);
    }
}
