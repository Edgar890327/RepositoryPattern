# Repository

Es un proyecto para un uso sencillo de SQl server a través de Entity Framework y MongoDb.

## Installation

Aun no tengo el paquete listo, pero estoy trabajando en ello.

## Usage
Para usar el repositorio de Mongo debes seguir las siguientes instrucciones.
Agregar la configuración en el AppSettings.Json
```C#
 "MongoDbSettings": {
    "ConnectionString": "mongodb+srv://",
    "DatabaseName": "DataBase"
  }
```
En el startup agregar el siguiente código:
```C#
 using Repository.Extensions;

 public void ConfigureServices(IServiceCollection services)
 {
      services.ConfigureMongoRepository(Configuration);
 }
```
Las clases con las que se va a trabajar deben heredad de Document, en el atributo BSonCollection se escribe el nombre que va a llevar la colección, en el siguiente ejemplo, La clase se llama Person, pero la colección en mongo se va a llamar peopleTest
```C#
  [BsonCollection("peopleTest")]
  public class Person : Document
  {
      public string FirstName { get; set; }
      public string LastName { get; set; }
  }
```
La implementación se puede realizar con el siguiente código:
```C#
  using Repository.MongoRepository;
  public class ExampleController : ControllerBase
  {
      private readonly IMongoRepository<Person> _peopleRepository;
      
      public ExampleController(IMongoRepository<Person> peopleRepository)
      {
          _peopleRepository = peopleRepository;
      }

      [HttpPost]
      public async Task AddPerson(Person person)
      {
          await _peopleRepository.InsertOneAsync(person);
      }

      [HttpGet]
      public IEnumerable<string> GetPeopleData()
      {
          var people = _peopleRepository.FilterBy(
                filter => filter.FirstName != "test",
                projection => projection.FirstName
            );
          return people;
      } 
  }
```

Para mayor información puedes consultar este link
[Generic Mongo Repository pattern implemented in .NET Core](https://medium.com/@marekzyla95/mongo-repository-pattern-700986454a0e)

Para usar el repositorio de SQL es necesario ya tener creado el DBContext, para configurar el repositorio se va a hacer de la siguiente forma.

```C#
 using Repository.Extensions;

 public void ConfigureServices(IServiceCollection services)
 {
      services.ConfigureSqlRepository(DBContext);
 }
```

El repositorio se puede implementar con el siguiente código:
```C#
  using Repository.MongoRepository;
  public class ExampleController : ControllerBase
  {
      private readonly ISqlRepository _peopleRepository;
      
      public ExampleController(ISqlRepository peopleRepository)
      {
          _peopleRepository = peopleRepository;
      }

      [HttpPost]
      public async Task AddPerson(Person person)
      {
          await _peopleRepository.Create(person);
      }

      [HttpGet]
      public People GetPeopleData()
      {
          var people = _peopleRepository.Retrieve(filter => filter.FirstName != "test" );
          return people;
      } 
  }
```