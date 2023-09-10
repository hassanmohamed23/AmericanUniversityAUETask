using AmericanUniversityAUETask.Entities;

namespace AmericanUniversityAUETask.Models
{
    public class GradeViewModel
    {
        public int Grade { get; set; }
        public Student Student { get; set; } = new();
    }
}
