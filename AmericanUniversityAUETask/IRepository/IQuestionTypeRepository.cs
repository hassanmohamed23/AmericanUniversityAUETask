using AmericanUniversityAUETask.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AmericanUniversityAUETask.IRepository
{
    public interface IQuestionTypeRepository
    {
        Task<IEnumerable<QuestionType>> GetAll();
        Task<QuestionType> GetById(int Value);
    }
}
