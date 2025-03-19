using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;
using MongoDB.Driver;

[ApiController]
[Route("conexion")]
public class Conexion : Controller {
    [HttpGet("sql")]
    public IActionResult ListarCarrerasSql(){
       List<CarrerasSql> lista = new List<CarrerasSql>();

       SqlConnection conn = new SqlConnection(CadenasConexion.SQL_SERVER);
       SqlCommand cmd = new SqlCommand("select IdCarrera, Carrera from Carreras");
       cmd.Connection = conn;
       cmd.CommandType = System.Data.CommandType.Text;
       cmd.Connection.Open();

       SqlDataReader reader = cmd.ExecuteReader();

       while (reader.Read()){
        CarrerasSql carrera = new CarrerasSql();
        carrera.IdCarrera = reader.GetInt16("IdCarrera");
        carrera.Carrera  = reader.GetString("Carrera");

        lista.Add(carrera);
       }

       return Ok (lista);
    }

 [HttpGet("Mongo")]
    public IActionResult ListarSalonesMongoDb(){
 MongoClient client = new MongoClient(CadenasConexion.MONGO_DB);
 var db = client.GetDatabase("Practica2_Ivan");
 var collection = db.GetCollection<SalonMongo>("Salones");

 var lista = collection.Find(FilterDefinition<SalonMongo>.Empty).ToList();

        return Ok(lista); 
    }
}