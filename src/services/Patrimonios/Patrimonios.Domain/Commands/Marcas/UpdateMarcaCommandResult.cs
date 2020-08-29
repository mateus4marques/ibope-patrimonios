using Patrimonios.Domain.Entities;
using System;

namespace Patrimonios.Domain.Commands.Marcas
{
    public class UpdateMarcaCommandResult
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }

        public static explicit operator UpdateMarcaCommandResult(Marca entity)
        {
            return new UpdateMarcaCommandResult
            {
                Id = entity.Id,
                Nome = entity.Nome
            };
        }
    }
}
