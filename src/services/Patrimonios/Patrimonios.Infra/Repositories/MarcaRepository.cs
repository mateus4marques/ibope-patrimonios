using Microsoft.Extensions.Configuration;
using Patrimonios.Domain.Entities;
using Patrimonios.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Patrimonios.Infra.Repositories
{
    public class MarcaRepository : IMarcaRepository
    {
        public MarcaRepository(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        private readonly string connectionString = string.Empty;

        public void Add(Marca marca)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string comandoSQL = "insert into marcas(id, nome) values(@id, @nome)";
                SqlCommand command = new SqlCommand(comandoSQL, connection);
                command.CommandType = CommandType.Text;
                command.Parameters.AddWithValue("@id", marca.Id);
                command.Parameters.AddWithValue("@Nome", marca.Nome);
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        public void Delete(Guid id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string comandoSQL = "delete from marcas where id = @id";
                SqlCommand command = new SqlCommand(comandoSQL, connection);
                command.CommandType = CommandType.Text;
                command.Parameters.AddWithValue("@id", id);
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        public IEnumerable<Marca> GetAll()
        {
            List<Marca> marcas = new List<Marca>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "select id, nome from marcas";
                SqlCommand command = new SqlCommand(query, connection);
                command.CommandType = CommandType.Text;
                connection.Open();
                SqlDataReader read = command.ExecuteReader();
                while (read.Read())
                {
                    Marca marca = Marca.Create();
                    typeof(Marca).GetProperty(nameof(Marca.Id)).SetValue(marca, Guid.Parse(read["id"].ToString()));
                    typeof(Marca).GetProperty(nameof(Marca.Nome)).SetValue(marca, read["nome"].ToString());
                    marcas.Add(marca);
                }
                connection.Close();
            }
            return marcas;
        }

        public Marca GetById(Guid id)
        {
            Marca marca = null;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sqlQuery = "select id, nome from marcas where id = @id";
                SqlCommand command = new SqlCommand(sqlQuery, connection);
                command.CommandType = CommandType.Text;
                command.Parameters.AddWithValue("@id", id);
                connection.Open();
                SqlDataReader read = command.ExecuteReader();
                while (read.Read())
                {
                    marca = Marca.Create();
                    typeof(Marca).GetProperty(nameof(Marca.Id)).SetValue(marca, Guid.Parse(read["id"].ToString()));
                    typeof(Marca).GetProperty(nameof(Marca.Nome)).SetValue(marca, read["nome"].ToString());
                }
            }
            return marca;
        }

        public Marca GetByName(string nome)
        {
            Marca marca = null;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sqlQuery = "select id, nome from marcas where nome = @nome";
                SqlCommand command = new SqlCommand(sqlQuery, connection);
                command.Parameters.AddWithValue("@nome", nome);
                connection.Open();
                SqlDataReader read = command.ExecuteReader();
                while (read.Read())
                {
                    marca = Marca.Create();
                    typeof(Marca).GetProperty(nameof(Marca.Id)).SetValue(marca, Guid.Parse(read["id"].ToString()));
                    typeof(Marca).GetProperty(nameof(Marca.Nome)).SetValue(marca, read["nome"].ToString());
                }
            }
            return marca;
        }

        public void Update(Marca marca)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string comandoSQL = "update marcas set Nome = @Nome where id = @id";
                SqlCommand command = new SqlCommand(comandoSQL, connection);
                command.CommandType = CommandType.Text;
                command.Parameters.AddWithValue("@id", marca.Id);
                command.Parameters.AddWithValue("@Nome", marca.Nome);
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
    }
}