using definitiva.Interfaces;
using definitiva.Models;
using Microsoft.AspNetCore.Mvc;

namespace definitiva.Controllers
{
    public class PersonasController : Controller
    {
        public readonly Ipersonas _ipersonas;

        public PersonasController(Ipersonas ipersonas)
        {
            _ipersonas = ipersonas;
        }

        [HttpGet]
        [Route("API/Personas")]
        public async Task<ActionResult> Index()
        {
            var result_final = await _ipersonas.GetPersonas();
            return Ok(result_final);
        }

        [HttpPut]
        [Route("API/Personas/{id_Personas}")]
        public async Task<IActionResult> Put(int id_Personas, [FromBody] PersonasModels updatePersonas)
        {
            try
            {
                var result = await _ipersonas.ActualizarPersonas(id_Personas, updatePersonas);

                if (result == null)
                {
                    return NotFound(); // Devuelve NotFound si no se encuentra el elemento o ambiente no es válido
                }

                return NoContent(); // Devuelve NoContent si se actualiza con éxito
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("API/BorrarPersona/{id_Personas}")]
        public async Task<IActionResult> Delete(int id_Personas)
        {
            try
            {
                var result = await _ipersonas.EliminarPersonas(id_Personas);

                if (result == null)
                {
                    return NotFound(); // Devuelve NotFound si no se encuentra el elemento o ambiente no es válido
                }

                return NoContent(); // Devuelve NoContent si se actualiza con éxito
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("API/agregarPersona")]

        public async Task<IActionResult> Post([FromBody] PersonasModels insertar)
        {
            try
            {
                var insertado = await _ipersonas.InsertarPersonas(insertar);

                return insertado != null
                    ? CreatedAtAction(nameof(Index), new { id = insertado.id_Personas}, insertado)
                    : Conflict();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
