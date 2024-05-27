using _5by5_ChampionshipController.src.Entity;
using Microsoft.Data.SqlClient;

namespace _5by5_ChampionshipController.src.Bank
{
    internal class TeamBankController : BankController<Team>
    {
        public TeamBankController() : base() { }

        public override bool Insert(Team team)
        {
            sqlCommand.Parameters.AddWithValue("@name", System.Data.SqlDbType.VarChar).Value = team.Name;
            sqlCommand.Parameters.AddWithValue("@nickname", System.Data.SqlDbType.VarChar).Value = team.Nickname;
            sqlCommand.Parameters.AddWithValue("@creationDate", System.Data.SqlDbType.Date).Value = team.CreationDate;

            return BooleanQuery("spCreateTeam");
        }

        public Team? GetByName(string name)
        {
            sqlCommand.Parameters.AddWithValue("@name", System.Data.SqlDbType.VarChar).Value = name;

            using SqlDataReader reader = ReadableQuery("spRetrieveTeam");
            if (reader.Read())
                return new Team(name, reader.GetString(1), DateOnly.Parse(reader.GetDateTime(2).ToString()));

            return null;
        }

        public override List<Team> GetAll()
        {
            List<Team> list = new();

            using SqlDataReader reader = ReadableQuery("spRetrieveAllTeams");
            while (reader.Read())
                list.Add(new Team(reader.GetString(0), reader.GetString(1), DateOnly.Parse(reader.GetDateTime(2).ToString())));

            return list;
        }

        public bool UpdateTeam(string name, string nickname, DateOnly creationDate)
        {
            sqlCommand.Parameters.AddWithValue("@name", System.Data.SqlDbType.VarChar).Value = name;
            sqlCommand.Parameters.AddWithValue("@nickname", System.Data.SqlDbType.VarChar).Value = nickname;
            sqlCommand.Parameters.AddWithValue("@creationDate", System.Data.SqlDbType.Date).Value = creationDate;

            return BooleanQuery("spUpdateTeam");
        }

        public bool RemoveByName(string name)
        {
            sqlCommand.Parameters.AddWithValue("@name", System.Data.SqlDbType.VarChar).Value = name;

            return BooleanQuery("spDeleteTeam");
        }

        public bool ChangeActivityStatus(string teamName)
        {
            sqlCommand.Parameters.AddWithValue("@name", System.Data.SqlDbType.VarChar).Value = teamName;

            return BooleanQuery("spChangeSituation");
        }
    }
}