using AmericanUniversityAUETask.DbContext;
using AmericanUniversityAUETask.Entities;
using AmericanUniversityAUETask.IRepository;
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
    public class ExamRepository : IExamRepository
    {
        private readonly ExamContext _context;
        public ExamRepository(ExamContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Exam>> GetAllExams()
        {
            using (var connection = _context.CreateConnection())
            {
                var result=await connection.QueryAsync<Exam>("SELECT * FROM Exams");
                return result;
            }
            
        }

        public async Task<Exam> GetExamById(int examId)
        {
            using (var connection = _context.CreateConnection())
            {
                var result =await connection.QueryFirstOrDefaultAsync<Exam>("SELECT * FROM Exams WHERE ExamId = @ExamId", new { ExamId = examId });
                return result;
            }
            
        }

        public async Task CreateExam(Exam exam)
        {
            using (var connection = _context.CreateConnection())
            {

                var sql = "INSERT INTO Exams (Title, Description) VALUES (@Title, @Description)";
                await connection.ExecuteAsync(sql, exam);
            }
         
        }

        public async Task UpdateExam(Exam exam)
        {
            using (var connection = _context.CreateConnection())
            {

                var sql = "UPDATE Exams SET Title = @Title, Description = @Description WHERE ExamId = @ExamId";
                await connection.ExecuteAsync(sql, exam);

            }
           
        }

        public async Task DeleteExam(int examId)
        {
            using (var connection = _context.CreateConnection())
            {

                var sql = "DELETE FROM Exams WHERE ExamId = @ExamId";
                await connection.ExecuteAsync(sql, new { ExamId = examId });
            }
         
        }
    }
}
