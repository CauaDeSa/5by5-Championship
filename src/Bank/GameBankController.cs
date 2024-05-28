using _5by5_ChampionshipController.src.Entity;
using Microsoft.Data.SqlClient;

namespace _5by5_ChampionshipController.src.Bank
{
    public class GameBankController : BankController<Game>
    {
        public GameBankController() : base() 
        {
            sqlCommand.CommandText = "spInitializeGame";
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            sqlCommand.Connection = sqlConnection;

            sqlConnection.Open();

            sqlCommand.ExecuteNonQuery();

            sqlCommand.Parameters.Clear();
            sqlConnection.Close();
        }

        public override bool Insert(Game game)
        {
            sqlCommand.Parameters.AddWithValue("@championship", System.Data.SqlDbType.VarChar).Value = game.Championship;
            sqlCommand.Parameters.AddWithValue("@season", System.Data.SqlDbType.VarChar).Value = game.Season;
            sqlCommand.Parameters.AddWithValue("@visitor", System.Data.SqlDbType.VarChar).Value = game.Visitor;
            sqlCommand.Parameters.AddWithValue("@home", System.Data.SqlDbType.VarChar).Value = game.Home;
            sqlCommand.Parameters.AddWithValue("@homeGoals", System.Data.SqlDbType.Int).Value = game.HGoals;
            sqlCommand.Parameters.AddWithValue("@visitorGoals", System.Data.SqlDbType.Int).Value = game.VGoals;

            return BooleanQuery("spCreateGame");
        }

        public Game RetrieveGame(string championship, string season, string visitor, string home)
        {
            sqlCommand.CommandText = "spRetrieveGame";
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

            sqlCommand.Parameters.AddWithValue("@championship", System.Data.SqlDbType.VarChar).Value = championship;
            sqlCommand.Parameters.AddWithValue("@season", System.Data.SqlDbType.VarChar).Value = season;
            sqlCommand.Parameters.AddWithValue("@visitor", System.Data.SqlDbType.VarChar).Value = visitor;
            sqlCommand.Parameters.AddWithValue("@home", System.Data.SqlDbType.VarChar).Value = home;

            sqlCommand.Connection = sqlConnection;

            sqlConnection.Open();

            SqlDataReader reader = sqlCommand.ExecuteReader();

            reader.Read();

            Game game = new Game(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetInt32(4), reader.GetInt32(5));

            sqlCommand.Parameters.Clear();
            sqlConnection.Close();

            return game;
        }
    }
}