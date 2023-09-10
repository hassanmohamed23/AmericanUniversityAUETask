using AmericanUniversityAUETask.Entities;
using AmericanUniversityAUETask.IRepository;
using AmericanUniversityAUETask.Models;
using AmericanUniversityAUETask.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.VisualBasic;
using Repository;
using System.Linq;
using System.Threading.Tasks;

namespace AmericanUniversityAUETask.Controllers
{
    public class AdminController : Controller
    {
        private readonly IExamRepository _examRepository;
        private readonly IQuestionTypeRepository _questionTypeRepository;
        private readonly IQuestionRepository _questionRepository;
        private readonly ILogger<HomeController> _logger;

        public AdminController(ILogger<HomeController> logger,IExamRepository examRepository, IQuestionTypeRepository questionTypeRepository, IQuestionRepository questionRepository)
        {
            _logger = logger;
            _examRepository = examRepository;
            _questionTypeRepository = questionTypeRepository;
            _questionRepository = questionRepository;
        }
        public async Task <IActionResult> AddExam()
        {
            var examList= await _examRepository.GetAllExams();

            var model = new ExamViewModel()
            {
                examList= examList.ToList(),
            };
            _logger.LogInformation("Get All Exam List From Db ", examList.ToList());

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddExam(ExamViewModel model)
        {

            var examDto = new Exam()
            {
                Title= model.title,
                Description=model.Description
            };

            await _examRepository.CreateExam(examDto);

            _logger.LogInformation("Add New Exam to Db", model);

            this.TempData["SuccessMessage"] = "Exam Added Successfully";
            return RedirectToAction("AddExam", "Admin");
            
        }

        [HttpGet]
        public async Task<IActionResult> AddQuestion()
        {
            var examList = await _examRepository.GetAllExams();
            var questionTypeList = await _questionTypeRepository.GetAll();


            var model = new QuestionViewModel()
            {
                examList = examList.ToList(),
                questionTypeList= questionTypeList.ToList()
            };


            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddQuestion(QuestionViewModel model)
        {
            
            await _questionRepository.CreateQuestion(model.question);

            _logger.LogInformation("Add New Question to Db", model.question);

            this.TempData["SuccessMessage"] = "Question Added Successfully";
            return RedirectToAction("AddQuestion", "Admin");
        }
    }
}
