using _5by5_ChampionshipController.Entity;
using Microsoft.Data.SqlClient;

namespace _5by5_ChampionshipController.Bank
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

        protected void Query()
        {
            sqlConnection.Open();
            sqlCommand.ExecuteNonQuery();
            sqlConnection.Close();

            sqlCommand.Connection = sqlConnection;
        }

        public abstract void Insert(T obj);

        public abstract List<T> GetAll();

        public abstract void RemoveAll();
    }
}