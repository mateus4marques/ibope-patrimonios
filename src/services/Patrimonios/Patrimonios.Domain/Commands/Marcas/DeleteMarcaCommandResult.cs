using Patrimonios.Domain.Entities;
using System;

namespace Patrimonios.Domain.Commands.Marcas
{
    public class DeleteMarcaCommandResult
    {
        public static explicit operator DeleteMarcaCommandResult(Marca v)
        {
            return new DeleteMarcaCommandResult(); 
        }
    }
}
