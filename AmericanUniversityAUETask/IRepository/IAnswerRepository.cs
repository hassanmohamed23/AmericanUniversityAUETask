using AmericanUniversityAUETask.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AmericanUniversityAUETask.IRepository
{
    public interface IAnswerRepository
    {
        Task<Answer> GetAnswerForQuestion(int questionId, int studentId, int examId);
        Task<IEnumerable<Answer>> GetAnswerForQuestion(int studentId, int examId);
        Task<Answer> GetAnswerById(int answerId);
        Task CreateAnswer(Answer answer);
        Task UpdateAnswer(Answer answer);
        Task DeleteAnswer(int answerId);
    }
}
