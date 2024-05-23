using _5by5_ChampionshipController.Entity;
using Microsoft.Data.SqlClient;

namespace _5by5_ChampionshipController.Bank
{
    internal class StatsBankController : BankController<Stats>
    {
        public StatsBankController() : base() { }

        public override void Insert(Stats obj)
        {
            sqlCommand.CommandText = "INSERT INTO Stats(teamName, championshipName, season, pontuation, scoredGoals, sufferedGoals) VALUES (@teamName, @championshipName, @season, @pontuation, @scoredGoals, @sufferedGoals);";

            SqlParameter teamName = new("@teamName", System.Data.SqlDbType.VarChar, 30);
            SqlParameter championshipName = new("@championshipName", System.Data.SqlDbType.VarChar, 30);
            SqlParameter season = new("@season", System.Data.SqlDbType.VarChar, 30);
            SqlParameter pontuation = new("@pontuation", System.Data.SqlDbType.Int);
            SqlParameter scoredGoals = new("@scoredGoals", System.Data.SqlDbType.Int);
            SqlParameter sufferedGoals = new("@sufferedGoals", System.Data.SqlDbType.Int);

            teamName.Value = obj.Tname;
            championshipName.Value = obj.Cname;
            season.Value = obj.Season;
            pontuation.Value = obj.Pontuation;
            scoredGoals.Value = obj.ScoredGoals;
            sufferedGoals.Value = obj.SufferedGoals;

            sqlCommand.Parameters.Add(teamName);
            sqlCommand.Parameters.Add(championshipName);
            sqlCommand.Parameters.Add(season);
            sqlCommand.Parameters.Add(pontuation);
            sqlCommand.Parameters.Add(scoredGoals);
            sqlCommand.Parameters.Add(sufferedGoals);
            
            Query();
        }
        
        public void SetPontuation(string tName, string cName, string cSeason, int pontuation)
        {
            sqlCommand.CommandText = "UPDATE Stats SET pontuation = @pontuation WHERE teamName = @teamName AND championshipName = @championshipName AND season = @season;";

            SqlParameter teamName = new("@teamName", System.Data.SqlDbType.VarChar, 30);
            SqlParameter championshipName = new("@championshipName", System.Data.SqlDbType.VarChar, 30);
            SqlParameter season = new("@season", System.Data.SqlDbType.VarChar, 30);
            SqlParameter Pontuation = new("@pontuation", System.Data.SqlDbType.Int);

            teamName.Value = tName;
            championshipName.Value = cName;
            season.Value = cSeason;
            Pontuation.Value = pontuation;

            sqlCommand.Parameters.Add(teamName);
            sqlCommand.Parameters.Add(championshipName);
            sqlCommand.Parameters.Add(season);
            sqlCommand.Parameters.Add(Pontuation);

            Query();
        }   

        public void SetScoredGoals(string tName, string cName, string cSeason, int scoredGoals)
        {
            sqlCommand.CommandText = "UPDATE Stats SET scoredGoals = @scoredGoals WHERE teamName = @teamName AND championshipName = @championshipName AND season = @season;";

            SqlParameter teamName = new("@teamName", System.Data.SqlDbType.VarChar, 30);
            SqlParameter championshipName = new("@championshipName", System.Data.SqlDbType.VarChar, 30);
            SqlParameter season = new("@season", System.Data.SqlDbType.VarChar, 30);
            SqlParameter ScoredGoals = new("@scoredGoals", System.Data.SqlDbType.Int);

            teamName.Value = tName;
            championshipName.Value = cName;
            season.Value = cSeason;
            ScoredGoals.Value = scoredGoals;

            sqlCommand.Parameters.Add(teamName);
            sqlCommand.Parameters.Add(championshipName);
            sqlCommand.Parameters.Add(season);
            sqlCommand.Parameters.Add(ScoredGoals);

            Query();
        }

        public void SetSufferedGoals(string tName, string cName, string cSeason, int sufferedGoals)
        {
            sqlCommand.CommandText = "UPDATE Stats SET sufferedGoals = @sufferedGoals WHERE teamName = @teamName AND championshipName = @championshipName AND season = @season;";

            SqlParameter teamName = new("@teamName", System.Data.SqlDbType.VarChar, 30);
            SqlParameter championshipName = new("@championshipName", System.Data.SqlDbType.VarChar, 30);
            SqlParameter season = new("@season", System.Data.SqlDbType.VarChar, 30);
            SqlParameter SufferedGoals = new("@sufferedGoals", System.Data.SqlDbType.Int);

            teamName.Value = tName;
            championshipName.Value = cName;
            season.Value = cSeason;
            SufferedGoals.Value = sufferedGoals;

            sqlCommand.Parameters.Add(teamName);
            sqlCommand.Parameters.Add(championshipName);
            sqlCommand.Parameters.Add(season);
            sqlCommand.Parameters.Add(SufferedGoals);

            Query();
        }

        public override Stats GetByTeamAndChampionship(string tName, string cName, string cSeason)
        {
            sqlCommand.CommandText = "SELECT * FROM Stats WHERE teamName = @teamName AND championshipName = @championshipName AND season = @season;";
            SqlParameter teamName = new("@teamName", System.Data.SqlDbType.VarChar, 30);
            SqlParameter championshipName = new("@championshipName", System.Data.SqlDbType.VarChar, 30);
            SqlParameter season = new("@season", System.Data.SqlDbType.VarChar, 30);

            teamName.Value = tName;
            championshipName.Value = cName;
            season.Value = cSeason;

            sqlCommand.Parameters.Add(teamName);
            sqlCommand.Parameters.Add(championshipName);
            sqlCommand.Parameters.Add(season);

            using SqlDataReader reader = sqlCommand.ExecuteReader();
            {
                reader.Read();
                return new Stats(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetInt32(3), reader.GetInt32(4), reader.GetInt32(5));
            }
        }

        public override List<Stats> GetAll()
        {
            sqlCommand.CommandText = "SELECT * FROM Stats;";
            List<Stats> list = new();

            using SqlDataReader reader = sqlCommand.ExecuteReader();
            while (reader.Read())
                list.Add(new Stats(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetInt32(3), reader.GetInt32(4), reader.GetInt32(5));)

            return list;
        }

        public override void RemoveByTeamAndChampionship(string tName, string cName, string cSeason)
        {
            sqlCommand.CommandText = "DELETE FROM Stats WHERE teamName = @teamName AND championshipName = @championshipName AND season = @season;";
            SqlParameter teamName = new("@teamName", System.Data.SqlDbType.VarChar, 30);
            SqlParameter championshipName = new("@championshipName", System.Data.SqlDbType.VarChar, 30);
            SqlParameter season = new("@season", System.Data.SqlDbType.VarChar, 30);

            teamName.Value = tName;
            championshipName.Value = cName;
            season.Value = cSeason;

            sqlCommand.Parameters.Add(teamName);
            sqlCommand.Parameters.Add(championshipName);
            sqlCommand.Parameters.Add(season);

            Query();

        }

        public override void RemoveAll()
        {
            sqlCommand.CommandText = "DELETE FROM Stats;";
            Query();
        }
    }
}