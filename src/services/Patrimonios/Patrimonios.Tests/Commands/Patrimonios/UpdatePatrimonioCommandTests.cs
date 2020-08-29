using Microsoft.VisualStudio.TestTools.UnitTesting;
using Patrimonios.Domain.Commands.Patrimonios;
using System;

namespace Patrimonios.Tests.Commands.Patrimonios
{
    [TestClass]
    public class UpdatePatrimonioCommandTests
    {
        [DataTestMethod]
        [DataRow(null, DisplayName = "Nome null")]
        [DataRow("", DisplayName = "Nome vazio")]
        [DataRow("ab", DisplayName = "Nome com menos de 3 caracteres")]
        [DataRow("ababababababababababababababababababababababababababababababababababababababababababababababababababa", DisplayName = "Nome com mais de 100 caracteres")]
        public void InvalidarCommandPeloNome(string nome)
        {
            UpdatePatrimonioCommand command = new UpdatePatrimonioCommand { Id = Guid.NewGuid(), Nome = nome, MarcaId = Guid.NewGuid(), Descricao = null };
            command.Validate();
            Assert.IsTrue(command.IsInvalid());
        }

        [TestMethod]
        public void InvalidarCommandPeloDescricao()
        {
            string descricao = new string('a', 305);
            UpdatePatrimonioCommand command = new UpdatePatrimonioCommand { Id = Guid.NewGuid(), Nome = "Teste", MarcaId = Guid.NewGuid(), Descricao = descricao };
            command.Validate();
            Assert.IsTrue(command.IsInvalid());
        }

        [TestMethod]
        public void InvalidarCommandPelaMarcaId()
        {
            UpdatePatrimonioCommand command = new UpdatePatrimonioCommand { Id = Guid.NewGuid(), Nome = "Teste", MarcaId = new Guid(), Descricao = null };
            command.Validate();
            Assert.IsTrue(command.IsInvalid());
        }

        [TestMethod]
        public void InvalidarCommandPelaId()
        {
            UpdatePatrimonioCommand command = new UpdatePatrimonioCommand { Nome = "Teste", MarcaId = Guid.NewGuid(), Descricao = null };
            command.Validate();
            Assert.IsTrue(command.IsInvalid());
        }
    }
}
