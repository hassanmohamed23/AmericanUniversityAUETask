using AmericanUniversityAUETask.Entities;
using AmericanUniversityAUETask.IRepository;
using AmericanUniversityAUETask.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using Repository;
using System.Threading.Tasks;

namespace AmericanUniversityAUETask.Controllers
{
    public class UserController : Controller
    {
        private readonly IStudentRepository _studentRepository;
        public UserController(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        [HttpGet]
        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(SignInViewModel model)
        {
          var student= await _studentRepository.Login(model.email, model.password);

            if(student!=null)
            {
                var option = new CookieOptions
                {
                    Expires = DateAndTime.Now.AddDays(1),
                    Secure = true,
                    HttpOnly = true
                };
                Response.Cookies.Append("userName", student.FirstName, option);
                Response.Cookies.Append("userId", student.StudentId.ToString(), option);
                Response.Cookies.Append("Email", student.Email, option);

            }
            else
            {
                return RedirectToAction("SignIn");
            }

            return RedirectToAction("Index","Home");
        }


        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public async Task< IActionResult> SignUp(SignUpViewModel model)
        {
            var studentDto = new Student()
            {
                FirstName=model.firstName,
                LastName=model.lastName,
                Email=model.email,
                PasswordHash=model.password,
            };

            var student=await _studentRepository.SignUp(studentDto);
            return RedirectToAction("SignIn");
        }

    
        public async Task<IActionResult> LogOut()
        {

            Response.Cookies.Delete("userName");
            Response.Cookies.Delete("userId");
            Response.Cookies.Delete("Email");

            return RedirectToAction("SignIn");
        }

        
    }
}
