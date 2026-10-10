namespace API_CINE.Exceptions;

// ERROR 400 REGLA DE NEGOCIO SI SE ROMPE ALGUNA REGLA DE NEGOCIO COMO UN ASIENTO YA VENDIDO
public class ReglaNegocioException : Exception
{
    // RECIBE EL MENSAJE Y SE LO PASA AL  CONSTRUCTOR PADRE (EXCEPTION) CON BASE
    public ReglaNegocioException(string mensaje) : base(mensaje) { }
}
