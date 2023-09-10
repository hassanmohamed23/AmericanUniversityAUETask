using AmericanUniversityAUETask.DbContext;
using AmericanUniversityAUETask.Entities;
using AmericanUniversityAUETask.IRepository;
using Dapper;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace AmericanUniversityAUETask.Repository
{
    public class QuestionRepository : IQuestionRepository
    {
        private readonly ExamContext _context;

        public QuestionRepository(ExamContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Questions>> GetAllQuestions()
        {
            using (var connection = _context.CreateConnection())
            {
                var result = await connection.QueryAsync<Questions>("SELECT * FROM Questions");
                return result;
            }
        }

        public async Task<Questions> GetQuestionById(int questionId)
        {
            using (var connection = _context.CreateConnection())
            {
                var result = await connection.QueryFirstOrDefaultAsync<Questions>(
                    "SELECT * FROM Questions WHERE QuestionId = @QuestionId",
                    new { QuestionId = questionId });
                return result;
            }
        }
      
        public async Task<IEnumerable<Questions>> GetQuestionsByExamId(int examId)
        {
            using (var connection = _context.CreateConnection())
            {
                var result = await connection.QueryAsync<Questions>(
                    "SELECT * FROM Questions WHERE ExamId = @ExamId",
                    new { ExamId = examId });
                return result;
            }
        }

     

        public async Task<int> GetQuestionCountByExamId(int examId)
        {
            using (var connection = _context.CreateConnection())
            {
                var result = await connection.ExecuteScalarAsync<int>(
                    "SELECT COUNT(*) FROM Questions WHERE ExamId = @ExamId",
                    new { ExamId = examId });
                return result;
            }
        }


        public async Task CreateQuestion(Questions question)
        {
            using (var connection = _context.CreateConnection())
            {
                var sql = "INSERT INTO Questions (ExamId, QuestionText, QuestionType, Option1, Option2, Option3, Option4, CorrectAnswer, Score) " +
                          "VALUES (@ExamId, @QuestionText, @QuestionType, @Option1, @Option2, @Option3, @Option4, @CorrectAnswer, @Score )";
                await connection.ExecuteAsync(sql, question);
            }
        }

        public async Task UpdateQuestion(Questions question)
        {
            using (var connection = _context.CreateConnection())
            {
                var sql = "UPDATE Questions SET ExamId = @ExamId, " +
                          "QuestionText = @QuestionText, " +
                          "QuestionType = @QuestionType, " +
                          "Option1 = @Option1, " +
                          "Option2 = @Option2, " +
                          "Option3 = @Option3, " +
                          "Option4 = @Option4, " +
                          "CorrectAnswer = @CorrectAnswer " +
                          "WHERE QuestionId = @QuestionId";
                await connection.ExecuteAsync(sql, question);
            }
        }

        public async Task DeleteQuestion(int questionId)
        {
            using (var connection = _context.CreateConnection())
            {
                var sql = "DELETE FROM Questions WHERE QuestionId = @QuestionId";
                await connection.ExecuteAsync(sql, new { QuestionId = questionId });
            }
        }
    }
}

