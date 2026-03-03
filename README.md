# Sistema de Nómina - ASP.NET Core MVC

## Descripción
Sistema de gestión de nómina desarrollado en ASP.NET Core MVC con C#, Entity Framework Core y SQL Server.

## Tecnologías
- ASP.NET Core 10 MVC
- Entity Framework Core
- SQL Server Express
- Bootstrap 5
- Git / GitHub

## Módulos
- Autenticación (Login/Logout con hash SHA256)
- Empleados (CRUD completo)
- Departamentos (CRUD completo)
- Salarios (con auditoría)

## Instrucciones de ejecución
1. Clonar el repositorio:
   git clone https://github.com/Andrewnd89/SistemaNomina.git
2. Abrir SistemaNomina.sln en Visual Studio
3. Configurar la cadena de conexión en appsettings.json
4. Ejecutar migraciones:
   dotnet ef database update
5. Presionar F5 para ejecutar

## Ramas
- main: rama estable
- develop: rama de integración
- feature/empleados: módulo empleados
- feature/departamentos: módulo departamentos
- feature/salarios: módulo salarios
- feature/autenticacion: módulo autenticación

## Equipo
- Eddy Quishpe