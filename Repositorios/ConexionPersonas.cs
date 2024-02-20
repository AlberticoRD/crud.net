using definitiva.Context;
using definitiva.Interfaces;
using definitiva.Models;
using Microsoft.EntityFrameworkCore;

namespace definitiva.Repositorios
{
    public class ConexionPersonas : Ipersonas
    {
        public readonly DbContextPersonas _dbContextPersonas;

        public ConexionPersonas (DbContextPersonas dbContextPersonas)
        {
            _dbContextPersonas = dbContextPersonas;
        }

        public async Task<IEnumerable<PersonasModels>> GetPersonas()
        {
            var result = await _dbContextPersonas.Personas.AsNoTracking().ToListAsync();
            return result;
        }

        public async Task<PersonasModels> ActualizarPersonas(int id_Personas, PersonasModels updatePersonas)
        {
            try
            {
                // Verificar si el ID es válido
                if (id_Personas <= 0)
                {
                    return null;
                }

                // Buscar la entidad por el ID
                var personaUpdate = await _dbContextPersonas.Personas.FindAsync(id_Personas);

                // Verificar si la entidad existe
                if (personaUpdate == null)
                {
                    return null;
                }

                // Actualizar propiedades necesarias
                personaUpdate.nombre = updatePersonas.nombre;
                personaUpdate.apellido = updatePersonas.apellido;
                personaUpdate.edad = updatePersonas.edad;

                // Guardar cambios en la base de datos
                await _dbContextPersonas.SaveChangesAsync();

                return personaUpdate;
            }
            catch (Exception ex)
            {
                // Manejar excepciones según tus necesidades
                Console.WriteLine($"Error al actualizar la marca: {ex.Message}");
                return null;
            }
        }

        public async Task<bool> EliminarPersonas(int id_Personas)
        {
            try
            {
                if (id_Personas <= 0)
                {
                    return false;
                }

                var personaEliminar = await _dbContextPersonas.Personas.FindAsync(id_Personas);

                if (personaEliminar == null)
                {
                    return false;
                }

                _dbContextPersonas.Personas.Remove(personaEliminar);

                await _dbContextPersonas.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                // Manejar excepciones según tus necesidades
                Console.WriteLine($"Error al eliminar la persona: {ex.Message}");
                return false;
            }
        }

        public async Task<PersonasModels>InsertarPersonas(PersonasModels insertar)
        {
            try
            {
                var insertado = await _dbContextPersonas.Personas.FirstOrDefaultAsync(i => i.nombre == insertar.nombre && i.apellido == insertar.apellido);

                if (insertado != null)
                {
                    
                    return null;
                }

                _dbContextPersonas.Personas.Add(insertar);

                await _dbContextPersonas.SaveChangesAsync();

                return insertar;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al agregar la persona: {ex.Message}");
                return null;
            }
        }
    }
}

