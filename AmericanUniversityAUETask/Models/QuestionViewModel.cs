using AmericanUniversityAUETask.Entities;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;

namespace AmericanUniversityAUETask.Models
{
    public class QuestionViewModel
    {
        public Questions question { get; set; } = new();
        public List<QuestionType> questionTypeList { get; set; } = new();
        public List<Exam> examList { get; set; } = new();
    }
}
