using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmericanUniversityAUETask.Entities
{
    public class Questions
    {
        public int QuestionId { get; set; }
        public int ExamId { get; set; }
        public string QuestionText { get; set; }
        public int QuestionType { get; set; }
        public string Option1 { get; set; }
        public string Option2 { get; set; }
        public string Option3 { get; set; }
        public string Option4 { get; set; }
        public int CorrectAnswer { get; set; }
        public int Score { get; set; }

    }
}
