
# Prueba Técnica Paga Todo.

¿Qué tal? Espero que se encuentren bien. Permítanme darles un recorrido por la aplicación y las características que contiene esta app.

# Tecnologías

Las tecnologías que elegí para este proyecto fueron:

- C#
- DotNet
- SQLServer

# Dependencias

Este proyecto cuenta con las siguientes dependencias para su funcionamiento:

- AutoMapper(v13.0.1)
- Microsoft.EntityFrameworkCore(v8.0.8)
- Microsoft.SqlServer.Server(v8.0.8)
- System.Diagnostics.Tools(v8.0.8)

# Explicación

En este proyecto decidí tomar la decisión de elegir las tecnologías anteriormente mencionadas. Con ellas, llegué al resultado esperado, pensando siempre en la calidad del código, sobre todo enfocándome en cumplir los objetivos de la prueba, así como dar unos extras en este proyecto.

Para poder trabajar con el proyecto se deben seguir los siguientes pasos:

1.- Una vez teniendo el proyecto clonado, se necesitará tener instalado Visual Studio y SQL Server. Las versiones que se usaron fueron las últimas, así como se trabajó con la versión (8.0.302) de .NET.

2.- Se necesitará que creen un usuario en SQL Server, con el SERVER_ROLE de sysadmin, así como la creación de la base de datos llamada `PagaTodo`.

3.- Luego de eso necesitaremos configurar los siguientes campos en el archivo `appsettings.json`:

| Propiedad         | Valor                                                              |
| ----------------- | ------------------------------------------------------------------ |
| SERVER_NAME | nombre_servidor |
| DATABASE_NAME | nombre_base_datos |
| USER_NAME | nombre_usuario |
| PASSWORD | tuContraseña |


4.- Ya teniendo todo listo, solo quedaría hacer las migraciones del proyecto para la generación de las tablas. Para eso se deben crear las migraciones:

- `add-migration nombre_migracion`(ese nombre es a elección del usuario, no afecta mucho, solo da a entender qué hace esta migración. En este caso sería la creación de empleados. Como recomendación, yo le puse así:  `add-migration InitializeMigration`).

- `update-database`(ese nombre es a elección del usuario, no afecta mucho, solo da a entender qué hace esta migración. En este caso sería la creación de empleados).


5.- Teniendo las migraciones listas y creada la tabla en la base de datos, solo queda ejecutar el código.


# Detalles

Este apartado es para explicar unos detalles del proyecto. Primero, configuré un apartado para los `CORS` para que no tuvieran problemas para poder consumirlo de cualquier manera, ya sea web, Postman, o el medio que ustedes crean conveniente.


## API Reference

#### Get all items

```http
  GET /api/Employee/GetAllEmployees
```

| Parameter | Type     | Description                |
| :-------- | :------- | :------------------------- |
| `pageNumber` | `integer` | **Optional**. Page |
| `pageSize` | `integer` | **Optional**.  Size of Employee |
| `nameEmployee` | `string` | **Optional**. Filter with name and lastname |

#### Get item

```http
  GET /api/Employee/GetEmployee/{employeeId }
```

| Parameter | Type     | Description                |
| :-------- | :------- | :------------------------- |
| `employeeId ` | `integer` | **Required**.  employeeId  |


#### Post item

```http
  POST /api/Employee/CreateEmployee
```

| Parameter | Type     | Description                |
| :-------- | :------- | :------------------------- |
| `firstName ` | `string` | **Required**.  firstName  |
| `lastName ` | `string` | **Required**.  lastName  |
| `salary ` | `double` | **Required**.  salary  |


#### Patch item

```http
  PATCH /api/Employee/UpdateEmployee
```

| Parameter | Type     | Description                |
| :-------- | :------- | :------------------------- |
| `employeeId ` | `integer` | **Required**.  employeeId  |
| `firstName ` | `string` | **Required**.  firstName  |
| `lastName ` | `string` | **Required**.  lastName  |
| `salary ` | `double` | **Required**.  salary  |


#### Remove item

```http
  Remove /api/Employee/DeleteEmployee/{employeeId }
```

| Parameter | Type     | Description                |
| :-------- | :------- | :------------------------- |
| `employeeId ` | `integer` | **Required**.  employeeId  |
