using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Tarea_KatherineMurilloJimenez.Models;

namespace Tarea_KatherineMurilloJimenez.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PerrosController : ControllerBase
    {
        private static List<cls_Perros> perros = new List<cls_Perros>(); //Se cre una lista para almacenar los datos de los perros

        //Método GET que devuelve una lista completa de los perros 
        [HttpGet]
        public ActionResult<List<cls_Perros>> Get()
        {
            return Ok(perros);
        }

        //Método GET que busca un perro en la lista por su Id tyhyrthr
        [HttpGet("{id}")]
        public ActionResult<cls_Perros> Get(int id)
        {
            var perro = perros.FirstOrDefault(p => p.Id == id); //Busca el perro con el Id 
            if (perro == null)
                return NotFound("Perro no encontrado.");
            return Ok(perro);
        }

        //Método POST para agregar los datos de un nuevo perro
        [HttpPost]
        public ActionResult Post([FromBody] cls_Perros nuevoPerro)
        {
            if (perros.Any(p => p.Id == nuevoPerro.Id))  //Verifica si ya existe un perro con el mismo ID
                return BadRequest("Ya existe un perro con ese ID.");
            perros.Add(nuevoPerro); //Si no existe lo agrega a la lista
            return CreatedAtAction(nameof(Get), new { id = nuevoPerro.Id }, nuevoPerro);
        }

        //Método PUT que recibe el Id de un perro y lo actualiza en la lista
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] cls_Perros perroActualizado)
        {
            var perro = perros.FirstOrDefault(p => p.Id == id); //Busca el perro en la lista por su Id
            if (perro == null)
                return NotFound("Perro no encontrado.");
            perro.EstadoSalud = perroActualizado.EstadoSalud; //Actualiza los datos del perro 
            perro.Peso = perroActualizado.Peso;
            perro.PaisOrigen = perroActualizado.PaisOrigen;
            perro.Razas = perroActualizado.Razas;

            return NoContent();
        }

        //Método DELETE que recibe el Id de un perro y lo elimina de la lista
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var perro = perros.FirstOrDefault(p => p.Id == id); //Busca el perro en la lista por su Id
            if (perro == null)
                return NotFound("Perro no encontrado.");
            perros.Remove(perro); //Si lo encuentra lo elimina de la lista
            return NoContent();
        }

        //Método POST que recibe el Id de un perro y agrega los datos de la raza
        [HttpPost("{perroId}/razas")]
        public ActionResult AgregarRaza(int perroId, [FromBody] cls_Razas nuevaRaza)
        {
            var perro = perros.FirstOrDefault(p => p.Id == perroId); //Busca el perro por su Id
            if (perro == null)
                return NotFound("Perro no encontrado.");
            if (perro.Razas.Any(r => r.Id == nuevaRaza.Id)) //Verifica si ya existe una raza con el mismo Id 
                return BadRequest("Ya existe una raza con ese ID para este perro.");
            perro.Razas.Add(nuevaRaza); //Si no existe agrega la nueva raza a la lista de las razas del perro
            return Ok(perro);
        }

        //Método PUT recibe el Id del perro, el Id de la raza y actualiza los datos de la raza en la lista 
        [HttpPut("{perroId}/razas/{razaId}")]
        public ActionResult ActualizarRaza(int perroId, int razaId, [FromBody] cls_Razas razaActualizada)
        {
            var perro = perros.FirstOrDefault(p => p.Id == perroId);  //Busca el perro por su Id
            if (perro == null)
                return NotFound("Perro no encontrado.");
            var raza = perro.Razas.FirstOrDefault(r => r.Id == razaId); //Busca la raza por su Id dentro del perro
            if (raza == null)
                return NotFound("Raza no encontrada.");
            raza.Descripcion = razaActualizada.Descripcion; //Actualiza la descripción de la raza
            return Ok(perro);
        }

        //Método DELETE que recibe el Id del perro, el Id de la raza y la elimina de la lista
        [HttpDelete("{perroId}/razas/{razaId}")]
        public ActionResult EliminarRaza(int perroId, int razaId)
        {
            var perro = perros.FirstOrDefault(p => p.Id == perroId); //Busca el perro por su Id
            if (perro == null)
                return NotFound("Perro no encontrado.");
            var raza = perro.Razas.FirstOrDefault(r => r.Id == razaId); //Busca la raza dentro del perro por su Id
            if (raza == null)
                return NotFound("Raza no encontrada.");
            perro.Razas.Remove(raza); //Si la encuentra la elimina de la lista
            return Ok(perro);
        }      
    }
}
