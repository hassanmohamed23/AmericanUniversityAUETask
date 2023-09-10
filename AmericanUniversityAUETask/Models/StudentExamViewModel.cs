using AmericanUniversityAUETask.Entities;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;

namespace AmericanUniversityAUETask.Models
{
    public class StudentExamViewModel
    {
        public int examId { get; set; }
        public int questionId { get; set; }
        public Questions question { get; set; } = new();
        public Answer answer { get; set; } = new();
        public int count { get; set; }
        public int pageNumber { get; set; }
        public int totalPages { get; set; }

        public int selectedOption { get; set; }
 
    }
}
