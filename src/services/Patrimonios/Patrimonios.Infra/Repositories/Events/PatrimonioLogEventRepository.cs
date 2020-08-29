using Microsoft.Extensions.Configuration;
using Patrimonios.Domain.Notifications;
using Patrimonios.Domain.Repositories.Events;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Patrimonios.Infra.Repositories.Events
{
    public class PatrimonioLogEventRepository : IPatrimonioLogEventRepository
    {
        public PatrimonioLogEventRepository(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("HistConnection");
        }

        private readonly string connectionString = string.Empty;

        public void Add(PatrimonioNotification patrimonio)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string comandoSQL = "insert into patrimonio_events(evento, data_evento, patrimonio_id, nome, marca_id, descricao, numero_do_tombo) values(@evento, @data_evento, @patrimonio_id, @nome, @marcaId, @descricao, @numeroDoTombo)";
                SqlCommand command = new SqlCommand(comandoSQL, connection);

                command.CommandType = CommandType.Text;
                command.Parameters.AddWithValue("@evento", patrimonio.Event);
                command.Parameters.AddWithValue("@data_evento", patrimonio.EventDate);
                command.Parameters.AddWithValue("@patrimonio_id", patrimonio.Id);
                command.Parameters.AddWithValue("@nome", patrimonio.Nome);
                command.Parameters.AddWithValue("@marcaId", patrimonio.MarcaId);
                command.Parameters.AddWithValue("@descricao", ((object)patrimonio.Descricao) ?? DBNull.Value);
                command.Parameters.AddWithValue("@numeroDoTombo", patrimonio.NumeroDoTombo);
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
    }
}
