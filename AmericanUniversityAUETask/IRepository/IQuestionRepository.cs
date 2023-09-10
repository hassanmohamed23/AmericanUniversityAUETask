using AmericanUniversityAUETask.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AmericanUniversityAUETask.IRepository
{
    public interface IQuestionRepository
    {
       Task<IEnumerable<Questions>> GetAllQuestions();
       Task<Questions> GetQuestionById(int questionId);
       Task<IEnumerable<Questions>> GetQuestionsByExamId(int examId);
       Task<int> GetQuestionCountByExamId(int examId);
       Task CreateQuestion(Questions question);
       Task UpdateQuestion(Questions question);
       Task DeleteQuestion(int questionId);
    }
}
