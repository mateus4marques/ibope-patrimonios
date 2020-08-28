using Patrimonios.Domain.Entities;

namespace Patrimonios.Domain.Commands.Patrimonios
{
    public class DeletePatrimonioCommandResult
    {
        public static explicit operator DeletePatrimonioCommandResult(Patrimonio entity)
        {
            return new DeletePatrimonioCommandResult { };
        }
    }
}
