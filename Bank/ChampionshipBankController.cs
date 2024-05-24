using _5by5_ChampionshipController.Entity;
using Microsoft.Data.SqlClient;
using System.Data;

namespace _5by5_ChampionshipController.Bank
{
    public class ChampionshipBankController : BankController<Championship>
    {
        public ChampionshipBankController() : base() { }

        public override bool Insert(Championship championship)
        {
            sqlCommand.Parameters.AddWithValue("@championshipName", SqlDbType.VarChar).Value = championship.Name;
            sqlCommand.Parameters.AddWithValue("@season", SqlDbType.VarChar).Value = championship.Season;
            sqlCommand.Parameters.AddWithValue("@startDate", SqlDbType.Date).Value = championship.StartDate;
            sqlCommand.Parameters.AddWithValue("@endDate", SqlDbType.Date).Value = championship.EndDate;

            return BooleanQuery("spCreateNewChampionship");
        }

        public bool SetEndDate(string championshipName, string cSeason) => SetEndDate(championshipName, cSeason, DateOnly.FromDateTime(DateTime.Now));

        public bool SetEndDate(string championshipName, string cSeason, DateOnly date)
        {
            sqlCommand.Parameters.AddWithValue("@championshipName", System.Data.SqlDbType.VarChar).Value = championshipName;
            sqlCommand.Parameters.AddWithValue("@season", System.Data.SqlDbType.VarChar).Value = cSeason;
            sqlCommand.Parameters.AddWithValue("@endDate", System.Data.SqlDbType.Date).Value = date;

            return BooleanQuery("spEndChampionship");
        }

        public Championship? GetByNameAndSeason(string championshipName, string season)
        {
            sqlCommand.Parameters.AddWithValue("@championshipName", System.Data.SqlDbType.VarChar).Value = championshipName;
            sqlCommand.Parameters.AddWithValue("@season", System.Data.SqlDbType.VarChar).Value = season;

            using SqlDataReader reader = ReadableQuery("spRetrieveChampionship");
                if (reader.Read())
                    return new(reader.GetString(0), reader.GetString(1), DateOnly.Parse(reader.GetDateTime(2).ToString()), DateOnly.Parse(reader.GetDateTime(3).ToString()));

            return null;
        }

        public override List<Championship> GetAll()
        {
            List<Championship> championships = new();

            using SqlDataReader reader = ReadableQuery("spRetrieveAllChampionships");
                while (reader.Read())
                    championships.Add(new(reader.GetString(0), reader.GetString(1), DateOnly.Parse(reader.GetDateTime(2).ToString()), DateOnly.Parse(reader.GetDateTime(3).ToString())));

            return championships;
        }

        public bool EndByNameAndSeason(string championshipName, string season) => EndByNameAndSeason(championshipName, season, DateOnly.FromDateTime(DateTime.Now));

        public bool EndByNameAndSeason(string championshipName, string season, DateOnly end)
        {
            sqlCommand.Parameters.AddWithValue("@championshipName", System.Data.SqlDbType.VarChar).Value = championshipName;
            sqlCommand.Parameters.AddWithValue("@season", System.Data.SqlDbType.VarChar).Value = season;
            sqlCommand.Parameters.AddWithValue("@endDate", System.Data.SqlDbType.Date).Value = end;

            return BooleanQuery("spEndChampionship");
        }
    }
}