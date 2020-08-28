using Microsoft.VisualStudio.TestTools.UnitTesting;
using Patrimonios.Domain.Commands;
using Patrimonios.Domain.Commands.Patrimonios;
using Patrimonios.Domain.Entities;
using Patrimonios.Domain.Handlers.Patrimonios;
using Patrimonios.Tests.FakeRepositories;
using System;
using System.Linq;

namespace Patrimonios.Tests.Hadlers.Patrimonios
{
    [TestClass]
    public class UpdatePatrimonioHandlerTests
    {
        public UpdatePatrimonioHandlerTests()
        {
            patrimonioRepository = new PatrimonioRepositoryFake();
            marcaRepository = new MarcaRepositoryFake();
            handler = new UpdatePatrimonioHandler(patrimonioRepository, marcaRepository, null);
        }

        UpdatePatrimonioHandler handler;
        PatrimonioRepositoryFake patrimonioRepository;
        MarcaRepositoryFake marcaRepository;

        [TestMethod]
        public void DadoUmCommandoInvalidoRetornarErro()
        {
            UpdatePatrimonioCommand command = new UpdatePatrimonioCommand { };

            var result = handler.Handle(command, new System.Threading.CancellationToken()).Result;

            Assert.IsInstanceOfType(result, typeof(ErrorCommandResult<UpdatePatrimonioCommandResult>));
        }

        [TestMethod]
        public void DadoUmCommandoValidoRetornarSucesso()
        {
            Guid id = patrimonioRepository.Patrimonios.First().Id;
            Marca marca = marcaRepository.Marcas.First();

            UpdatePatrimonioCommand command = new UpdatePatrimonioCommand { Id = id, Nome = "TESTE VALIDO", MarcaId = marca.Id, Descricao = null };

            var result = handler.Handle(command, new System.Threading.CancellationToken()).Result;

            Assert.IsInstanceOfType(result, typeof(SuccessCommandResult<UpdatePatrimonioCommandResult>));
        }

        [TestMethod]
        public void DadoUmCommandoValidoMasComMarcaIdNaoExisteRetornarErro()
        {
            Guid id = patrimonioRepository.Patrimonios.First().Id;
            UpdatePatrimonioCommand command = new UpdatePatrimonioCommand { Id = id, Nome = "TESTE VALIDO", MarcaId = Guid.NewGuid(), Descricao = null };

            var result = handler.Handle(command, new System.Threading.CancellationToken()).Result;

            Assert.IsInstanceOfType(result, typeof(ErrorCommandResult<UpdatePatrimonioCommandResult>));
        }

        [TestMethod]
        public void DadoUmCommandoValidoMasComIdNaoExisteRetornarErro()
        {
            Guid id = patrimonioRepository.Patrimonios.First().Id;
            UpdatePatrimonioCommand command = new UpdatePatrimonioCommand { Id = Guid.NewGuid(), Nome = "TESTE VALIDO", MarcaId = Guid.NewGuid(), Descricao = null };

            var result = handler.Handle(command, new System.Threading.CancellationToken()).Result;

            Assert.IsInstanceOfType(result, typeof(ErrorCommandResult<UpdatePatrimonioCommandResult>));
        }

        [TestMethod]
        public void DadoUmCommandoValidoMasComUmNomeQueJaExisteRetornarErro()
        {
            Marca marca = marcaRepository.Marcas.First();
            Guid id = patrimonioRepository.Patrimonios.First().Id;
            string nome = patrimonioRepository.Patrimonios.Last().Nome;

            UpdatePatrimonioCommand command = new UpdatePatrimonioCommand { Id = id, Nome = nome, MarcaId = marca.Id, Descricao = null };

            var result = handler.Handle(command, new System.Threading.CancellationToken()).Result;

            Assert.IsInstanceOfType(result, typeof(ErrorCommandResult<UpdatePatrimonioCommandResult>));
        }
    }
}
