using Cl.Aplicacion.Dto.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cl.Aplicacion.Contratos
{
    public interface IPersonaLicenciaContrato<T> where T : class
    {
        Task<IEnumerable<T>> ObtenerUsuariosLicencia();

        Task<T> ObtenerUsuariosLicencia(int? id);

        Task<bool> InsertaPersonaNatural(PersonaRequest personaRequest);

        Task<bool> InsertaListaPersonas(IEnumerable<PersonaRequest> personaRequest);

        /// <summary>
        /// Elimina un usuario por medio de un Id
        /// </summary>
        /// <param name="idPersona">Id persona</param>
        /// <returns></returns>
        Task EliminarPersona(int idPersona);

        Task ActualizaUsuario(PersonaRequest personaRequest);

    }
}
