using Microsoft.VisualStudio.TestTools.UnitTesting;
using Patrimonios.Domain.Entities;
using System;

namespace Patrimonios.Tests.Entities
{
    [TestClass]
    public class PatrimonioTests
    {
        [TestMethod]
        public void AoCriarUmPatrimonioDeveGerarUmId()
        {
            Patrimonio patrimonio = new Patrimonio("Teste", Guid.NewGuid(), null);
            Assert.IsTrue(patrimonio.Id != Guid.Empty);
        }

        [TestMethod]
        public void AoCriarUmPatrimonioDeveGerarNumeroDoTombo()
        {
            Patrimonio patrimonio = new Patrimonio("Teste", Guid.NewGuid(), null);
            Assert.IsTrue(!string.IsNullOrEmpty(patrimonio.NumeroDoTombo));
        }

        [TestMethod]
        public void AoAlterarUmaMarcaOIdNaoDeveSerAlterado()
        {
            Patrimonio patrimonio = new Patrimonio("Teste", Guid.NewGuid(), null);
            Guid id = patrimonio.Id;
            patrimonio.Update("Alterado", Guid.NewGuid(), null);
            Assert.AreEqual(id, patrimonio.Id);
        }
    }
}
