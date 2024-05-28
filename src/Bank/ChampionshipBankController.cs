using _5by5_ChampionshipController.src.Entity;
using Microsoft.Data.SqlClient;
using System.Data;

namespace _5by5_ChampionshipController.src.Bank
{
    public class ChampionshipBankController : BankController<Championship>
    {
        public ChampionshipBankController() : base() 
        {
            sqlCommand.CommandText = "spInitializeChampionship";
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            sqlCommand.Connection = sqlConnection;

            sqlConnection.Open();

            sqlCommand.ExecuteNonQuery();

            sqlCommand.Parameters.Clear();
            sqlConnection.Close();
        }

        public bool HasChampionship(string championshipName, string season)
        {
            sqlCommand.CommandText = "spHasChampionship";
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

            sqlCommand.Parameters.AddWithValue("@name", SqlDbType.VarChar).Value = championshipName;
            sqlCommand.Parameters.AddWithValue("@season", SqlDbType.VarChar).Value = season;
            sqlCommand.Parameters.Add("@bool", SqlDbType.Int).Direction = ParameterDirection.Output;

            sqlCommand.Connection = sqlConnection;

            sqlConnection.Open();

            sqlCommand.ExecuteNonQuery();

            bool result = (int)sqlCommand.Parameters["@bool"].Value == 1;

            sqlCommand.Parameters.Clear();
            sqlConnection.Close();

            return result;
        }

        public override bool Insert(Championship championship)
        {
            sqlCommand.Parameters.AddWithValue("@name", SqlDbType.VarChar).Value = championship.Name;
            sqlCommand.Parameters.AddWithValue("@season", SqlDbType.VarChar).Value = championship.Season;
            sqlCommand.Parameters.AddWithValue("@startDate", SqlDbType.Date).Value = championship.StartDate;

            return BooleanQuery("spCreateNewChampionship");
        }

        public bool SetEndDate(string championshipName, string cSeason) => SetEndDate(championshipName, cSeason, DateOnly.FromDateTime(DateTime.Now));

        public bool SetEndDate(string championshipName, string cSeason, DateOnly date)
        {
            sqlCommand.Parameters.AddWithValue("@name", SqlDbType.VarChar).Value = championshipName;
            sqlCommand.Parameters.AddWithValue("@season", SqlDbType.VarChar).Value = cSeason;
            sqlCommand.Parameters.AddWithValue("@endDate", SqlDbType.Date).Value = date;

            return BooleanQuery("spEndChampionship");
        }

        public Championship? GetByNameAndSeason(string championshipName, string season)
        {
            Championship? aux = null;

            sqlCommand.CommandText = "spRetrieveChampionship";
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

            sqlCommand.Parameters.AddWithValue("@name", SqlDbType.VarChar).Value = championshipName;
            sqlCommand.Parameters.AddWithValue("@season", SqlDbType.VarChar).Value = season;

            sqlCommand.Connection = sqlConnection;

            sqlConnection.Open();

            using SqlDataReader reader = sqlCommand.ExecuteReader();
            if (reader.Read())
            {
                aux = new(reader.GetString(0), reader.GetString(1), DateOnly.FromDateTime(reader.GetDateTime(2)));

                if (!reader.IsDBNull(3))
                    aux.EndDate = DateOnly.FromDateTime(reader.GetDateTime(3));
                else
                    aux.EndDate = null;
            }

            sqlCommand.Parameters.Clear();
            sqlConnection.Close();

            return aux;
        }

        public List<Championship> GetAll()
        {
            List<Championship> list = new();
            SqlDataReader reader;

            sqlCommand.CommandText = "spRetrieveAllChampionships";
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            sqlCommand.Connection = sqlConnection;

            sqlConnection.Open();
            try
            {
                reader = sqlCommand.ExecuteReader();
            } catch (Exception)
            {
                sqlCommand.Parameters.Clear();
                sqlConnection.Close();
                return null;
            }

            while (reader.Read())
            {
                Championship aux = new(reader.GetString(0), reader.GetString(1), DateOnly.FromDateTime(reader.GetDateTime(2)));

                if (!reader.IsDBNull(3))
                    aux.EndDate = DateOnly.FromDateTime(reader.GetDateTime(3));
                else
                    aux.EndDate = null;

                list.Add(aux);
            }
            
            sqlCommand.Parameters.Clear();
            sqlConnection.Close();

            return list;
        }
    }
}