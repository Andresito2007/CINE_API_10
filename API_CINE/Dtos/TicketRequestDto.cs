
using System.ComponentModel.DataAnnotations;

namespace API_CINE.Dtos;

// DTO (DATA TRANSFER OBJECT)

public class TicketRequestDto
{
    [Range(1, int.MaxValue, ErrorMessage = "EL ID DEL CLIENTE ES OBLIGATORIO Y DEBE SER MAYOR A 0")]
    public int ClienteId { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "EL ID DE LA PELÍCULA ES OBLIGATORIO Y DEBE SER MAYOR A 0")]
    public int PeliculaId { get; set; }

    [Required(ErrorMessage = "LA FECHA DE LA FUNCIÓN ES OBLIGATORIA")]
    public DateTime? FechaFuncion { get; set; } // FORAZMOS EL ? PA QUE REQUIRED SE ACTIVE

    [Required(ErrorMessage = "LA SALA ES OBLIGATORIA")]

    [StringLength(20, MinimumLength = 3, ErrorMessage = "LA SALA DEBE TENER ENTRE 3 Y 20 CARACTERES")]
    public string Sala { get; set; } = "";

    [Required(ErrorMessage = "EL ASIENTO ES OBLIGATORIO.")]
    [RegularExpression("^[A-La-l]([1-9]|1[0-9]|20)$", ErrorMessage = "EL ASIENTO DEBE TENER UNA FILA (A-L) Y UN NÚMERO (1-20). EJEMPLO: F7.")]
    public string Asiento { get; set; } = "";

    [Range(5, 50, ErrorMessage = "EL PRECIO DEBE ESTAR ENTRE 5 Y 50 SOLES.")]
    public decimal Precio { get; set; }
}
