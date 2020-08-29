using Microsoft.VisualStudio.TestTools.UnitTesting;
using Patrimonios.Domain.Commands.Marcas;

namespace Patrimonios.Tests.Commands.Marcas
{
    [TestClass]
    public class CreateMarcaCommandTests
    {
        [DataTestMethod]
        [DataRow(null, DisplayName = "Nome null")]
        [DataRow("", DisplayName = "Nome vazio")]
        [DataRow("ab", DisplayName = "Nome com menos de 3 caracteres")]
        [DataRow("ababababababababababababababababababababababababababababababababababababababababababababababababababa", DisplayName = "Nome com mais de 100 caracteres")]
        public void InvalidarCommandPeloNome(string nome)
        {
            CreateMarcaCommand command = new CreateMarcaCommand { Nome = nome };
            command.Validate();
            Assert.IsTrue(command.IsInvalid());
        }

        public void CommandValido()
        {
            CreateMarcaCommand command = new CreateMarcaCommand { Nome = "Teste" };
            command.Validate();
            Assert.IsTrue(command.IsValid());
        }
    }
}
