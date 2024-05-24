using _5by5_ChampionshipController.Entity;
using Microsoft.Data.SqlClient;

namespace _5by5_ChampionshipController.Bank
{
    public class GameBankController : BankController<Game>
    {
        public GameBankController() : base() { }

        public override bool Insert(Game game)
        {
            sqlCommand.Parameters.AddWithValue("@championship", System.Data.SqlDbType.VarChar).Value = game.Championship;
            sqlCommand.Parameters.AddWithValue("@season", System.Data.SqlDbType.VarChar).Value = game.Season;
            sqlCommand.Parameters.AddWithValue("@visitor", System.Data.SqlDbType.VarChar).Value = game.Season;
            sqlCommand.Parameters.AddWithValue("@home", System.Data.SqlDbType.VarChar).Value = game.Home;
            sqlCommand.Parameters.AddWithValue("@homeGoals", System.Data.SqlDbType.Int).Value = game.HGoals;
            sqlCommand.Parameters.AddWithValue("@visitorGoals", System.Data.SqlDbType.Int).Value = game.VGoals;

            return BooleanQuery("spCreateGame");
        }

        public bool UpdateGoals(string championship, string season, string home, string visitor, int hGgoals, int vGoals)
        {
            sqlCommand.Parameters.AddWithValue("@championship", System.Data.SqlDbType.VarChar).Value = championship;
            sqlCommand.Parameters.AddWithValue("@season", System.Data.SqlDbType.Int).Value = season;
            sqlCommand.Parameters.AddWithValue("@home", System.Data.SqlDbType.VarChar).Value = home;
            sqlCommand.Parameters.AddWithValue("@visitor", System.Data.SqlDbType.VarChar).Value = visitor;
            sqlCommand.Parameters.AddWithValue("@homeGoals", System.Data.SqlDbType.Int).Value = hGgoals;
            sqlCommand.Parameters.AddWithValue("@visitorGoals", System.Data.SqlDbType.Int).Value = vGoals;

            return BooleanQuery("spUpdateGoals");
        }

        public Game? GetByName(string championship, string season, string home, string visitor)
        {
            sqlCommand.Parameters.AddWithValue("@championship", System.Data.SqlDbType.VarChar).Value = championship;
            sqlCommand.Parameters.AddWithValue("@season", System.Data.SqlDbType.VarChar).Value = season;
            sqlCommand.Parameters.AddWithValue("@home", System.Data.SqlDbType.VarChar).Value = home;
            sqlCommand.Parameters.AddWithValue("@visitor", System.Data.SqlDbType.VarChar).Value = visitor;

            using SqlDataReader reader = ReadableQuery("spRetrieveGame");
                if (reader.Read())
                    return new Game(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetInt32(4), reader.GetInt32(5));
            
            return null;
        }

        public override List<Game> GetAll()
        {
            List<Game> list = new();

            using SqlDataReader reader = ReadableQuery("spRetrieveAllGames");
                while (reader.Read())
                    list.Add(new Game(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetInt32(4), reader.GetInt32(5)));

            return list;
        }
    }
}