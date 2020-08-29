using Patrimonios.Domain.Entities;
using System;

namespace Patrimonios.Domain.Commands.Patrimonios
{
    public class UpdatePatrimonioCommandResult
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public Guid MarcaId { get; set; }

        public static explicit operator UpdatePatrimonioCommandResult(Patrimonio entity)
        {
            return new UpdatePatrimonioCommandResult
            {
                Id = entity.Id,
                Nome = entity.Nome,
                MarcaId = entity.MarcaId,
                Descricao = entity.Descricao
            };
        }
    }
}
