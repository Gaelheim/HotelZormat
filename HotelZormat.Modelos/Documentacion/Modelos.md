# Capa Modelo (HotelZormat.Modelo) 

40232840757 - 20233607

## Descripción

La capa HotelZormat.Modelo fue creada con el propósito de separar las entidades del dominio del resto de la lógica de la aplicación, siguiendo una arquitectura por capas más limpia y desacoplada.

Inicialmente, las clases de modelo (Habitación, Huésped, Usuario, Reserva, etc.) se encontraban dentro de la capa Negocio. 
Sin embargo, esta organización generaba una dependencia incorrecta, ya que la capa Datos necesitaba utilizar dichas clases para mapear la información obtenida desde la base de datos.

Por todo lo aprendido en clases sabemos que, la capa Datos no debe depender de la capa Negocio, ya que esto rompe el principio de independencia entre capas.
Para solucionar este problema todas las entidades fueron trasladadas a un proyecto independiente llamado HotelZormat.Modelo.


# Responsabilidades

La capa Modelo únicamente contiene las clases que representan los datos del sistema.

Ejemplos:

- Usuario
- Rol
- Habitación
- Huésped
- Reserva
- Estadía
- Factura
- Bitácora
- TipoHabitación

Estas clases contienen únicamente propiedades y constructores necesarios para representar la información del dominio.

La capa Modelo no contiene:

- Consultas SQL
- Reglas de negocio
- Código de interfaz gráfica
- Acceso a datos
- Validaciones complejas

**Su única responsabilidad es representar los objetos utilizados por el sistema.**


# Relación con las demás capas


La capa **Modelo** es compartida por **Negocio** y **Datos**, ya que ambas necesitan utilizar las mismas entidades.

- La capa Datos crea y devuelve objetos Modelo a partir de la información almacenada en SQL Server.
- La capa Negocio utiliza esas entidades para aplicar las reglas del sistema.
- La interfaz de usuario consume los resultados procesados por la capa Negocio.

De esta forma se evita que una capa dependa de otra cuando únicamente necesita conocer la estructura de los datos.


# Motivo del cambio

Las clases de modelo fueron trasladadas desde la capa **Negocio** hacia un proyecto independiente porque la capa **Datos** necesitaba utilizarlas para mapear la información obtenida desde la base de datos.

Mantener los modelos dentro de la capa Negocio obligaba a que la capa Datos dependiera de Negocio, lo cual viola el principio de separación de responsabilidades de una arquitectura por capas.

