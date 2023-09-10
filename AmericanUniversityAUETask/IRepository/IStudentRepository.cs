using AmericanUniversityAUETask.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AmericanUniversityAUETask.IRepository
{
    public interface IStudentRepository
    {
        Task<IEnumerable<Student>> GetAllStudents();
        Task<Student> GetStudentById(int studentId);
        Task<Student> Login(string email, string password);
        Task<Student> SignUp(Student student);
        Task UpdateStudent(Student student);
        Task DeleteStudent(int studentId);
    }
}
