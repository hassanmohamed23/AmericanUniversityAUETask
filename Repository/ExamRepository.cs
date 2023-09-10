using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class ExamRepository
    {
        private readonly string _connectionString;

        public ExamRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<Exam> GetAllExams()
        {
            using (var dbConnection = new SqlConnection(_connectionString))
            {
                dbConnection.Open();
                return dbConnection.Query<Exam>("SELECT * FROM Exams");
            }
            
        }

        public Exam GetExamById(int examId)
        {
            using (var dbConnection = new SqlConnection(_connectionString))
            {
                dbConnection.Open();
                return dbConnection.QueryFirstOrDefault<Exam>("SELECT * FROM Exams WHERE ExamId = @ExamId", new { ExamId = examId });
            }
            
        }

        public void CreateExam(Exam exam)
        {
            using (var dbConnection = new SqlConnection(_connectionString))
            {
                dbConnection.Open();
                var sql = "INSERT INTO Exams (Title, Description) VALUES (@Title, @Description)";
                dbConnection.Execute(sql, exam);
            }
         
        }

        public void UpdateExam(Exam exam)
        {
            using (var dbConnection = new SqlConnection(_connectionString))
            {
                dbConnection.Open();
                var sql = "UPDATE Exams SET Title = @Title, Description = @Description WHERE ExamId = @ExamId";
                dbConnection.Execute(sql, exam);

            }
           
        }

        public void DeleteExam(int examId)
        {
            using (var dbConnection = new SqlConnection(_connectionString))
            {
                dbConnection.Open();
                var sql = "DELETE FROM Exams WHERE ExamId = @ExamId";
                dbConnection.Execute(sql, new { ExamId = examId });
            }
         
        }
    }
}
