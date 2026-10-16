# CINE API — Sistema de Venta de Entradas de Cine
NUESTRO API EN .NET DE DESARROLLO WEB SDK10


# Integrantes del equipo
- Integrante 1: Ochupe Ayma Estefano Alessandro
- Integrante 2: Choqque Mejia Andres David
- Integrante 3: Tomayquispe Amasifuen Rodrigo Isaias

# Objetivo general

Desarrollar una API RESTful para gestionar la venta de entradas de cine, aplicando buenas prácticas de programación en C#, separación de responsabilidades, validación de datos y manejo de excepciones.

# Tecnologías utilizadas

- Lenguaje de programación: C#
- Framework: ASP.NET Core
- Versión objetivo de .NET: .NET 10
- Formato de intercambio de datos: JSON
- Herramienta de pruebas: Postman
- Control de versiones: Git y GitHub

# Gestión de tickets

El módulo de tickets implementa las siguientes operaciones:

- Registrar un nuevo ticket.
- Consultar todos los tickets.
- Consultar un ticket por su identificador.
- Actualizar un ticket existente.
- Eliminar un ticket.
- Generar identificadores automáticamente.
- Establecer el estado inicial del ticket como `Activo`.
- Validar los datos de entrada.
- Verificar la existencia del cliente y de la película asociados.
- Comprobar la disponibilidad del asiento para una misma película, función y sala.

# Estructura del proyecto

CINE_API/
├── Controllers/
│   └── TicketsController.cs
├── Data/
│   └── DatosIniciales.cs
├── Dtos/
│   ├── ErrorResponse.cs
│   └── TicketRequestDto.cs
├── Exceptions/
│   ├── ConflictoException.cs
│   ├── NoEncontradoException.cs
│   └── ReglaNegocioException.cs
├── Middleware/
│   └── ManejadorExcepciones.cs
├── Models/
│   ├── Cliente.cs
│   ├── Pelicula.cs
│   └── Ticket.cs
├── Services/
│   ├── ITicketService.cs
│   └── TicketService.cs
├── Program.cs
├── appsettings.json
├── appsettings.Development.json
└── CINE_API.csproj


# Arquitectura del sistema

El proyecto utiliza una arquitectura por capas para mantener separadas las responsabilidades de cada componente.

          Solicitud HTTP
                |
                v
       TicketsController
                |
                v
         ITicketService
                |
                v
          TicketService
                |
                v
         DatosIniciales
                |
                v
       Colecciones en memoria
# Descripción de los componentes

- Controllers: reciben las solicitudes HTTP y devuelven las respuestas.
- Services: contienen la lógica de negocio y las operaciones relacionadas con los tickets.
- Models: representan las entidades Cliente, Pelicula y Ticket.
- DTOs: definen los datos que se reciben mediante las solicitudes y las estructuras de respuesta.
- Data: contiene los datos iniciales y las colecciones utilizadas durante la ejecución.
- Exceptions: contiene excepciones personalizadas para representar distintos errores.
- Middleware: centraliza el manejo de excepciones.
- Program.cs: configura los servicios, los controladores y el middleware de la aplicación.

# Endpoints implementados

La API utiliza la ruta base `/api/tickets` para las operaciones del módulo de tickets.

| Método HTTP | Endpoint | Descripción |

| GET | `/api/tickets` | Obtener todos los tickets |
| GET | `/api/tickets/{id}` | Obtener un ticket por su identificador |
| POST | `/api/tickets` | Registrar un nuevo ticket |
| PUT | `/api/tickets/{id}` | Actualizar un ticket existente |
| DELETE | `/api/tickets/{id}` | Eliminar un ticket |

# Códigos HTTP

Las operaciones utilizan respuestas HTTP según el resultado de la solicitud. Entre los códigos previstos para las operaciones exitosas se encuentran:

- `200 OK`: consulta o actualización exitosa.
- `201 Created`: creación de un ticket.
- `204 No Content`: eliminación exitosa.

Las solicitudes inválidas y los errores de negocio deben verificarse mediante las pruebas funcionales.

# Ejemplo de solicitud

Para registrar un ticket, se utiliza el endpoint:

POST /api/tickets
Content-Type: application/json

Ejemplo de cuerpo JSON:

{
  "clienteId": 1,
  "peliculaId": 1,
  "fechaFuncion": "2026-11-15T19:30:00",
  "sala": "Sala 1",
  "asiento": "F7",
  "precio": 20.00
}

Los identificadores deben corresponder a clientes y películas existentes en los datos iniciales. Además, el asiento solicitado debe encontrarse disponible.

# Validaciones y reglas de negocio

El DTO `TicketRequestDto` utiliza anotaciones para validar los datos recibidos.

Entre las reglas definidas se encuentran:

- El identificador del cliente debe ser mayor que cero.
- El identificador de la película debe ser mayor que cero.
- La fecha de la función es obligatoria.
- La sala debe tener entre 3 y 20 caracteres.
- El asiento debe cumplir el formato definido para filas y números.
- El precio debe encontrarse entre S/ 5 y S/ 50.

Adicionalmente, el servicio verifica que el cliente y la película existan y comprueba que no se registre un asiento duplicado para la misma película, función y sala.

# Manejo de excepciones

La aplicación cuenta con un middleware de manejo centralizado de excepciones y clases personalizadas:

- `NoEncontradoException`: representa situaciones en las que no se encuentra un recurso.
- `ReglaNegocioException`: representa incumplimientos de las reglas de negocio.
- `ConflictoException`: representa conflictos, como intentar reservar un asiento ocupado.

El middleware procesa las excepciones contempladas en la aplicación y genera las respuestas de error correspondientes.

# Instalación y ejecución - requisitos previos

- SDK de .NET compatible con el proyecto.
- Visual Studio o Visual Studio Code.
- Git, si se descarga el repositorio desde GitHub.
- Postman para realizar pruebas de los endpoints.

# Restaurar dependencias

Desde la carpeta que contiene el archivo `CINE_API.csproj`, ejecutar:

```bash
dotnet restore
```

# Compilar el proyecto

```bash
dotnet build
```

# Ejecutar la aplicación

```bash
dotnet run
```

La consola mostrará las direcciones locales en las que se ejecuta la aplicación. Utilizar la dirección correspondiente para enviar solicitudes desde Postman.

# Pruebas funcionales

Se utilizará Postman para verificar el funcionamiento de los endpoints implementados.

Las pruebas contemplan:

- Consultar todos los tickets.
- Registrar un ticket válido.
- Consultar un ticket por ID.
- Actualizar un ticket.
- Eliminar un ticket.
- Intentar consultar un ticket inexistente.
- Enviar solicitudes con datos inválidos.
- Intentar registrar un asiento ocupado.
- Utilizar identificadores de clientes o películas inexistentes.

Las capturas de las solicitudes y sus respuestas se incorporarán cuando las pruebas hayan sido ejecutadas y verificadas.
