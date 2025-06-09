NOTA: TODAS LAS KEYS ESTAN DESHABILITADAS, FUERON UNICAMENTE INCLUIDAS CUANDO EL ENTORNO DE PRUEBAS ESTABA ACTIVO, POR LO QUE PARA EJECUTAR LA BASE DE DATOS, OAUTH, ETC, DEBERA GENERAR SUS PROPIAS KEYS. EL PROYECTO FUE DISEÑADO EXCLUSIVAMENTE PARA EFECTOS DE EJEMPLO PARA UN CURSO UNIVERSITARIO, POR LO QUE NO POSEE LAS MEJORES PRACTICAS, SIN EMBARGO USA EL MODELO N-TIER, SEPARANDO LA INTERFAZ DE USUARIO DE LA LOGICA DE NEGOCIO CON ESTA APIREST, LA FINALIDAD ES DEMOSTRAR LA CONEXION OAUTH, ENVIO DE CORREOS POR LOGIN, Y UN EJEMPLO DE GESTOR DE VENTAS.

## Descripción General del Proyecto
Este proyecto es un sistema de gestión de inventarios diseñado como ejemplo práctico para un curso universitario. Su objetivo principal es demostrar la implementación de funcionalidades comunes en aplicaciones empresariales, tales como la administración de productos, seguimiento de historial, ajustes de inventario, gestión de caja y ventas.

El sistema sigue una arquitectura de N capas, lo que permite una clara separación entre la presentación (interfaz de usuario), la lógica de negocio y el acceso a datos. Esto facilita la mantenibilidad y escalabilidad del proyecto.


