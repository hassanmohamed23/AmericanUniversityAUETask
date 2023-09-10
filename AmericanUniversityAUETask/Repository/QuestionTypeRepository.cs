using AmericanUniversityAUETask.DbContext;
using AmericanUniversityAUETask.Entities;
using AmericanUniversityAUETask.IRepository;
using Dapper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AmericanUniversityAUETask.Repository
{
    public class QuestionTypeRepository : IQuestionTypeRepository
    {
        private readonly ExamContext _context;
        public QuestionTypeRepository(ExamContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<QuestionType>> GetAll()
        {
            using (var connection = _context.CreateConnection())
            {
                var result = await connection.QueryAsync<QuestionType>("SELECT * FROM QuestionType");
                return result;
            }

        }

        public async Task<QuestionType> GetById(int Value)
        {
            using (var connection = _context.CreateConnection())
            {
                var result = await connection.QueryFirstOrDefaultAsync<QuestionType>("SELECT * FROM Exams WHERE QuestionType = @Value", new { Value });
                return result;
            }
        }
    }
}
