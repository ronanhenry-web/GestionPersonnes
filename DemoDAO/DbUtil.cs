using MySql.Data.MySqlClient;

namespace DemoDAO
{
    public class DbUtil
    {
        internal static MySqlConnection GetConnection()
        {
            var conn = new MySqlConnection(
                "Server=localhost;Port=3306;Database=csharpdao;User=root;Password=;");
            conn.Open();
            return conn;
        }
    }
}