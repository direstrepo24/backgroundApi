# Proyecto de Tareas Programadas con API en .NET Core

Este proyecto demuestra la creación de una aplicación en .NET Core que incluye un servicio cron para ejecutar tareas programadas automáticamente dos veces al día (a medianoche y al mediodía) y también expone una API para ejecutar tareas manualmente y consultar la próxima ejecución.

## Descripción

El servicio de tareas programadas utiliza expresiones cron para definir los tiempos de ejecución. Está diseñado para ser flexible y permite la configuración de la programación mediante variables de entorno o configuración directa en el código.

## Componentes

- **CronJobService**: Servicio en segundo plano que ejecuta tareas según una expresión cron.
- **CronJobController**: Controlador API que permite la ejecución manual de las tareas y la consulta de la próxima ejecución.

## Tecnologías Utilizadas

- ASP.NET Core 8
- Cronos – una biblioteca de terceros para manejar expresiones cron.

## Configuración

### Prerrequisitos

Asegúrate de tener instalado .NET Core 8 SDK.

### Instalación

Clona este repositorio y navega al directorio del proyecto:

```bash
git clone [URL_DEL_REPOSITORIO]
cd [NOMBRE_DEL_DIRECTORIO]
```

### Configuración de la Expresión Cron
Se puede configurar la expresión cron en Program.cs o pasarla como una variable de entorno CronSchedule.

### Ejecución del Proyecto
Para ejecutar el proyecto, utiliza el siguiente comando desde la raíz del directorio del proyecto:


```bash
dotnet run

```

### Uso de la API
La API expone dos endpoints:

GET /cronjob/run: Ejecuta la tarea programada manualmente.
GET /cronjob/next: Consulta cuándo será la próxima ejecución programada.
Puedes usar herramientas como Postman o cURL para interactuar con la API.

Ejemplo de cURL
Para ejecutar la tarea manualmente:

```bash

curl http://localhost:5000/cronjob/run

```
Para consultar la próxima ejecución:

```bash

curl http://localhost:5000/cronjob/next
```

# Contribuir
Si deseas contribuir a este proyecto, considera hacer un fork del repositorio y proponer tus cambios mediante un pull request.

# Licencia
Este proyecto está licenciado bajo la Licencia MIT. Consulta el archivo LICENSE para obtener más detalles.

## Notas Adicionales

- **Personalización**: Se deberá actualizar los placeholders como `[URL_DEL_REPOSITORIO]` y `[NOMBRE_DEL_DIRECTORIO]` con la información real del proyecto.

- **Licencia**: Incluye un archivo `LICENSE` si mencionas la licencia en el README.
