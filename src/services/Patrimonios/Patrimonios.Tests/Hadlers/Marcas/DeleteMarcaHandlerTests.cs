using Microsoft.VisualStudio.TestTools.UnitTesting;
using Patrimonios.Domain.Commands;
using Patrimonios.Domain.Commands.Marcas;
using Patrimonios.Domain.Handlers.Marcas;
using Patrimonios.Tests.FakeRepositories;
using System;
using System.Linq;

namespace Patrimonios.Tests.Hadlers.Marcas
{
    [TestClass]
    public class DeleteMarcaHandlerTests
    {
        public DeleteMarcaHandlerTests()
        {
            marcaRepository = new MarcaRepositoryFake();
            patrimonioRepository = new PatrimonioRepositoryFake(); 
            handler = new DeleteMarcaHandler(marcaRepository, patrimonioRepository, null);
        }

        DeleteMarcaHandler handler;
        MarcaRepositoryFake marcaRepository;
        PatrimonioRepositoryFake patrimonioRepository; 

        [TestMethod]
        public void DadoUmCommandoInvalidoRetornarErro()
        {
            DeleteMarcaCommand command = new DeleteMarcaCommand { };

            var result = handler.Handle(command, new System.Threading.CancellationToken()).Result;

            Assert.IsInstanceOfType(result, typeof(ErrorCommandResult<DeleteMarcaCommandResult>));
        }

        [TestMethod]
        public void DadoUmCommandoValidoRetornarSucesso()
        {
            Guid id = marcaRepository.Marcas.First().Id;

            DeleteMarcaCommand command = new DeleteMarcaCommand { Id = id };

            var result = handler.Handle(command, new System.Threading.CancellationToken()).Result;

            Assert.IsInstanceOfType(result, typeof(SuccessCommandResult<DeleteMarcaCommandResult>));
        }

        [TestMethod]
        public void DadoUmCommandoValidoMasComIdNaoExisteRetornarErro()
        {
            DeleteMarcaCommand command = new DeleteMarcaCommand { Id = Guid.NewGuid() };

            var result = handler.Handle(command, new System.Threading.CancellationToken()).Result;

            Assert.IsInstanceOfType(result, typeof(ErrorCommandResult<DeleteMarcaCommandResult>));
        }
    }
}
