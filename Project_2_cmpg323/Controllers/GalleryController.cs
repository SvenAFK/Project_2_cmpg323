using Microsoft.AspNetCore.Mvc;
using Project_2_cmpg323.Models;
using SimpleImageGallery.Data;
using SimpleImageGallery.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project_2_cmpg323.Controllers
{
    public class GalleryController : Controller
    {
        private readonly IImage _imageService;

        public GalleryController(IImage imageService) {
            _imageService = imageService;
        }

        public IActionResult Index()
        {
            var imageList = _imageService.GetAll();
            var model = new GalleryIndexModel()
            {
                Images = imageList,
                SearchQuery = ""
            };

            return View(model);
        }
    }
}
