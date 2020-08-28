using Patrimonios.Domain.Entities;
using Patrimonios.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Patrimonios.Infra.Repositories
{
    public class PatrimonioRepository : IPatrimonioRepository
    {
        private string connectionString = @"Data Source=MARQUES-PC\SQLEXPRESS;Initial Catalog=Ibope;Integrated Security=True;";

        public void Add(Patrimonio patrimonio)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string comandoSQL = "insert into patrimonios(id, nome, marca_id, descricao, numero_do_tombo) values(@id, @nome, @marcaId, @descricao, @numeroDoTombo)";
                SqlCommand command = new SqlCommand(comandoSQL, connection);

                command.CommandType = CommandType.Text;
                command.Parameters.AddWithValue("@id", patrimonio.Id);
                command.Parameters.AddWithValue("@nome", patrimonio.Nome);
                command.Parameters.AddWithValue("@marcaId", patrimonio.MarcaId);
                command.Parameters.AddWithValue("@descricao", ((object)patrimonio.Descricao) ?? DBNull.Value);
                command.Parameters.AddWithValue("@numeroDoTombo", patrimonio.NumeroDoTombo);
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        public void Delete(Guid id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string comandoSQL = "delete from patrimonios where id = @id";
                SqlCommand command = new SqlCommand(comandoSQL, connection);
                command.CommandType = CommandType.Text;
                command.Parameters.AddWithValue("@id", id);
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        public IEnumerable<Patrimonio> GetAll()
        {
            List<Patrimonio> patrimonios = new List<Patrimonio>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "select id, nome, marca_id, descricao, numero_do_tombo from patrimonios";

                SqlCommand command = new SqlCommand(query, connection);
                command.CommandType = CommandType.Text;

                connection.Open();
                SqlDataReader read = command.ExecuteReader();

                while (read.Read())
                {
                    Patrimonio marca = Patrimonio.Create();
                    typeof(Patrimonio).GetProperty(nameof(Patrimonio.Id)).SetValue(marca, Guid.Parse(read["id"].ToString()));
                    typeof(Patrimonio).GetProperty(nameof(Patrimonio.Nome)).SetValue(marca, read["nome"].ToString());
                    typeof(Patrimonio).GetProperty(nameof(Patrimonio.MarcaId)).SetValue(marca, Guid.Parse(read["marca_id"].ToString()));
                    typeof(Patrimonio).GetProperty(nameof(Patrimonio.Descricao)).SetValue(marca, read["descricao"].ToString());
                    typeof(Patrimonio).GetProperty(nameof(Patrimonio.NumeroDoTombo)).SetValue(marca, read["numero_do_tombo"].ToString());
                }
                connection.Close();
            }

            return patrimonios;
        }

        public Patrimonio GetById(Guid id)
        {
            Patrimonio marca = null;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sqlQuery = "select id, nome, marca_id, descricao, numero_do_tombo from patrimonios where id = @id";
                SqlCommand command = new SqlCommand(sqlQuery, connection);
                command.CommandType = CommandType.Text;
                command.Parameters.AddWithValue("@id", id);

                connection.Open();
                SqlDataReader read = command.ExecuteReader();
                while (read.Read())
                {
                    marca = Patrimonio.Create();
                    typeof(Patrimonio).GetProperty(nameof(Patrimonio.Id)).SetValue(marca, Guid.Parse(read["id"].ToString()));
                    typeof(Patrimonio).GetProperty(nameof(Patrimonio.Nome)).SetValue(marca, read["nome"].ToString());
                    typeof(Patrimonio).GetProperty(nameof(Patrimonio.MarcaId)).SetValue(marca, Guid.Parse(read["marca_id"].ToString()));
                    typeof(Patrimonio).GetProperty(nameof(Patrimonio.Descricao)).SetValue(marca, read["descricao"].ToString());
                    typeof(Patrimonio).GetProperty(nameof(Patrimonio.NumeroDoTombo)).SetValue(marca, read["numero_do_tombo"].ToString());
                }
            }
            return marca;
        }

        public IEnumerable<Patrimonio> GetAllFromMarcaId(Guid marcaId)
        {
            List<Patrimonio> patrimonios = new List<Patrimonio>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "select id, nome, marca_id, descricao, numero_do_tombo from patrimonios where marca_id = @marcaId";

                SqlCommand command = new SqlCommand(query, connection);
                command.CommandType = CommandType.Text;
                command.Parameters.AddWithValue("@marcaId", marcaId);

                connection.Open();
                SqlDataReader read = command.ExecuteReader();

                while (read.Read())
                {
                    Patrimonio marca = Patrimonio.Create();
                    typeof(Patrimonio).GetProperty(nameof(Patrimonio.Id)).SetValue(marca, Guid.Parse(read["id"].ToString()));
                    typeof(Patrimonio).GetProperty(nameof(Patrimonio.Nome)).SetValue(marca, read["nome"].ToString());
                    typeof(Patrimonio).GetProperty(nameof(Patrimonio.MarcaId)).SetValue(marca, Guid.Parse(read["marca_id"].ToString()));
                    typeof(Patrimonio).GetProperty(nameof(Patrimonio.Descricao)).SetValue(marca, read["descricao"].ToString());
                    typeof(Patrimonio).GetProperty(nameof(Patrimonio.NumeroDoTombo)).SetValue(marca, read["numero_do_tombo"].ToString());
                }
                connection.Close();
            }

            return patrimonios;
        }

        public Patrimonio GetByName(string nome)
        {
            Patrimonio patrimonio = null;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sqlQuery = "select id, nome, marca_id, descricao, numero_do_tombo from patrimonios where nome = @nome";
                SqlCommand command = new SqlCommand(sqlQuery, connection);
                command.Parameters.AddWithValue("@nome", nome);
                connection.Open();
                SqlDataReader read = command.ExecuteReader();
                while (read.Read())
                {
                    patrimonio = Patrimonio.Create();
                    typeof(Patrimonio).GetProperty(nameof(Patrimonio.Id)).SetValue(patrimonio, Guid.Parse(read["id"].ToString()));
                    typeof(Patrimonio).GetProperty(nameof(Patrimonio.Nome)).SetValue(patrimonio, read["nome"].ToString());
                    typeof(Patrimonio).GetProperty(nameof(Patrimonio.MarcaId)).SetValue(patrimonio, Guid.Parse(read["marca_id"].ToString()));
                    typeof(Patrimonio).GetProperty(nameof(Patrimonio.Descricao)).SetValue(patrimonio, read["descricao"].ToString());
                    typeof(Patrimonio).GetProperty(nameof(Patrimonio.NumeroDoTombo)).SetValue(patrimonio, read["numero_do_tombo"].ToString());
                }
            }
            return patrimonio;
        }

        public void Update(Patrimonio patrimonio)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string comandoSQL = "update patrimonios set nome = @nome, marca_id = @marcaId, descricao = @descricao where id = @id";
                SqlCommand command = new SqlCommand(comandoSQL, connection);
                command.CommandType = CommandType.Text;
                command.Parameters.AddWithValue("@id", patrimonio.Id);
                command.Parameters.AddWithValue("@nome", patrimonio.Nome);
                command.Parameters.AddWithValue("@marcaId", patrimonio.MarcaId);
                command.Parameters.AddWithValue("@descricao", ((object)patrimonio.Descricao) ?? DBNull.Value);
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
    }
}
