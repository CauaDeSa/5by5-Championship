using _5by5_ChampionshipController.Entity;
using Microsoft.Data.SqlClient;

namespace _5by5_ChampionshipController.Bank
{
    public class ChampionshipBankController : BankController<Championship>
    {
        public ChampionshipBankController() : base() { }

        public override void Insert(Championship championship)
        {
            sqlCommand.CommandText = "INSERT INTO Championship(name, season, startDate, endDate) VALUES (@name, @season, @startDate, @endDate);";

            SqlParameter name = new("@name", System.Data.SqlDbType.VarChar, 30);
            SqlParameter season = new("@season", System.Data.SqlDbType.VarChar, 30);
            SqlParameter startDate = new("@startDate", System.Data.SqlDbType.Date);
            SqlParameter endDate = new("@endDate", System.Data.SqlDbType.Date);

            name.Value = championship.Name;
            season.Value = championship.Season;
            startDate.Value = championship.StartDate;
            endDate.Value = championship.EndDate;

            sqlCommand.Parameters.Add(name);
            sqlCommand.Parameters.Add(season);
            sqlCommand.Parameters.Add(startDate);
            sqlCommand.Parameters.Add(endDate);

            Query();
        }
        
        public void SetEndDate(string name, string season, DateOnly endDate)
        {
            sqlCommand.CommandText = "UPDATE Championship SET endDate = @endDate WHERE name = @name AND season = @season;";

            SqlParameter Name = new("@name", System.Data.SqlDbType.VarChar, 30);
            SqlParameter Season = new("@season", System.Data.SqlDbType.VarChar, 7);
            SqlParameter EndDate = new("@endDate", System.Data.SqlDbType.Date);

            Name.Value = name;
            Season.Value = season;
            EndDate.Value = endDate;

            sqlCommand.Parameters.Add(Name);
            sqlCommand.Parameters.Add(Season);
            sqlCommand.Parameters.Add(EndDate);

            Query();
        }

        public Championship GetByNameAndSeason(string name, string season)
        {
            sqlCommand.CommandText = "SELECT * FROM Championship WHERE name = @name AND season = @season;";
            SqlParameter Name = new("@name", System.Data.SqlDbType.VarChar, 30);
            SqlParameter Season = new("@season", System.Data.SqlDbType.VarChar, 7);
            
            Name.Value = name;
            Season.Value = season;

            sqlCommand.Parameters.Add(Name);
            sqlCommand.Parameters.Add(Season);

            using SqlDataReader reader = sqlCommand.ExecuteReader();
            {
                reader.Read();
                return new(reader.GetString(0), reader.GetString(1), DateOnly.Parse(reader.GetDateTime(2).ToString()), DateOnly.Parse(reader.GetDateTime(3).ToString()));
            }
        }

        public override List<Championship> GetAll()
        {
            List<Championship> championships = new();
            sqlCommand.CommandText = "SELECT * FROM Championship;";

            using SqlDataReader reader = sqlCommand.ExecuteReader();
                while (reader.Read())
                    championships.Add(new(reader.GetString(0), reader.GetString(1), DateOnly.Parse(reader.GetDateTime(2).ToString()), DateOnly.Parse(reader.GetDateTime(3).ToString())));

            return championships;
        }

        public void RemoveByNameAndSeason(string name, string season)
        {
            sqlCommand.CommandText = "DELETE FROM Championship WHERE name = @name AND season = @season;";
            SqlParameter Name = new("@name", System.Data.SqlDbType.VarChar, 30);
            SqlParameter Season = new("@season", System.Data.SqlDbType.VarChar, 7);

            Name.Value = name;
            Season.Value = season;

            sqlCommand.Parameters.Add(Name);
            sqlCommand.Parameters.Add(Season);

            Query();
        }

        public override void RemoveAll()
        {
            sqlCommand.CommandText = "DELETE FROM Championship;";

            Query();
        }
    }
}