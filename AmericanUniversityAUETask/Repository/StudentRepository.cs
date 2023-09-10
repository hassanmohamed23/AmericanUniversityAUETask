using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using AmericanUniversityAUETask.DbContext;
using AmericanUniversityAUETask.Entities;
using AmericanUniversityAUETask.IRepository;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Repository
{
    public class StudentRepository: IStudentRepository
    {
        private readonly ExamContext _context;
        public StudentRepository(ExamContext context)
        {
            _context = context;
        }

        public async Task <IEnumerable<Student>> GetAllStudents()
        {
            using (var connection = _context.CreateConnection())
            {
                var result= await connection.QueryAsync<Student>("SELECT * FROM Students");
                return result;
            }

        }

        public async Task<Student> GetStudentById(int studentId)
        {
            using (var connection = _context.CreateConnection())
            {
                var result= await connection.QueryFirstOrDefaultAsync<Student>("SELECT * FROM Students WHERE StudentId = @StudentId", new { StudentId = studentId });
                return result;
            }
      
        }

        public async Task<Student> Login(string email ,string password)
        {
            using (var connection = _context.CreateConnection())
            {
                var result = await connection.QueryFirstOrDefaultAsync<Student>("SELECT * FROM Students WHERE Email = @email AND PasswordHash = @password", new { email, password });
                return result;
            }

        }

        public async Task<Student> SignUp(Student student)
        {
            using (var connection = _context.CreateConnection())
            {

                var query = "INSERT INTO Students (FirstName, LastName, Email, PasswordHash) OUTPUT INSERTED.* VALUES (@FirstName, @LastName, @Email, @PasswordHash)";
                var insertedStudent = await connection.QueryFirstOrDefaultAsync<Student>(query, student);
                return insertedStudent;
            }
       
        }

        public async Task UpdateStudent(Student student)
        {
            using (var connection = _context.CreateConnection())
            {

               await connection.ExecuteAsync("UPDATE Students SET FirstName = @FirstName, LastName = @LastName, Email = @Email, PasswordHash = @PasswordHash WHERE StudentId = @StudentId", student);
            }
             
        }

        public async Task DeleteStudent(int studentId)
        {
            using (var connection = _context.CreateConnection())
            {

               await connection.ExecuteAsync("DELETE FROM Students WHERE StudentId = @StudentId", new { StudentId = studentId });
            }
      
        }
    }
}
