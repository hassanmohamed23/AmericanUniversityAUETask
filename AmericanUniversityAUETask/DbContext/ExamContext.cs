using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace AmericanUniversityAUETask.DbContext
{
    public class ExamContext
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;
        public ExamContext(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("SqlConnection");
        }
        public IDbConnection CreateConnection() => new SqlConnection(_connectionString);
    }
}
