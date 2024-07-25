using System.Data.SqlClient;
namespace GestioneHotel.Services


{
    public class DatabaseService
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public DatabaseService(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("DefaultConnection");
        }

        public SqlConnection GetConnection()
        {
            return new SqlConnection(_connectionString);
        }
    }
}
