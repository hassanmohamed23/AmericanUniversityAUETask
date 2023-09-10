using AmericanUniversityAUETask.DbContext;
using AmericanUniversityAUETask.Entities;
using AmericanUniversityAUETask.IRepository;
using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmericanUniversityAUETask.Repository
{
    public class AnswerRepository: IAnswerRepository
    {
        private readonly ExamContext _context;
        public AnswerRepository(ExamContext context)
        {
            _context = context;
        }

     

        public async Task<Answer> GetAnswerForQuestion(int questionId ,int studentId,int examId)
        {
            using (var connection = _context.CreateConnection())
            {
                var result = await connection.QueryFirstOrDefaultAsync<Answer>("SELECT * FROM Answers WHERE QuestionId = @QuestionId AND StudentId=@StudentId AND ExamId=@ExamId"
                    , new { QuestionId = questionId , StudentId =studentId, ExamId = examId });
                return result;
            }
        }

        public async Task<IEnumerable<Answer>> GetAnswerForQuestion( int studentId, int examId)
        {
            using (var connection = _context.CreateConnection())
            {
                var result = await connection.QueryAsync<Answer>("SELECT * FROM Answers WHERE StudentId=@StudentId AND ExamId=@ExamId"
                    , new {  StudentId = studentId, ExamId = examId });
                return result;
            }
        }

        public async Task<Answer> GetAnswerById(int answerId)
        {
            using (var connection = _context.CreateConnection())
            {
                var result= await connection.QueryFirstOrDefaultAsync<Answer>("SELECT * FROM Answers WHERE AnswerId = @AnswerId", new { AnswerId = answerId });
                return result;
            }

        }

        public async Task CreateAnswer(Answer answer)
        {
            using (var connection = _context.CreateConnection())
            {

                var sql = "INSERT INTO Answers (StudentId, QuestionId,ExamId, StudentAnswer, Score) VALUES (@StudentId, @QuestionId,@ExamId, @StudentAnswer, @Score)";
                await connection.ExecuteAsync(sql, answer);
            }

        }

        public async Task UpdateAnswer(Answer answer)
        {
            using (var connection = _context.CreateConnection())
            {

                var sql = "UPDATE Answers SET StudentId = @StudentId, QuestionId = @QuestionId, StudentAnswer = @StudentAnswer, Score = @Score WHERE AnswerId = @AnswerId";
               await connection.ExecuteAsync(sql, answer);
            }

        }

        public async Task DeleteAnswer(int answerId)
        {
            using (var connection = _context.CreateConnection())
            {

                var sql = "DELETE FROM Answers WHERE AnswerId = @AnswerId";
               await connection.ExecuteAsync(sql, new { AnswerId = answerId });
            }

        }
    }

}
