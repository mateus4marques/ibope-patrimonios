using Patrimonios.Domain.Entities;
using System;
using System.Collections.Generic;

namespace Patrimonios.Domain.Repositories
{
    public interface IPatrimonioRepository
    {
        IEnumerable<Patrimonio> GetAll();
        IEnumerable<Patrimonio> GetAllFromMarcaId(Guid marcaId);
        Patrimonio GetById(Guid id);
        Patrimonio GetByName(string name);
        void Add(Patrimonio marca);
        void Update(Patrimonio marca);
        void Delete(Guid id);
    }
}