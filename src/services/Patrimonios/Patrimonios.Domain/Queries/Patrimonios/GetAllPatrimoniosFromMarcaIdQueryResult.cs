using Patrimonios.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Patrimonios.Domain.Queries.Patrimonios
{
    public class GetAllPatrimoniosFromMarcaIdQueryResult
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public Guid MarcaId { get; set; }
        public string Descricao { get; set; }
        public string NumeroDoTombo { get; set; }

        public static explicit operator GetAllPatrimoniosFromMarcaIdQueryResult(Patrimonio v)
        {
            return new GetAllPatrimoniosFromMarcaIdQueryResult
            {
                Id = v.Id,
                Nome = v.Nome,
                Descricao = v.Descricao,
                MarcaId = v.MarcaId,
                NumeroDoTombo = v.NumeroDoTombo
            };
        }
    }
}
