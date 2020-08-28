using Patrimonios.Domain.Entities;
using System;

namespace Patrimonios.Domain.Queries.Marcas
{
    public class GetMarcaByIdQueryResult
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }

        public static explicit operator GetMarcaByIdQueryResult(Marca v)
        {
            if (v == null)
                return null;

            return new GetMarcaByIdQueryResult
            {
                Id = v.Id,
                Nome = v.Nome
            };
        }
    }
}
