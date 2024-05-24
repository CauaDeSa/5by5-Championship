using _5by5_ChampionshipController.Entity;
using Microsoft.Data.SqlClient;

namespace _5by5_ChampionshipController.Bank
{
    internal class StatsBankController : BankController<Stats>
    {
        public StatsBankController() : base() { }

        public override bool Insert(Stats stats)
        {
            sqlCommand.Parameters.AddWithValue("@teamName", System.Data.SqlDbType.VarChar).Value = stats.Tname;
            sqlCommand.Parameters.AddWithValue("@championshipName", System.Data.SqlDbType.VarChar).Value = stats.Cname;
            sqlCommand.Parameters.AddWithValue("@season", System.Data.SqlDbType.VarChar).Value = stats.Season;
            sqlCommand.Parameters.AddWithValue("@pontuation", System.Data.SqlDbType.Int).Value = stats.Pontuation;
            sqlCommand.Parameters.AddWithValue("@scoredGoals", System.Data.SqlDbType.Int).Value = stats.ScoredGoals;
            sqlCommand.Parameters.AddWithValue("@sufferedGoals", System.Data.SqlDbType.Int).Value = stats.SufferedGoals;
            
            return BooleanQuery("spCreateStats");
        }
        
        public Stats? RetrieveStatsFromTeamAndChampionship(string teamName, string championshipName, string season)
        {
            sqlCommand.Parameters.AddWithValue("@teamName", System.Data.SqlDbType.VarChar).Value = teamName;
            sqlCommand.Parameters.AddWithValue("@championshipName", System.Data.SqlDbType.VarChar).Value = championshipName;
            sqlCommand.Parameters.AddWithValue("@season", System.Data.SqlDbType.VarChar).Value = season;

            using SqlDataReader reader = ReadableQuery("spRetrieveStats");
                if (reader.Read())
                    return new Stats(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetInt32(3), reader.GetInt32(4), reader.GetInt32(5));
            
            return null;
        }

        public override List<Stats> GetAll()
        {
            List<Stats> list = new();

            using SqlDataReader reader = ReadableQuery("spRetrieveAllStats");
            while (reader.Read())
                list.Add(new Stats(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetInt32(3), reader.GetInt32(4), reader.GetInt32(5)));

            return list;
        }

        public bool UpdateTeamStats(string teamName, string championshipName, string season)
        {
            sqlCommand.Parameters.AddWithValue("@teamName", System.Data.SqlDbType.VarChar).Value = teamName;
            sqlCommand.Parameters.AddWithValue("@championshipName", System.Data.SqlDbType.VarChar).Value = championshipName;
            sqlCommand.Parameters.AddWithValue("@season", System.Data.SqlDbType.VarChar).Value = season;

            return BooleanQuery("spUpdateTeamStats");
        }   

        public bool DeleteStatsFromTeamAndChampionship(string teamName, string championshipName, string season)
        {
            sqlCommand.Parameters.AddWithValue("@teamName", System.Data.SqlDbType.VarChar).Value = teamName;
            sqlCommand.Parameters.AddWithValue("@championshipName", System.Data.SqlDbType.VarChar).Value = championshipName;
            sqlCommand.Parameters.AddWithValue("@season", System.Data.SqlDbType.VarChar).Value = season;

            return BooleanQuery("spDeleteStats");
        }
    }
}