using Business.Services;
using DAL.Models;
using Exceptions.Entity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using NetEduHomeTask.Helpers.Extensions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NetEduHomeTask.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SliderController : Controller
    {
        private readonly ISliderService _sliderService;
        private readonly IImageService _imageService;
        private readonly IWebHostEnvironment _env;
        public SliderController(ISliderService sliderService, IImageService imageService, IWebHostEnvironment env)
        {
            _sliderService = sliderService;
            _imageService = imageService;
            _env = env;
        }

        public async Task<IActionResult> Index()
        {
            List<Slider> sliders;
            try
            {
                sliders = await _sliderService.GetAll();
            }
            catch (EntityIsNullException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw;
            }
            return View(sliders);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Slider slider)
        {
            if (!ModelState.IsValid)
            {
                return View(slider);
            }

            if (slider.ImageFile is null)
            {
                ModelState.AddModelError("ImageFile", "Image can not be empty");
                return View(slider);
            }

            string fileName = await slider.ImageFile.CreateFile(_env);

            Image image = new Image();
            image.Name = fileName;

            await _imageService.Create(image);
            slider.ImageId = image.Id;

            await _sliderService.Create(slider);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int? id)
        {
            Slider slider;
            try
            {
                slider = await _sliderService.Get(id);
            }
            catch (ArgumentNullException ex)
            {
                throw ex;
            }
            catch (NullReferenceException ex)
            {
                throw ex;
            }
            catch (Exception)
            {
                throw;
            }
            return View(slider);
        }

        public async Task<IActionResult> Update(int? id)
        {

            Slider slider;
            try
            {
                slider = await _sliderService.Get(id);
            }
            catch (ArgumentNullException ex)
            {
                throw ex;
            }
            catch (EntityIsNullException ex)
            {
                throw ex;
            }
            catch (Exception)
            {
                throw;
            }

            return View(slider);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int id, Slider slider)
        {

            if (!ModelState.IsValid)
            {
                return View(slider);
            }

            if (slider.ImageFile != null)
            {
                string fileName = await slider.ImageFile.CreateFile(_env);

                Image image = new Image();
                image.Name = fileName;

                await _imageService.Create(image);

                int oldImageId = slider.ImageId;

                slider.ImageId = image.Id;

                await _sliderService.Update(id, slider);
                await _imageService.Delete(oldImageId);
            }

            await _sliderService.Update(id, slider);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            try
            {
                await _sliderService.Delete(id);
            }
            catch (ArgumentNullException ex)
            {
                throw ex;
            }
            catch (EntityIsNullException ex)
            {
                throw ex;
            }
            catch (Exception)
            {
                throw;
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
