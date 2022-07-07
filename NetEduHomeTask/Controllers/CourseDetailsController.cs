using Business.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace NetEduHomeTask.Controllers
{
    public class CourseDetailsController : Controller
    {
        private readonly ICourseService _courseService;
        public CourseDetailsController(ICourseService courseService)
        {
            _courseService = courseService;
        }
        public async Task<IActionResult> Index(int? id)
        {
            var data = await _courseService.Get(id);
            return View(data);
        }
    }
}
