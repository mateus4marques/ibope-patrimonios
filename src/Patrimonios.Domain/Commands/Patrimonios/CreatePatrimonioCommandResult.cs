using Patrimonios.Domain.Entities;
using System;

namespace Patrimonios.Domain.Commands.Patrimonios
{
    public class CreatePatrimonioCommandResult
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public Guid MarcaId { get; set; }


        public static explicit operator CreatePatrimonioCommandResult(Patrimonio entity)
        {
            return new CreatePatrimonioCommandResult
            {
                Id = entity.Id,
                Nome = entity.Nome,
                MarcaId = entity.MarcaId,
                Descricao = entity.Descricao
            };
        }
    }
}
