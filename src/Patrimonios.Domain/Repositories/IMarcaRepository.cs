using Patrimonios.Domain.Entities;
using System;
using System.Collections.Generic;

namespace Patrimonios.Domain.Repositories
{
    public interface IMarcaRepository
    {
        IEnumerable<Marca> GetAll();
        Marca GetById(Guid id);
        Marca GetByName(string name); 
        void Add(Marca marca);
        void Update(Marca marca);
        void Delete(Guid id);
    }
}