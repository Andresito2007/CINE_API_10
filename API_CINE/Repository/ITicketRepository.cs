using API_CINE.Models;

namespace API_CINE.Repository;

// CONTRATO DEL REPOSITORIO PARA EL SERVICIO : SOLO ACCESO A  MANUPILAR DATOS DE LA BD
public interface ITicketRepository
{
    List<Ticket> ObtenerTodos();
    Ticket? ObtenerPorId(int id);
    void Agregar(Ticket ticket);
    void Eliminar(Ticket ticket);
}
