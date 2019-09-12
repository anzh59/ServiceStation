using System.Configuration;
using MySql.Data.MySqlClient;

namespace ServiceStation.Domain.Abstract
{
    public class MySqlDBAccessor : IDBAccessor
    {
        public string ConnectionString { get; private set; }

        public bool CheckConnectivity()
        {
            bool result = false;

            var connection = new MySqlConnection(ConnectionString);
            try
            {
                connection.Open();
                result = true;
                connection.Close();
            }
            catch
            {
                result = false;
            }

            return result;
        }

        public void SetConnectionUserInfo(string username, string password)
        {
            ConnectionString = $"{ConfigurationManager.ConnectionStrings["ServiceStation"].ConnectionString}" +
                $"UserId={username};Password={password};";
        }
    }
}