using Patrimonios.Domain.Entities;
using System;

namespace Patrimonios.Domain.Commands.Marcas
{
    public class CreateMarcaCommandResult
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }

        public static explicit operator CreateMarcaCommandResult(Marca entity)
        {
            return new CreateMarcaCommandResult
            {
                Id = entity.Id,
                Nome = entity.Nome,
            };
        }
    }
}
