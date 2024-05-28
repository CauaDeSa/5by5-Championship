using _5by5_ChampionshipController.src;
using Microsoft.Data.SqlClient;
using System.Data;

namespace _5by5_ChampionshipController.src.Bank
{
    public abstract class BankController<T>
    {
        readonly string Connection = "Data Source=127.0.0.1; Initial Catalog=DBChampionship; User Id=sa; Password=SqlServer2019!; TrustServerCertificate=Yes";
        protected readonly SqlConnection sqlConnection;
        protected readonly SqlCommand sqlCommand;

        public BankController()
        {
            sqlConnection = new(Connection);
            sqlCommand = new();
        }

        protected bool BooleanQuery(string sp)
        {
            sqlCommand.CommandText = sp;
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            sqlCommand.Parameters.Add("@bool", SqlDbType.Int).Direction = ParameterDirection.Output;

            sqlCommand.Connection = sqlConnection;

            sqlConnection.Open();
            sqlCommand.ExecuteNonQuery();
            sqlConnection.Close();
            bool result = (int)sqlCommand.Parameters["@bool"].Value == 1;

            sqlCommand.Parameters.Clear();

            return result;
        }

        public abstract bool Insert(T obj);
    }
}