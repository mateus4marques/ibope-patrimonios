using Microsoft.VisualStudio.TestTools.UnitTesting;
using Patrimonios.Domain.Commands;
using Patrimonios.Domain.Commands.Marcas;
using Patrimonios.Domain.Entities;
using Patrimonios.Domain.Handlers.Marcas;
using Patrimonios.Tests.FakeRepositories;
using System.Linq;

namespace Patrimonios.Tests.Hadlers.Marcas
{
    [TestClass]
    public class CreateMarcaHandlerTests
    {
        public CreateMarcaHandlerTests()
        {
            marcaRepository = new MarcaRepositoryFake();
            handler = new CreateMarcaHandler(marcaRepository, null);
        }

        CreateMarcaHandler handler;
        MarcaRepositoryFake marcaRepository;

        [TestMethod]
        public void DadoUmCommandoInvalidoRetornarErro()
        {
            CreateMarcaCommand command = new CreateMarcaCommand { };

            var result = handler.Handle(command, new System.Threading.CancellationToken()).Result;

            Assert.IsInstanceOfType(result, typeof(ErrorCommandResult<CreateMarcaCommandResult>));
        }

        [TestMethod]
        public void DadoUmCommandoValidoRetornarSucesso()
        {
            CreateMarcaCommand command = new CreateMarcaCommand { Nome = "TESTE VALIDO" };

            var result = handler.Handle(command, new System.Threading.CancellationToken()).Result;

            Assert.IsInstanceOfType(result, typeof(SuccessCommandResult<CreateMarcaCommandResult>));
        }

        [TestMethod]
        public void DadoUmCommandoValidoMasComUmNomeQueJaExisteRetornarErro()
        {
            Marca marca = marcaRepository.Marcas.First();

            CreateMarcaCommand command = new CreateMarcaCommand { Nome = marca.Nome };

            var result = handler.Handle(command, new System.Threading.CancellationToken()).Result;

            Assert.IsInstanceOfType(result, typeof(ErrorCommandResult<CreateMarcaCommandResult>));
        }
    }
}
