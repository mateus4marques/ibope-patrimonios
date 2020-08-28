using Microsoft.VisualStudio.TestTools.UnitTesting;
using Patrimonios.Domain.Commands.Patrimonios;
using System;
using System.Collections.Generic;
using System.Text;

namespace Patrimonios.Tests.Commands.Patrimonios
{
    [TestClass]
    public class CreatePatrimonioCommandTests
    {
        [DataTestMethod]
        [DataRow(null, DisplayName = "Nome null")]
        [DataRow("", DisplayName = "Nome vazio")]
        [DataRow("ab", DisplayName = "Nome com menos de 3 caracteres")]
        [DataRow("ababababababababababababababababababababababababababababababababababababababababababababababababababa", DisplayName = "Nome com mais de 100 caracteres")]
        public void InvalidarCommandPeloNome(string nome)
        {
            CreatePatrimonioCommand command = new CreatePatrimonioCommand { Nome = nome, MarcaId = Guid.NewGuid(), Descricao = null };
            command.Validate();
            Assert.IsTrue(command.IsInvalid());
        }

        [TestMethod]
        public void InvalidarCommandPeloDescricao()
        {
            string descricao = new string('a', 305);
            CreatePatrimonioCommand command = new CreatePatrimonioCommand { Nome = "Teste", MarcaId = Guid.NewGuid(), Descricao = descricao };
            command.Validate();
            Assert.IsTrue(command.IsInvalid());
        }

        [TestMethod]
        public void InvalidarCommandPelaMarcaId()
        {
            CreatePatrimonioCommand command = new CreatePatrimonioCommand { Nome = "Teste", MarcaId = new Guid(), Descricao = null };
            command.Validate();
            Assert.IsTrue(command.IsInvalid());
        }

        [TestMethod]
        public void CommandValido()
        {
            CreatePatrimonioCommand command = new CreatePatrimonioCommand { Nome = "Teste", MarcaId = Guid.NewGuid(), Descricao = null };
            command.Validate();
            Assert.IsTrue(command.IsValid());
        }
    }
}
