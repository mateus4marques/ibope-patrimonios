using Microsoft.VisualStudio.TestTools.UnitTesting;
using Patrimonios.Domain.Commands;
using Patrimonios.Domain.Commands.Patrimonios;
using Patrimonios.Domain.Handlers.Patrimonios;
using Patrimonios.Tests.FakeRepositories;
using System;
using System.Linq;

namespace Patrimonios.Tests.Hadlers.Patrimonios
{
    [TestClass]
    public class DeletePatrimonioHandlerTests
    {
        public DeletePatrimonioHandlerTests()
        {
            patrimonioRepository = new PatrimonioRepositoryFake();
            handler = new DeletePatrimonioHandler(patrimonioRepository, null);
        }

        DeletePatrimonioHandler handler;
        PatrimonioRepositoryFake patrimonioRepository;

        [TestMethod]
        public void DadoUmCommandoInvalidoRetornarErro()
        {
            DeletePatrimonioCommand command = new DeletePatrimonioCommand { };

            var result = handler.Handle(command, new System.Threading.CancellationToken()).Result;

            Assert.IsInstanceOfType(result, typeof(ErrorCommandResult<DeletePatrimonioCommandResult>));
        }

        [TestMethod]
        public void DadoUmCommandoValidoRetornarSucesso()
        {
            Guid id = patrimonioRepository.Patrimonios.First().Id;

            DeletePatrimonioCommand command = new DeletePatrimonioCommand { Id = id };

            var result = handler.Handle(command, new System.Threading.CancellationToken()).Result;

            Assert.IsInstanceOfType(result, typeof(SuccessCommandResult<DeletePatrimonioCommandResult>));
        }

        [TestMethod]
        public void DadoUmCommandoValidoMasComIdNaoExisteRetornarErro()
        {
            DeletePatrimonioCommand command = new DeletePatrimonioCommand { Id = Guid.NewGuid() };

            var result = handler.Handle(command, new System.Threading.CancellationToken()).Result;

            Assert.IsInstanceOfType(result, typeof(ErrorCommandResult<DeletePatrimonioCommandResult>));
        }
    }
}
