using API_CINE.Dtos;
using API_CINE.Exceptions;
using Microsoft.AspNetCore.Diagnostics; // namespace de manejo de errores ( IexceptionHanler)

namespace API_CINE.Middleware;

// (GUARDIAN) TODA EXCEPCION QUE SE LANCE LLEGA AQUI 
public class ManejadorExcepciones : IExceptionHandler{
    // ILOGGER PA ESCRIBIR MENSAJES EN CONSOLA Y SE INYECTA SOLO POR .NET
    private ILogger<ManejadorExcepciones> _logger; 
    
    public ManejadorExcepciones(ILogger<ManejadorExcepciones> logger)
    {
        _logger = logger;
    }
    // ASYNC PARA REALIZAR OPERACIONES ASINCRONAZ , ASI PUEDEN ATENDER OTRAS PETIS SIN AFECTAR EL HILO
    public async ValueTask<bool> TryHandleAsync(HttpContext context, Exception exception, CancellationToken cancellationToken)
    {
        int estado;
        string error;
        string mensaje = exception.Message;
        
        switch (exception)
        {
            case NoEncontradoException:
                estado = StatusCodes.Status404NotFound; // status code la clase que tiene los estados http
                error = "NO ENCONTRADO ESTIMADO APICINITO";
                break;
            case ReglaNegocioException:
                estado = StatusCodes.Status400BadRequest;
                error = "SOLICITID INVALIDA ESTIMADO APICINITO";
                break;
            case ConflictoException:
                estado = StatusCodes.Status409Conflict;
                error = "CONFLICTO ENCONTRADO ESTIMADO APICINITO";
                break;
            default:
                // POR SI HAY UN ERROR QUE NO ESPERABAMOS , LO GUARDAMOS EN EL LOG PA VERLO EN EL VISUAL
                _logger.LogError(exception, "OCURRIO UN BUG ESTIMADO APICINITO");
                estado = StatusCodes.Status500InternalServerError;
                error = "ERROR INTERNO DE CINE API";
                mensaje = "OCURRIO UN ERROR INSEPERADO APICNITO , INTENTALO MAS TARDE";
                break;
        }

        // ARMAMOS LA RESPUESTA CON NUESTRO DTO DE SALIDA
        ErrorResponse respuesta = new ErrorResponse
        {
            Estado = estado,
            Error = error,
            Mensaje = mensaje,
            Ruta = context.Request.Path // SOLO LA PETICION CON PATH
        };
        // LE ASGINAMOS A LA VARIABLE ESTADO  Y LA MANDAMOS COMO JSON
        context.Response.StatusCode = estado;
        // esperamos la operacion con await
        await context.Response.WriteAsJsonAsync(respuesta, cancellationToken);
        return true;
    }
}
