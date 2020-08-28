using Microsoft.Extensions.Configuration;
using Patrimonios.Domain.Notifications;
using Patrimonios.Domain.Repositories.Events;
using System.Data;
using System.Data.SqlClient;

namespace Patrimonios.Infra.Repositories.Events
{
    public class MarcaLogEventRepository : IMarcaLogEventRepository
    {
        public MarcaLogEventRepository(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("HistConnection");
        }

        private readonly string connectionString = string.Empty;

        public void Add(MarcaNotification marca)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string comandoSQL = "insert into marca_events(evento, data_evento, marca_id, nome) values(@evento, @data_evento, @marca_id, @nome)";
                SqlCommand command = new SqlCommand(comandoSQL, connection);
                command.CommandType = CommandType.Text;
                command.Parameters.AddWithValue("@evento", marca.Event);
                command.Parameters.AddWithValue("@data_evento", marca.EventDate);
                command.Parameters.AddWithValue("@marca_id", marca.Id);
                command.Parameters.AddWithValue("@nome", marca.Nome);
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
    }
}
