using definitiva.Models;

namespace definitiva.Interfaces
{
    public interface Ipersonas
    {
        Task<IEnumerable<PersonasModels>> GetPersonas();
        Task<PersonasModels> ActualizarPersonas(int id_Personas, PersonasModels updatePersonas);
        Task<bool> EliminarPersonas(int id_Personas);
        Task<PersonasModels> InsertarPersonas( PersonasModels insertar);
    }
}