Date un tour del sistema:
[![Ask DeepWiki](https://deepwiki.com/badge.svg)](https://deepwiki.com/PENDRAGON2121/GestionDeInventarios_ApiRest)

## Estructura del Proyecto
El proyecto está organizado en los siguientes módulos principales:

*   **`GestionDeInventarios.Model`**: Contiene las clases que representan las entidades de datos del sistema (por ejemplo, Usuario, Producto, Venta). Estas clases se utilizan para transferir datos entre las diferentes capas de la aplicación.
*   **`GestioDeInventario.DA` (Capa de Acceso a Datos)**: Responsable de la interacción directa con la base de datos. Incluye la lógica para realizar operaciones CRUD (Crear, Leer, Actualizar, Eliminar) sobre los datos del inventario y otras entidades.
*   **`GestionDeInventario.BL` (Capa de Lógica de Negocio)**: Contiene las reglas de negocio y la lógica central de la aplicación. Coordina las operaciones entre la capa de presentación/API y la capa de acceso a datos. Aquí se procesan los datos y se toman decisiones basadas en los requisitos funcionales.
*   **`GestionDeInventario.SI` (Capa de Interfaz de Servicio/API)**: Expone la funcionalidad del sistema como un servicio API REST. Permite que la interfaz de usuario (UI) u otras aplicaciones cliente interactúen con la lógica de negocio de forma desacoplada.
*   **`GestorDeInventarios.UI` (Capa de Interfaz de Usuario)**: Es la aplicación web con la que interactúan los usuarios finales. Se comunica con la capa de API (`GestionDeInventario.SI`) para obtener y enviar datos, presentando la información de manera amigable.

Esta estructura modular facilita el desarrollo, mantenimiento y la posibilidad de realizar pruebas de forma independiente en cada componente.

## Configuración y Puesta en Marcha
Para configurar y ejecutar este proyecto en su entorno local, siga los siguientes pasos:

**1. Configuración de la Base de Datos:**
   - El archivo `README.md` incluye un script SQL bajo la sección "SQL SERVER USER SCRIPT".
   - Ejecute este script en su instancia de SQL Server para crear las tablas necesarias para el funcionamiento del sistema.
   - Asegúrese de que su base de datos esté accesible y configúrela adecuadamente en los archivos de configuración del proyecto (ver punto 2).

**2. Generación de Claves y Cadenas de Conexión:**
   - **Importante:** Como se menciona en la nota inicial, todas las claves, secretos y cadenas de conexión (para la base de datos, servicios OAuth como Google, configuración de correo electrónico, etc.) han sido deshabilitadas o eliminadas del código fuente por razones de seguridad y por ser un proyecto de demostración.
   - **Deberá generar sus propias claves y credenciales** para estos servicios.
   - Busque los archivos de configuración del proyecto (por ejemplo, `appsettings.json` en los proyectos `GestionDeInventario.SI` y `GestorDeInventarios.UI`, o donde se definan las configuraciones de conexión y servicios) y actualice las secciones correspondientes con sus propias claves. Sin esto, el proyecto no funcionará correctamente.

**3. Restauración de Dependencias:**
   - Este es un proyecto .NET. Abra la solución (`.sln`) en Visual Studio o utilice el comando `dotnet restore` en la línea de comandos para descargar e instalar todas las dependencias NuGet necesarias.

**4. Compilación y Ejecución:**
   - **API (`GestionDeInventario.SI`):**
     - Configure este proyecto para que se ejecute primero o asegúrese de que esté en ejecución antes de iniciar la UI.
     - Puede ejecutarlo desde Visual Studio o usando el comando `dotnet run` dentro del directorio del proyecto `GestionDeInventario.SI`.
   - **Interfaz de Usuario (`GestorDeInventarios.UI`):**
     - Una vez que la API esté en funcionamiento y las configuraciones (especialmente la URL de la API) estén correctamente establecidas en la UI, puede ejecutar este proyecto.
     - Puede ejecutarlo desde Visual Studio o usando el comando `dotnet run` dentro del directorio del proyecto `GestorDeInventarios.UI`.

**Nota Adicional:**
   - Revise los archivos de configuración (`appsettings.json`, `launchSettings.json`, etc.) en cada proyecto para ajustar URLs, puertos u otras configuraciones específicas de su entorno si es necesario.

## Uso de la API
El proyecto incluye una API REST (`GestionDeInventario.SI`) que expone la lógica de negocio y permite la interacción con el sistema de gestión de inventarios.

**Funcionalidades Principales Expuestas por la API:**
La API proporciona endpoints para gestionar las siguientes entidades y procesos:
*   **Usuarios:** Autenticación, registro y gestión de datos de usuarios.
*   **Inventario:** Creación, consulta, actualización y eliminación de productos en el inventario.
*   **Ajustes de Inventario:** Registro y consulta de ajustes manuales de stock.
*   **Gestión de Caja:** Operaciones relacionadas con la apertura y cierre de caja.
*   **Ventas:** Registro y consulta de transacciones de venta.

**Autenticación:**
Como se mencionó anteriormente, el sistema está configurado para utilizar autenticación (potencialmente OAuth). Cualquier cliente que consuma la API deberá implementar el flujo de autenticación correspondiente para acceder a los endpoints protegidos. Deberá configurar sus propias credenciales de cliente OAuth en los archivos de configuración de la API y del cliente que la consuma.

**Consumo de la API:**
Puede interactuar con la API utilizando herramientas como Postman, cURL, o integrándola en aplicaciones cliente (como es el caso de `GestorDeInventarios.UI`).
La URL base de la API dependerá de cómo la configure en su entorno local (ver `launchSettings.json` en el proyecto `GestionDeInventario.SI`). Generalmente será algo como `https://localhost:<puerto>/api/`.

Por ejemplo, para obtener el listado de inventarios, podría existir un endpoint como `GET /api/Inventario`. Para registrar una nueva venta, podría ser un `POST /api/Ventas`. Se recomienda revisar el código de los controladores en `GestionDeInventario.SI/Controllers` para conocer los endpoints exactos, los métodos HTTP que soportan y los datos que esperan y devuelven.

## SQL SERVER USER SCRIPT

Para Crear las tablas

```sql
CREATE TABLE Users (
    ID INT PRIMARY KEY IDENTITY(1,1),
    OauthID NVARCHAR(50) NULL,
    Name NVARCHAR(100) NOT NULL,
    Email NVARCHAR(100) NOT NULL,
    Password NVARCHAR(100) NULL,
    Role INT NOT NULL,
    LoginAttempts INT DEFAULT 0,
    IsBlocked BIT DEFAULT 0,
    BlockedUntil DATETIME NULL
);

CREATE TABLE Inventarios (
    Id INT PRIMARY KEY IDENTITY,
    Nombre NVARCHAR(100),
    Categoria INT,
    Cantidad INT,
    Precio DECIMAL(10, 2),
);

CREATE TABLE HistorialDeInventario (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    InventarioId INT NOT NULL,
    Usuario NVARCHAR(100) NOT NULL,
    Fecha DATETIME NOT NULL,
    TipoModificacion INT NOT NULL
);

CREATE TABLE AjusteDeInventario (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Id_Inventario INT NOT NULL,
    CantidadActual INT NOT NULL,
    Ajuste INT NOT NULL,
    Tipo INT NOT NULL,
    Observaciones NVARCHAR(MAX) NOT NULL,
    UserId Nvarchar(255) NOT NULL,
    Fecha DATETIME NOT NULL
);

CREATE TABLE Caja (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    UserId NVARCHAR(100) NOT NULL,
    FechaDeInicio DATETIME NOT NULL,
    FechaDeCierre DATETIME NULL,
    Estado INT NOT NULL
);

CREATE TABLE Ventas (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    NombreCliente NVARCHAR(100) NOT NULL,
    Fecha DATETIME NOT NULL,
    TipoDePago INT NOT NULL,
    Total DECIMAL(18, 2) NOT NULL,
    SubTotal DECIMAL(18, 2) NOT NULL,
    PorcentajeDescuento INT NOT NULL,
    MontoDescuento DECIMAL(18, 2) NOT NULL,
    UserId NVARCHAR(100) NOT NULL,
    Estado INT NOT NULL,
    IdAperturaDeCaja INT NOT NULL
);

CREATE TABLE VentaDetalle (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Id_Venta INT NOT NULL,
    Id_Inventario INT NOT NULL,
    Cantidad INT NOT NULL,
    Precio DECIMAL(18, 2) NOT NULL,
    Monto DECIMAL(18, 2) NOT NULL,
    MontoDescuento DECIMAL(18, 2) NOT NULL,
);


INSERT INTO Users(OauthID, Name, Email, Password, Role, LoginAttempts, IsBlocked, BlockedUntil)
VALUES (NULL, 'Administrador', 'PPGR.GestorDeInventario.2024@gmail.com', 'Nuevo123*', 1, 0, 0, NULL);

  
```
