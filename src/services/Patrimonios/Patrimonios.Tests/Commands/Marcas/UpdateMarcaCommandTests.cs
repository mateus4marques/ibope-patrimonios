using Microsoft.VisualStudio.TestTools.UnitTesting;
using Patrimonios.Domain.Commands.Marcas;
using System;

namespace Patrimonios.Tests.Commands.Marcas
{
    [TestClass]
    public class UpdateMarcaCommandTests
    {
        [DataTestMethod]
        [DataRow(null, DisplayName = "Nome null")]
        [DataRow("", DisplayName = "Nome vazio")]
        [DataRow("ab", DisplayName = "Nome com menos de 3 caracteres")]
        [DataRow("ababababababababababababababababababababababababababababababababababababababababababababababababababa", DisplayName = "Nome com mais de 100 caracteres")]
        public void InvalidarCommandPeloNome(string nome)
        {
            UpdateMarcaCommand command = new UpdateMarcaCommand { Id = Guid.NewGuid(), Nome = nome };
            command.Validate();
            Assert.IsTrue(command.IsInvalid());
        }

        [TestMethod]
        public void InvalidarCommandPeloId()
        {
            UpdateMarcaCommand command = new UpdateMarcaCommand { Nome = "Não informei o ID."};
            command.Validate();
            Assert.IsTrue(command.IsInvalid());
        }

        [TestMethod]
        public void CommandValid()
        {
            UpdateMarcaCommand command = new UpdateMarcaCommand { Id = Guid.NewGuid(), Nome = "Command Valido" };
            command.Validate();
            Assert.IsTrue(command.IsValid());
        }
    }
}
