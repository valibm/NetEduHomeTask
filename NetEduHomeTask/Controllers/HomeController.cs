using Business.Services;
using Business.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace NetEduHomeTask.Controllers
{
    public class HomeController : Controller
    {
        private readonly ISliderService _sliderService;
        private readonly INoticeService _noticeService;
        private readonly ICourseService _courseService;
        public HomeController(ISliderService sliderService, INoticeService noticeService, ICourseService courseService)
        {
            _sliderService = sliderService;
            _noticeService = noticeService;
            _courseService = courseService;
        }
        public async Task<IActionResult> Index()
        {
            HomeVM homeVm = new HomeVM();
            
            homeVm.Sliders = await _sliderService.GetAll();
            homeVm.Notices = await _noticeService.GetAll();
            homeVm.Courses = await _courseService.GetAll();
            
            return View(homeVm);
        }
    }
}
