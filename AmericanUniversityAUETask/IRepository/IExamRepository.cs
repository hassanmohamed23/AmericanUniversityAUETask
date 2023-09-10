using AmericanUniversityAUETask.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AmericanUniversityAUETask.IRepository
{
    public interface IExamRepository
    {
        Task<IEnumerable<Exam>> GetAllExams();
        Task<Exam> GetExamById(int examId);
        Task CreateExam(Exam exam);
        Task UpdateExam(Exam exam);
        Task DeleteExam(int examId);
    }
}
