using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using Dapper;

namespace Repository
{
    public class StudentRepository
    {
        private readonly string _connectionString;

        public StudentRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<Student> GetAllStudents()
        {
            using (var dbConnection = new SqlConnection(_connectionString))
            {
                dbConnection.Open();
                return dbConnection.Query<Student>("SELECT * FROM Students");
            }

        }

        public Student GetStudentById(int studentId)
        {
            using (var dbConnection = new SqlConnection(_connectionString))
            {
                dbConnection.Open();
                return dbConnection.QueryFirstOrDefault<Student>("SELECT * FROM Students WHERE StudentId = @StudentId", new { StudentId = studentId });
            }
      
        }

        public void CreateStudent(Student student)
        {
            using (var dbConnection = new SqlConnection(_connectionString))
            {
                dbConnection.Open();
                dbConnection.Execute("INSERT INTO Students (FirstName, LastName, Email, PasswordHash) VALUES (@FirstName, @LastName, @Email, @PasswordHash)", student);
            }
       
        }

        public void UpdateStudent(Student student)
        {
            using (var dbConnection = new SqlConnection(_connectionString))
            {
                dbConnection.Open();
                dbConnection.Execute("UPDATE Students SET FirstName = @FirstName, LastName = @LastName, Email = @Email, PasswordHash = @PasswordHash WHERE StudentId = @StudentId", student);
            }
             
        }

        public void DeleteStudent(int studentId)
        {
            using (var dbConnection = new SqlConnection(_connectionString))
            {
                dbConnection.Open();
                dbConnection.Execute("DELETE FROM Students WHERE StudentId = @StudentId", new { StudentId = studentId });
            }
      
        }
    }
}
