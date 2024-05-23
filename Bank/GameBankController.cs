using _5by5_ChampionshipController.Entity;
using Microsoft.Data.SqlClient;

namespace _5by5_ChampionshipController.Bank
{
    public class GameBankController : BankController<Game>
    {
        public GameBankController() : base() { }

        public override void Insert(Game game)
        {
            sqlCommand.CommandText = "INSERT INTO Game(championship, season, visitor, home, homeGoals, visitorGoals) VALUES (@championship, @season, @visitor, @home, @homeGoals, @visitorGoals);";

            SqlParameter championship = new("@championship", System.Data.SqlDbType.VarChar, 30);
            SqlParameter season = new("@season", System.Data.SqlDbType.VarChar, 30);
            SqlParameter visitor = new("@visitor", System.Data.SqlDbType.VarChar, 30);
            SqlParameter home = new("@home", System.Data.SqlDbType.VarChar, 30);
            SqlParameter hGoals = new("@homeGoals", System.Data.SqlDbType.Int);
            SqlParameter vGoals = new("@visitorGoals", System.Data.SqlDbType.Int);

            championship.Value = game.Championship;
            season.Value = game.Season;
            visitor.Value = game.Visitor;
            home.Value = game.Home;
            hGoals.Value = game.HGoals;
            vGoals.Value = game.VGoals;

            sqlCommand.Parameters.Add(championship);
            sqlCommand.Parameters.Add(season);
            sqlCommand.Parameters.Add(visitor);
            sqlCommand.Parameters.Add(home);
            sqlCommand.Parameters.Add(vGoals);
            sqlCommand.Parameters.Add(hGoals);

            Query();
        }

        public override Game GetByName(string name)
        {
            sqlCommand.CommandText = "DELETE FROM Game WHERE name = @name;";

            SqlParameter Name = new("@name", System.Data.SqlDbType.VarChar, 30);
            Name.Value = name;

            sqlCommand.Parameters.Add(Name);

            Query();
        }

        public override List<Game> GetAll()
        {
            List<Game> list = new List<Game>();

            sqlCommand.CommandText = "SELECT * FROM Game;";

            using SqlDataReader reader = sqlCommand.ExecuteReader();
                while (reader.Read())
                    list.Add(new Game(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetInt32(4), reader.GetInt32(5)));

            return list;
        }

        public override void RemoveByName(string id)
        {
            sqlCommand.CommandText = "DELETE FROM Game WHERE name = @name;";
            SqlParameter Name = new("@name", System.Data.SqlDbType.VarChar, 30);

            Name.Value = id;

            sqlCommand.Parameters.Add(Name);

            Query();
        }

        public override void RemoveAll()
        {
            sqlCommand.CommandText = "DELETE FROM Game;";

            Query();
        }
    }
}