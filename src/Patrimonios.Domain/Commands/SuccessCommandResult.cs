namespace Patrimonios.Domain.Commands
{
    public class SuccessCommandResult<T> : CommandResult<T> where T : class
    {
        public T Data { get; set; }

        public static SuccessCommandResult<T> Create(T data)
        {
            return new SuccessCommandResult<T>
            {
                Data = data
            };
        }
    }
}
