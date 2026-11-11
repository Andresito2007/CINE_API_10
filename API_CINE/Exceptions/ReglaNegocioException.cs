namespace API_CINE.Exceptions;

// ERROR 400 ( REGLA DE NEGOCIO )
public class ReglaNegocioException : Exception
{
    // RECIBE EL MENSAJE Y SE LO PASA AL  CONSTRUCTOR PADRE (EXCEPTION) CON BASE
    public ReglaNegocioException(string mensaje) : base(mensaje) { }
}
