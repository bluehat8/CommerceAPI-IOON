# Guía de Configuración

A continuación, se explica cómo configurar y ejecutar la API de comercio en máquina local.

## 1. Requisitos Previos

- **.NET 8 SDK**: Se debe tener instalado .NET 8 SDK en tu nodo. Se puede verificar la instalación con el siguiente comando:

  ```bash
  dotnet --version

-**SQL Server**: La API usa SQL Server como base de datos. Es primordial tener una instancia de SQL Server en ejecución, pero se puede usar una DB en la nube.

 
## 1. Configuración de la base de datos

- **Crear la base de datos**: Se debe crear una nueva en SQL Server.
- **Configurar la conexión a la base de datos**: En el archivo appsettings.json, se debe actualizar la cadena de conexión de la base de datos.
- **Ejecutar las migraciones**: Luego de terminar las configuraciones, se debe ejecutar el siguiente comando en la consola de administrador de paquetes para aplicar todas las migraciones pendientes:

  ```bash
  dotnet ef database update

-**Ejecutar la api**:  Finalmente, se debe usar el siguiente comando para ejecutar la API:

  ```bash
  dotnet run
