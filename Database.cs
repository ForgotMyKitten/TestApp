using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp
{
    public class Database
    {
        private string connectionStr = String.Format("Server={0};Port={1};User Id={2};Password={3};Database={4};",
                "localhost", 5432, "postgres", "password", "db");
        private NpgsqlConnection npgSqlConnection;
        private NpgsqlCommand cmd;

        public void CreateConnection()
        {
            npgSqlConnection = new NpgsqlConnection(connectionStr);
            try
            {
                npgSqlConnection.Open();
            }
            catch (Exception ex)
            {
                throw new Exception("Не удалось подключиться к базе данных");
            }
        }

        public void CloseConnection()
        {
            try
            {
                npgSqlConnection.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("Не удалось закрыть сессию");
            }
        }

        public DataTable Refresh()
        {
            var dt = new DataTable();
            try
            {
                cmd = new NpgsqlCommand(@"select * from MyTable", npgSqlConnection);
                dt.Load(cmd.ExecuteReader());
            }
            catch (Exception ex)
            {
                throw new Exception("Не удалось загрузить данные в таблицу");
            }

            return dt;
        }

        public void Add(Data data)
        {
            try
            {
                cmd = new NpgsqlCommand("insert into MyTable(mt_a, mt_b, mt_sum) values(@a, @b, @sum)", npgSqlConnection);
                cmd.Parameters.AddWithValue("@a", data.A);
                cmd.Parameters.AddWithValue("@b", data.B);
                cmd.Parameters.AddWithValue("@sum", data.Sum);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception("Не удалось добавить данные в таблицу");
            }

            Refresh();
        }

        public void Delete(int id)
        {
            try
            {
                cmd = new NpgsqlCommand("delete from MyTable where mt_id = @id", npgSqlConnection);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception("Не удалось удалить данные из таблицы");
            }

            Refresh();
        }
    }
}
