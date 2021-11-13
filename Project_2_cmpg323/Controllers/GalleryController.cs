using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project_2_cmpg323.Models;
using SimpleImageGallery.Data;
using System.Linq;
using System.Security.Claims;

namespace Project_2_cmpg323.Controllers
{
    [Authorize]
    public class GalleryController : Controller
    {
        private readonly IImage _imageService;

        public GalleryController(IImage imageService) {
            _imageService = imageService;
        }

        //Show all gallery images regardless of user
        /*public IActionResult Index()
        {
            var imageList = _imageService.GetAll();
            var model = new GalleryIndexModel()
            {
                Images = imageList,
                SearchQuery = ""
            };

            return View(model);  
        }*/

        public IActionResult Index()
        {
            string strCurrentUserID = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var imageList = _imageService.GetWithUserId(strCurrentUserID);
            var model = new GalleryIndexModel()
            {
                Images = imageList,
                SearchQuery = ""
            };

            return View(model);
        }

        public IActionResult Detail(int id)
        {
            var image = _imageService.GetById(id);

            var model = new GalleryDetailModel()
            {
                Id = image.Id,
                Title = image.Title,
                CreatedOn = image.Created,
                Url = image.Url,
                Tags = image.Tags.Select(t => t.Description).ToList()
            };

            return View(model);
        }
    }
}
