// namespace es el espacio de nombres de mi archivo con la ubi de la carpeta donde esta
namespace API_CINE.Models;

// CREAMOS LA CLASE CLIENTE (ENTIDAD)
public class Cliente
{
    // PROPIEDADEES
    public int Id { get; set; } // GET Y SET ENCAPSULANMIENTO
    public string Nombre { get; set; } = "";
    public string Dni { get; set; } = "";
    public string Email { get; set; } = "";

    // CONSTRUCTOR VACIO PA INICIALIZAR OBJETOS
    public Cliente() { }

    // CREAMOS NUESTRO OBJETO CLIENTE CON SUS PROPIEDADES
    public Cliente(int id, string nombre, string dni, string email)
    {
        Id = id;
        Nombre = nombre;
        Dni = dni;
        Email = email;
    }
}
