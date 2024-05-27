using _5by5_ChampionshipController.Entity;
using Microsoft.Data.SqlClient;

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

            sqlConnection.Open();
            object result = sqlCommand.ExecuteScalar();
            sqlConnection.Close();

            sqlCommand.Parameters.Clear();
            sqlCommand.Connection = sqlConnection;

            return (int)result == 1;
        }

        protected SqlDataReader ReadableQuery(string sp)
        {
            sqlCommand.CommandText = sp;
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

            sqlCommand.Parameters.Add(new SqlParameter("@result", System.Data.SqlDbType.Int) { Direction = System.Data.ParameterDirection.Output });

            sqlConnection.Open();
            SqlDataReader dr = sqlCommand.ExecuteReader();
            sqlConnection.Close();

            sqlCommand.Parameters.Clear();
            sqlCommand.Connection = sqlConnection;

            return dr;
        }

        public abstract bool Insert(T obj);

        public abstract List<T> GetAll();
    }
}