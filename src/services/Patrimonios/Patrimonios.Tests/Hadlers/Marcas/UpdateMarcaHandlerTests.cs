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
    public class UpdateMarcaHandlerTests
    {
        public UpdateMarcaHandlerTests()
        {
            marcaRepository = new MarcaRepositoryFake();
            handler = new UpdateMarcaHandler(marcaRepository, null);
        }

        UpdateMarcaHandler handler;
        MarcaRepositoryFake marcaRepository;

        [TestMethod]
        public void DadoUmCommandoInvalidoRetornarErro()
        {
            UpdateMarcaCommand command = new UpdateMarcaCommand { };

            var result = handler.Handle(command, new System.Threading.CancellationToken()).Result;

            Assert.IsInstanceOfType(result, typeof(ErrorCommandResult<UpdateMarcaCommandResult>));
        }

        [TestMethod]
        public void DadoUmCommandoValidoRetornarSucesso()
        {
            Guid id = marcaRepository.Marcas.First().Id;

            UpdateMarcaCommand command = new UpdateMarcaCommand { Id = id, Nome = "TESTE VALIDO" };

            var result = handler.Handle(command, new System.Threading.CancellationToken()).Result;

            Assert.IsInstanceOfType(result, typeof(SuccessCommandResult<UpdateMarcaCommandResult>));
        }

        [TestMethod]
        public void DadoUmCommandoValidoMasComIdNaoExisteRetornarErro()
        {
            UpdateMarcaCommand command = new UpdateMarcaCommand { Id = Guid.NewGuid(), Nome = "TESTE VALIDO" };

            var result = handler.Handle(command, new System.Threading.CancellationToken()).Result;

            Assert.IsInstanceOfType(result, typeof(ErrorCommandResult<UpdateMarcaCommandResult>));
        }

        [TestMethod]
        public void DadoUmCommandoValidoMasComUmNomeQueJaExisteRetornarErro()
        {
            Guid id = marcaRepository.Marcas.First().Id;
            string nome = marcaRepository.Marcas.Last().Nome;

            UpdateMarcaCommand command = new UpdateMarcaCommand { Id = id, Nome = nome };

            var result = handler.Handle(command, new System.Threading.CancellationToken()).Result;

            Assert.IsInstanceOfType(result, typeof(ErrorCommandResult<UpdateMarcaCommandResult>));
        }
    }
}
