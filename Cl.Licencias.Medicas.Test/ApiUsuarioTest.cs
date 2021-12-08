using Cl.Licencias.Medicas.Aplicacion.Dto.Request;
using Cl.Licencias.Medicas.Usuarios.Api.Controllers;
using FakeItEasy;
using System;
using System.Data;
using System.Threading.Tasks;
using Xunit;
using System.Linq;
using Cl.Licencias.Medicas.Transversal.Log;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Cl.Licencias.Medicas.Aplicacion.Contratos;

namespace Cl.Licencias.Medicas.Test
{
    public class ApiUsuarioTest
    {
        private readonly LoggerManager _LoggerManager = new();


        [Fact]
        public async void GetUsuarios_Returns_Lista()
        {
            //Arrange
            int count = 2;
            var lstFakeLista = A.CollectionOfDummy<PersonaRequest>(count).AsEnumerable();
            var dataStore = A.Fake<IPersonaLicenciaContrato<PersonaRequest>>();
            
            A.CallTo(() => dataStore.ObtenerUsuariosLicencia()).Returns(Task.FromResult(lstFakeLista));
            var controller = new UsuariosLicenciasController(dataStore, _LoggerManager);

            // Act

            var actionResult = await controller.GetListUsuarios();
            

            // Assert

            var result = actionResult.Result as OkObjectResult;
            var returnPersonas = result.Value as IEnumerable<PersonaRequest>;
            Assert.True(returnPersonas.Count().Equals(count));
            //Assert.NotNull(result);
        }

        [Fact]
        public async void GetUsuario_Return_Object()
        {
            //Arrange           
            
            var fakeUsuario = A.Fake<PersonaRequest>();
            var dataStore = A.Fake<IPersonaLicenciaContrato<PersonaRequest>>();

            A.CallTo(() => dataStore.ObtenerUsuariosLicencia(1)).Returns(Task.FromResult(fakeUsuario));
            var controller = new UsuariosLicenciasController(dataStore, _LoggerManager);

            // Act

            var actionResult = await controller.GetUsuario(1);

            // Assert

            var result = actionResult as OkObjectResult;
            var returnPersonas = result.Value as PersonaRequest;
            //Assert.True(returnPersonas);
            Assert.NotNull(returnPersonas);
        }

        [Fact]
        public async void AgregaUsuario_verifica_llamada_insert()
        {
            //Arrange            
            var personaDemo = new PersonaRequest { 
              Apellido = "apellidoTest",
              Direccion = "DireccionTest",
              Email = "email@email.test",
              Nombre = "nombreTest",
              Passuser = "passTest",
              PersonaId = 1,
              Telefono = "123456789"
            };

            PersonaRequest persona;

            var dataStore = A.Fake<IPersonaLicenciaContrato<PersonaRequest>>();

            A.CallTo(() => dataStore.InsertaPersonaNatural(A<PersonaRequest>._))
                .Invokes((PersonaRequest per) => persona = new PersonaRequest())
                .Returns(true);
            await dataStore.InsertaPersonaNatural(personaDemo);



            var controller = new UsuariosLicenciasController(dataStore, _LoggerManager);
           
            // Act
            
            var actionResult = await controller.AgregaUsuario(personaDemo);

            var result = actionResult as OkObjectResult;
            var returnPersonas = result.Value as PersonaRequest;

            // Assert
            Assert.True(result.Value.Equals(true));

        }

        [Fact]
        public async void AgregaListaUsuarios_verifica_llamada_insert()
        {
            //Arrange 
            IEnumerable<PersonaRequest> lstPersonaRequest = new List<PersonaRequest>();

            IEnumerable<PersonaRequest> lstPersonas;

           var dataStore = A.Fake<IPersonaLicenciaContrato<PersonaRequest>>();

            A.CallTo(() => dataStore.InsertaListaPersonas(A<IEnumerable<PersonaRequest>>._))
                .Invokes((IEnumerable<PersonaRequest> list) => lstPersonas = new List<PersonaRequest>())
                .Returns(true);
            await dataStore.InsertaListaPersonas(lstPersonaRequest);

            var controller = new UsuariosLicenciasController(dataStore, _LoggerManager);

            // Act
            var actionResult = await controller.AgregaListaUsuarios(lstPersonaRequest);

            var result = actionResult as OkObjectResult;
            var returnPersonas = result.Value as PersonaRequest;

            // Assert
            Assert.True(result.Value.Equals(true));

        }

    }
}
