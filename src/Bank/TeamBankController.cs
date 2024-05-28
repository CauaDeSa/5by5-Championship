using _5by5_ChampionshipController.src.Entity;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using System.Reflection.PortableExecutable;

namespace _5by5_ChampionshipController.src.Bank
{
    internal class TeamBankController : BankController<Team>
    {
        public TeamBankController() : base() 
        {
            sqlCommand.CommandText = "spInitializeTeam";
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

            sqlCommand.Connection = sqlConnection;

            sqlConnection.Open();

            sqlCommand.ExecuteNonQuery();
            sqlCommand.Parameters.Clear();
            sqlConnection.Close();
        }

        public bool nameIsUsed(string name)
        {
            sqlCommand.CommandText = "spNameIsUsed";
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            sqlCommand.Connection = sqlConnection;
            
            sqlCommand.Parameters.AddWithValue("@name", System.Data.SqlDbType.VarChar).Value = name;
            sqlCommand.Parameters.Add("@bool", SqlDbType.Int).Direction = ParameterDirection.Output;

            sqlConnection.Open();

            sqlCommand.ExecuteNonQuery();

            bool result = (int)sqlCommand.Parameters["@bool"].Value == 1;

            sqlCommand.Parameters.Clear();
            sqlConnection.Close();

            return result;
        }

        public bool nicknameIsUsed(string nickname)
        {
            sqlCommand.CommandText = "spNicknameIsUsed";
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            sqlCommand.Connection = sqlConnection;

            sqlCommand.Parameters.AddWithValue("@nickname", System.Data.SqlDbType.VarChar).Value = nickname;
            sqlCommand.Parameters.Add("@bool", SqlDbType.Int).Direction = ParameterDirection.Output;

            sqlConnection.Open();

            sqlCommand.ExecuteNonQuery();

            bool result = (int)sqlCommand.Parameters["@bool"].Value == 1;

            sqlCommand.Parameters.Clear();
            sqlConnection.Close();

            return result;
        }

        public override bool Insert(Team team)
        {
            sqlCommand.Parameters.AddWithValue("@name", System.Data.SqlDbType.VarChar).Value = team.Name;
            sqlCommand.Parameters.AddWithValue("@nickname", System.Data.SqlDbType.VarChar).Value = team.Nickname;
            sqlCommand.Parameters.AddWithValue("@creationDate", System.Data.SqlDbType.Date).Value = team.CreationDate;

            return BooleanQuery("spCreateTeam");
        }

        public Team? RetrieveByName(string name)
        {
            sqlCommand.CommandText = "spRetrieveTeam";
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            sqlCommand.Connection = sqlConnection;

            sqlCommand.Parameters.AddWithValue("@name", System.Data.SqlDbType.VarChar).Value = name;

            sqlConnection.Open();

            SqlDataReader reader;

            try
            {
                reader = sqlCommand.ExecuteReader();
            }
            catch (Exception)
            {
                sqlCommand.Parameters.Clear();
                sqlConnection.Close();
                return null;
            }

            if (!reader.Read())
            {
                sqlCommand.Parameters.Clear();
                sqlConnection.Close();
                return null;
            }

            Team t = new(name, reader.GetString(1), DateOnly.FromDateTime(reader.GetDateTime(2)));
            
            sqlCommand.Parameters.Clear();
            sqlConnection.Close();

            return t;
        }

        public List<Team> GetAll()
        {
            List<Team> list = new();

            sqlCommand.CommandText = "spRetrieveAllTeams";
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            sqlCommand.Connection = sqlConnection;

            sqlConnection.Open();

            SqlDataReader reader = sqlCommand.ExecuteReader();

            while (reader.Read())
                list.Add(new Team(reader.GetString(0), reader.GetString(1), DateOnly.FromDateTime(reader.GetDateTime(2))));

            sqlCommand.Parameters.Clear();
            sqlConnection.Close();

            return list;
        }
    }
}