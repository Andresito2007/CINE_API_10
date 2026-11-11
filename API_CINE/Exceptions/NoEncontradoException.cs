namespace API_CINE.Exceptions;

// ERROR 404  ( NO ENCONTRADO )
public class NoEncontradoException : Exception
{
    // RECIBE EL MENSAJE Y SE LO PASA AL PADRE (EXCEPTION) CON BASE
    public NoEncontradoException(string mensaje) : base(mensaje) { }
}
