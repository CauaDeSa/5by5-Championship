using _5by5_ChampionshipController.Entity;
using Microsoft.Data.SqlClient;

namespace _5by5_ChampionshipController.Bank
{
    internal class TeamBankController : BankController<Team>
    {
        public TeamBankController() : base () { }

        public override void Insert(Team team)
        {
            sqlCommand.CommandText = "INSERT INTO Team(name, nickname, creationDate, pontuation, scoredGoals, sufferedGoals) VALUES (@name, @nickname, @creationDate, @pontuation, @scoredGoals, @sufferedGoals);";

            SqlParameter name = new("@name", System.Data.SqlDbType.VarChar, 30);
            SqlParameter nickname = new("@nickname", System.Data.SqlDbType.VarChar, 30);
            SqlParameter creationDate = new("@creationDate", System.Data.SqlDbType.Date);

            name.Value = team.Name;
            nickname.Value = team.Nickname;
            creationDate.Value = team.CreationDate;

            sqlCommand.Parameters.Add(name);
            sqlCommand.Parameters.Add(nickname);
            sqlCommand.Parameters.Add(creationDate);

            Query();
        }

        public override Team GetByName(string name)
        {
            sqlCommand.CommandText = "SELECT * FROM Team WHERE name = @name;";

            SqlParameter Name = new("@name", System.Data.SqlDbType.VarChar, 30);
            Name.Value = name;

            sqlCommand.Parameters.Add(Name);

            using SqlDataReader reader = sqlCommand.ExecuteReader();
            {
                reader.Read();
                return new Team(reader.GetString(0), reader.GetString(1), DateOnly.Parse(reader.GetDateTime(2).ToString()));
            }
        }

        public override List<Team> GetAll()
        {
            sqlCommand.CommandText = "SELECT * FROM Team;";

            List<Team> list = new();

            using SqlDataReader reader = sqlCommand.ExecuteReader();
                while (reader.Read()) 
                    list.Add(new Team(reader.GetString(0), reader.GetString(1), DateOnly.Parse(reader.GetDateTime(2).ToString())));

            return list;
        }

        public override void RemoveByName(string name)
        {
            sqlCommand.CommandText = "DELETE FROM Team WHERE name = @name;";

            SqlParameter Name = new("@name", System.Data.SqlDbType.VarChar, 30);
            Name.Value = name;

            sqlCommand.Parameters.Add(Name);

            Query();
        }

        public override void RemoveAll()
        {
            sqlCommand.CommandText = "DELETE FROM Team;";

            Query();
        }
    }
}