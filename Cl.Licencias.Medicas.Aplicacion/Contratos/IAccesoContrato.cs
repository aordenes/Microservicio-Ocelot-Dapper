using Cl.Licencias.Medicas.Aplicacion.Dto.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cl.Licencias.Medicas.Aplicacion.Contratos
{
    public interface IAccesoContrato<T> where T : class
    {
        /// <summary>
        /// Obtiene el nombre y el pass del usuario
        /// </summary>
        /// <param name="authenticationRequest">Entidad de entrada entregando el usuario y el pass</param>
        /// <returns>Obtiene el nombre y el pass del usario</returns>
        Task<T> GetPersonaByNombreAndPass(AuthenticationRequest authenticationRequest);

        /// <summary>
        /// Obtiene a la persona por medio del ID
        /// </summary>
        /// <param name="id">Id del usuario</param>
        /// <returns>Retorna a la persona</returns>
        Task<T> GetPersonaId(int? id);
    }
}
