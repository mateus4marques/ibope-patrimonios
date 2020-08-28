using System;

namespace Patrimonios.Domain.Entities
{
    public class Patrimonio
    {
        internal Patrimonio() { }

        public Patrimonio(string nome, Guid marcarId, string descricao)
        {
            Id = Guid.NewGuid();
            Nome = nome;
            MarcaId = marcarId;
            Descricao = descricao;
            NumeroDoTombo = NumeroDoTomboGenerate();
        }

        public Guid Id { get; private set; }
        public string Nome { get; private set; }
        public Guid MarcaId { get; private set; }
        public string Descricao { get; private set; }
        public string NumeroDoTombo { get; private set; }

        public void Update(string nome, Guid marcarId, string descricao)
        {
            Nome = nome;
            MarcaId = marcarId;
            Descricao = descricao;
        }

        private string NumeroDoTomboGenerate()
        {
            return Guid.NewGuid().ToString().Replace("-", "");
        }

        public static Patrimonio Create()
        {
            return new Patrimonio();
        }
    }
}