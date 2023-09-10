using Dapper;
using Entities;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class AnswerRepository
    {
        private readonly IDbConnection _dbConnection;

        public AnswerRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection ?? throw new ArgumentNullException(nameof(dbConnection));
        }

        public IEnumerable<Answer> GetAnswersForQuestion(int questionId)
        {
            using (var dbConnection = new SqlConnection(_connectionString))
            {
                dbConnection.Open();
                return dbConnection.Query<Answer>("SELECT * FROM Answers WHERE QuestionId = @QuestionId", new { QuestionId = questionId });
            }
           
        }

        public Answer GetAnswerById(int answerId)
        {
            using (var dbConnection = new SqlConnection(_connectionString))
            {
                dbConnection.Open();
                return dbConnection.QueryFirstOrDefault<Answer>("SELECT * FROM Answers WHERE AnswerId = @AnswerId", new { AnswerId = answerId });
            }
  
        }

        public void CreateAnswer(Answer answer)
        {
            using (var dbConnection = new SqlConnection(_connectionString))
            {
                dbConnection.Open();
                var sql = "INSERT INTO Answers (StudentId, QuestionId, AnswerText, Score) VALUES (@StudentId, @QuestionId, @AnswerText, @Score)";
                dbConnection.Execute(sql, answer);
            }
     
        }

        public void UpdateAnswer(Answer answer)
        {
            using (var dbConnection = new SqlConnection(_connectionString))
            {
                dbConnection.Open();
                var sql = "UPDATE Answers SET StudentId = @StudentId, QuestionId = @QuestionId, AnswerText = @AnswerText, Score = @Score WHERE AnswerId = @AnswerId";
                dbConnection.Execute(sql, answer);
            }
 
        }

        public void DeleteAnswer(int answerId)
        {
            using (var dbConnection = new SqlConnection(_connectionString))
            {
                dbConnection.Open();
                var sql = "DELETE FROM Answers WHERE AnswerId = @AnswerId";
                dbConnection.Execute(sql, new { AnswerId = answerId });
            }
         
        }
    }

}
