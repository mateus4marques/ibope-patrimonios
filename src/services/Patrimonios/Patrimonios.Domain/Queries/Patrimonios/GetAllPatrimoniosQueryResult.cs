using Patrimonios.Domain.Entities;
using System;

namespace Patrimonios.Domain.Queries.Patrimonios
{
    public class GetAllPatrimoniosQueryResult
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public Guid MarcaId { get; set; }
        public string Descricao { get; set; }
        public string NumeroDoTombo { get; set; }

        public static explicit operator GetAllPatrimoniosQueryResult(Patrimonio v)
        {
            return new GetAllPatrimoniosQueryResult
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
