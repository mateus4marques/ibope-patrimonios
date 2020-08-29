using Patrimonios.Domain.Entities;
using Patrimonios.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Patrimonios.Tests.FakeRepositories
{
    public class PatrimonioRepositoryFake : IPatrimonioRepository
    {
        public PatrimonioRepositoryFake()
        {
            Generate();
        }

        public List<Patrimonio> Patrimonios = new List<Patrimonio>();

        public void Generate()
        {
            Patrimonios = new List<Patrimonio>();
            Patrimonios.Add(new Patrimonio("TESTE 1", Guid.NewGuid(), null));
            Patrimonios.Add(new Patrimonio("TESTE 2", Guid.NewGuid(), null));
            Patrimonios.Add(new Patrimonio("TESTE 3", Guid.NewGuid(), null));
            Patrimonios.Add(new Patrimonio("TESTE 4", Guid.NewGuid(), null));
            Patrimonios.Add(new Patrimonio("TESTE 5", Guid.NewGuid(), null));
            Patrimonios.Add(new Patrimonio("TESTE 6", Guid.NewGuid(), null));
            Patrimonios.Add(new Patrimonio("TESTE 7", Guid.NewGuid(), null));
        }

        public void Add(Patrimonio marca) { }

        public void Delete(Guid id) { }

        public IEnumerable<Patrimonio> GetAll()
        {
            return Patrimonios;
        }

        public IEnumerable<Patrimonio> GetAllFromMarcaId(Guid marcaId)
        {
            return Patrimonios.Where(x => x.MarcaId == marcaId);
        }

        public Patrimonio GetById(Guid id)
        {
            return Patrimonios.FirstOrDefault(x => x.Id == id);
        }

        public Patrimonio GetByName(string name)
        {
            return Patrimonios.FirstOrDefault(x => x.Nome == name);
        }

        public void Update(Patrimonio marca) { }
    }
}
