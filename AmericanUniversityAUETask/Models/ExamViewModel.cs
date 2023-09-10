using AmericanUniversityAUETask.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AmericanUniversityAUETask.Models
{
    public class ExamViewModel
    {

        [Required]
        public string title { get; set; }

        [Required]
        public string Description { get; set; }

        public List<Exam> examList { get; set; } = new();
    }


}
