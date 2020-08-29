using Microsoft.VisualStudio.TestTools.UnitTesting;
using Patrimonios.Domain.Entities;
using System;

namespace Patrimonios.Tests.Entities
{
    [TestClass]
    public class MarcaTests
    {
        [TestMethod]
        public void AoCriarUmaMarcaDeveGerarUmId()
        {
            Marca marca = new Marca("Teste");
            Assert.IsTrue(marca.Id != Guid.Empty);
        }

        [TestMethod]
        public void AoAlterarUmaMarcaOIdNaoDeveSerAlterado()
        {
            Marca marca = new Marca("Teste");
            Guid id = marca.Id;
            marca.Update("Alterado");
            Assert.AreEqual(id, marca.Id);
        }
    }
}
