using _5by5_ChampionshipController.src.Entity;
using Microsoft.Data.SqlClient;

namespace _5by5_ChampionshipController.src.Bank
{
    internal class StatsBankController : BankController<Stats>
    {
        public StatsBankController() : base() 
        {
            sqlCommand.CommandText = "spInitializeStats";
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

            sqlCommand.Connection = sqlConnection;

            sqlConnection.Open();

            sqlCommand.ExecuteNonQuery();
            sqlCommand.Parameters.Clear();
            sqlConnection.Close();
        }

        public override bool Insert(Stats stats)
        {
            sqlCommand.Parameters.AddWithValue("@teamName", System.Data.SqlDbType.VarChar).Value = stats.Tname;
            sqlCommand.Parameters.AddWithValue("@championshipName", System.Data.SqlDbType.VarChar).Value = stats.Cname;
            sqlCommand.Parameters.AddWithValue("@season", System.Data.SqlDbType.VarChar).Value = stats.Season;
            sqlCommand.Parameters.AddWithValue("@pontuation", System.Data.SqlDbType.Int).Value = 0;
            sqlCommand.Parameters.AddWithValue("@scoredGoals", System.Data.SqlDbType.Int).Value = stats.ScoredGoals;
            sqlCommand.Parameters.AddWithValue("@sufferedGoals", System.Data.SqlDbType.Int).Value = stats.SufferedGoals;

            return BooleanQuery("spCreateStats");
        }

        public List<Stats>? RetrieveStatsByChampionship(string championshipName, string season)
        {
            List<Stats> list = new();

            sqlCommand.CommandText = "spRetrieveByChampionshipAndSeason";
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

            sqlCommand.Parameters.AddWithValue("@championship", System.Data.SqlDbType.VarChar).Value = championshipName;
            sqlCommand.Parameters.AddWithValue("@season", System.Data.SqlDbType.VarChar).Value = season;

            sqlCommand.Connection = sqlConnection;

            sqlConnection.Open();

            SqlDataReader reader = sqlCommand.ExecuteReader();

            while (reader.Read())
                list.Add(new Stats(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetInt32(3), reader.GetInt32(4), reader.GetInt32(5)));

            sqlCommand.Parameters.Clear();
            sqlConnection.Close();

            return list;
        }

        public bool UpdateTeamStats(string teamName, string nickname, string championshipName, string season)
        {
            sqlCommand.Parameters.AddWithValue("@name", System.Data.SqlDbType.VarChar).Value = teamName;
            sqlCommand.Parameters.AddWithValue("@nickname", System.Data.SqlDbType.VarChar).Value = nickname;
            sqlCommand.Parameters.AddWithValue("@championshipName", System.Data.SqlDbType.VarChar).Value = championshipName;
            sqlCommand.Parameters.AddWithValue("@season", System.Data.SqlDbType.VarChar).Value = season;

            return BooleanQuery("spUpdateTeamStats");
        }
    }
}