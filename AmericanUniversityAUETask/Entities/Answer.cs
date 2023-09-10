using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmericanUniversityAUETask.Entities
{
    public class Answer
    {
        public int AnswerId { get; set; }
        public int StudentId { get; set; }
        public int QuestionId { get; set; }
        public int ExamId { get; set; }
        public int StudentAnswer { get; set; }
        public int Score { get; set; }
    }
}
