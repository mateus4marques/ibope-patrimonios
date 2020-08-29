using Patrimonios.Domain.Entities;
using System;

namespace Patrimonios.Domain.Queries.Marcas
{
    public class GetAllMarcasQueryResult
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }

        public static explicit operator GetAllMarcasQueryResult(Marca v)
        {
            return new GetAllMarcasQueryResult
            {
                Id = v.Id,
                Nome = v.Nome
            };
        }
    }
}
