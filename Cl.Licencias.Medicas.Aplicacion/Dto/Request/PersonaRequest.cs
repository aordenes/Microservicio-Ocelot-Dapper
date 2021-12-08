using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cl.Licencias.Medicas.Aplicacion.Dto.Request
{
    public class PersonaRequest
    {
        public int? PersonaId { get; set; }
        public string Nombre { get; set; }

        public string Apellido { get; set; }

        public string Direccion { get; set; }

        public string Telefono { get; set; }

        public string Email { get; set; }

        public string Passuser { get; set; }
    }
}
