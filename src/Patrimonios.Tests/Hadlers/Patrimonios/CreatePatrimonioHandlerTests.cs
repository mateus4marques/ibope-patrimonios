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
    public class CreatePatrimonioHandlerTests
    {
        public CreatePatrimonioHandlerTests()
        {
            patrimonioRepository = new PatrimonioRepositoryFake();
            marcaRepository = new MarcaRepositoryFake();
            handler = new CreatePatrimonioHandler(patrimonioRepository, marcaRepository, null);
        }

        CreatePatrimonioHandler handler;
        PatrimonioRepositoryFake patrimonioRepository;
        MarcaRepositoryFake marcaRepository;

        [TestMethod]
        public void DadoUmCommandoInvalidoRetornarErro()
        {
            CreatePatrimonioCommand command = new CreatePatrimonioCommand { };

            var result = handler.Handle(command, new System.Threading.CancellationToken()).Result;

            Assert.IsInstanceOfType(result, typeof(ErrorCommandResult<CreatePatrimonioCommandResult>));
        }

        [TestMethod]
        public void DadoUmCommandoValidoRetornarSucesso()
        {
            Marca marca = marcaRepository.Marcas.First();

            CreatePatrimonioCommand command = new CreatePatrimonioCommand { Nome = "TESTE VALIDO", MarcaId = marca.Id, Descricao = null };

            var result = handler.Handle(command, new System.Threading.CancellationToken()).Result;

            Assert.IsInstanceOfType(result, typeof(SuccessCommandResult<CreatePatrimonioCommandResult>));
        }

        [TestMethod]
        public void DadoUmCommandoValidoMasComMarcaIdNaoExisteRetornarErro()
        {
            CreatePatrimonioCommand command = new CreatePatrimonioCommand { Nome = "TESTE VALIDO", MarcaId = Guid.NewGuid(), Descricao = null };

            var result = handler.Handle(command, new System.Threading.CancellationToken()).Result;

            Assert.IsInstanceOfType(result, typeof(ErrorCommandResult<CreatePatrimonioCommandResult>));
        }


        [TestMethod]
        public void DadoUmCommandoValidoMasComUmNomeQueJaExisteRetornarErro()
        {
            Marca marca = marcaRepository.Marcas.First();
            string nome = patrimonioRepository.Patrimonios.First().Nome;

            CreatePatrimonioCommand command = new CreatePatrimonioCommand { Nome = nome, MarcaId = marca.Id, Descricao = null };

            var result = handler.Handle(command, new System.Threading.CancellationToken()).Result;

            Assert.IsInstanceOfType(result, typeof(ErrorCommandResult<CreatePatrimonioCommandResult>));
        }
    }
}
