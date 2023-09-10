using AmericanUniversityAUETask.Entities;
using AmericanUniversityAUETask.IRepository;
using AmericanUniversityAUETask.Models;
using AmericanUniversityAUETask.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Identity.Client;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;

namespace AmericanUniversityAUETask.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IExamRepository _examRepository;
        private readonly IQuestionRepository _questionRepository;
        private readonly IAnswerRepository _answerRepository;
        private readonly IStudentRepository _studentRepository;
        private const int size = 1;
        public HomeController(ILogger<HomeController> logger, IExamRepository examRepository, IQuestionRepository questionRepository, IAnswerRepository answerRepository, IStudentRepository studentRepository)
        {
            _logger = logger;
            _examRepository = examRepository;
            _questionRepository = questionRepository;
            _answerRepository = answerRepository;
            _studentRepository = studentRepository;
        }

        public async Task<IActionResult> Index()
        {
            if(string.IsNullOrEmpty(HttpContext.Request.Cookies["userName"]))
            {
                return RedirectToAction("SignIn","User");
            }
            
            var examList=await _examRepository.GetAllExams();

            return View(examList);
        }

        [HttpGet]
        public async Task<IActionResult> Exam(int id,int pageNumber=1 )
        {
            var studentId = 0;
            int.TryParse( HttpContext.Request.Cookies["userId"], out studentId);

            var QuestionsCount = await _questionRepository.GetQuestionCountByExamId(id);
            var QuestionList = await _questionRepository.GetQuestionsByExamId(id);
            var Question = GetQuestionsByPage(QuestionList, pageNumber, size);
            var Answer = await _answerRepository.GetAnswerForQuestion(Question.QuestionId, studentId,id);
            var TotalPages = (int)Math.Ceiling(QuestionsCount / (double)size);


            var model = new StudentExamViewModel()
            {             
                question = Question,
                answer= Answer,
                count = QuestionsCount,
                totalPages = TotalPages,
                pageNumber= pageNumber,
                examId = id,
            };
            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> Exam(StudentExamViewModel model)
        {
            var studentId = 0;
            int.TryParse(HttpContext.Request.Cookies["userId"], out studentId);

            var Question = await _questionRepository.GetQuestionById(model.questionId);
            var Score= model.selectedOption==Question.CorrectAnswer ? Question.Score : 0;

            var oldAnswer = await _answerRepository.GetAnswerForQuestion(Question.QuestionId, studentId, model.examId);

            if (oldAnswer != null)
            {
                oldAnswer.StudentAnswer = model.selectedOption;
                oldAnswer.Score= model.selectedOption == Question.CorrectAnswer ? Question.Score : 0;
                await _answerRepository.UpdateAnswer(oldAnswer);

            }
            else
            {

                var Answer = new Answer()
                {
                    ExamId = model.examId,
                    QuestionId = model.questionId,
                    StudentId = studentId,
                    Score = Score,
                    StudentAnswer = model.selectedOption,
                };
                await _answerRepository.CreateAnswer(Answer);

            }
     

          
          
            return RedirectToAction("Exam", "Home", new { id=model.examId ,pageNumber=model.pageNumber+1});
        }


        [HttpGet]
        public async Task<IActionResult> Grade(int examId)
        {

            var studentId = 0;
            int.TryParse(HttpContext.Request.Cookies["userId"], out studentId);

            var student = await _studentRepository.GetStudentById(studentId);
            var AnswerList = await _answerRepository.GetAnswerForQuestion(studentId, examId);

            var Grade = 0;
            foreach(var answer in AnswerList.ToList())
            {
                Grade += answer.Score;
            }

            var model = new GradeViewModel()
            {
                Grade = Grade,
               Student = student,
            };

            return View(model);
        }

        public Questions GetQuestionsByPage(IEnumerable<Questions> allQuestions, int pageNumber, int pageSize)
        {
            int startIndex = (pageNumber - 1) * pageSize;

            var pageQuestions = allQuestions.Skip(startIndex).Take(pageSize);

            return pageQuestions.FirstOrDefault();
        }



    }
}
