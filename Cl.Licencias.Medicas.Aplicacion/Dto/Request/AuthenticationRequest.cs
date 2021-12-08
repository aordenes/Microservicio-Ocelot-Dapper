using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cl.Licencias.Medicas.Aplicacion.Dto.Request
{
    public class AuthenticationRequest
    {
        [Required]
        public string Nombre { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
