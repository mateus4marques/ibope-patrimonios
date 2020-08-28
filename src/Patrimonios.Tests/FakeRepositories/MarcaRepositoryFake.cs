using Patrimonios.Domain.Entities;
using Patrimonios.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Patrimonios.Tests.FakeRepositories
{
    public class MarcaRepositoryFake : IMarcaRepository
    {
        public MarcaRepositoryFake()
        {
            Generate();
        }

        public List<Marca> Marcas = new List<Marca>();

        public void Generate()
        {
            Marcas = new List<Marca>();
            Marcas.Add(new Marca("TESTE 1"));
            Marcas.Add(new Marca("TESTE 2"));
            Marcas.Add(new Marca("TESTE 3"));
            Marcas.Add(new Marca("TESTE 4"));
            Marcas.Add(new Marca("TESTE 5"));
            Marcas.Add(new Marca("TESTE 6"));
            Marcas.Add(new Marca("TESTE 7"));
        }

        public void Add(Marca marca) { }

        public void Delete(Guid id) { }

        public IEnumerable<Marca> GetAll()
        {
            return Marcas;
        }

        public Marca GetById(Guid id)
        {
            return Marcas.FirstOrDefault(x => x.Id == id);
        }

        public Marca GetByName(string name)
        {
            return Marcas.FirstOrDefault(x => x.Nome == name);

        }

        public void Update(Marca marca) { }
    }
}
