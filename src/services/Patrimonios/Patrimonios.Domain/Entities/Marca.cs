using System;

namespace Patrimonios.Domain.Entities
{
    public class Marca
    {
        protected Marca() { }

        public Marca(string nome)
        {
            Id = Guid.NewGuid();
            Nome = nome;
        }

        public Guid Id { get; private set; }
        public string Nome { get; private set; }

        public void Update(string nome)
        {
            Nome = nome;
        }


        public static Marca Create()
        {
            return new Marca();
        }
    }
}