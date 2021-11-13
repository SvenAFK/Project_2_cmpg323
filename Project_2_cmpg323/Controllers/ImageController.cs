using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Project_2_cmpg323.Models;
using SimpleImageGallery.Data;
using SimpleImageGallery.Services;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Project_2_cmpg323.Controllers
{
    [Authorize]
    public class ImageController : Controller
    {
        private IConfiguration _config;
        private IImage _imageService;
        private string AzureConnectionString { get; }

        public ImageController(IConfiguration config, IImage imageService)
        {
            _config = config;
            _imageService = imageService;
            AzureConnectionString = _config["AzureStorageConnectionString"];
        }

        public IActionResult Upload()
        {
            var model = new UploadImageModel();
            return View(model);
        }

        public IActionResult Update()
        {
            var model = new UpdateImageModel();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateOldImage(int Id, string Title, string Tags)
        {
            await _imageService.UpdateImage(Id, Title, Tags);
            return RedirectToAction("index", "Gallery");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int Id)
        {
            await _imageService.DeleteImage(Id);
            return RedirectToAction("index", "Gallery");
        }

        [HttpPost]
        public async Task<IActionResult> UploadNewImage(IFormFile file, string tags, string title)
        {
            var container = _imageService.GetBlobContainer(AzureConnectionString, "images");

            var content = ContentDispositionHeaderValue.Parse(file.ContentDisposition);
            var fileName = content.FileName.Trim('"');

            //Get current user
            string strCurrentUserID = User.FindFirstValue(ClaimTypes.NameIdentifier);

            //Get a reference to a Block Blob
            var blockBlob = container.GetBlockBlobReference(fileName);

            await blockBlob.UploadFromStreamAsync(file.OpenReadStream());
            await _imageService.SetImage(title, tags, blockBlob.Uri, strCurrentUserID);

            return RedirectToAction("Index", "Gallery");
        }


    }
}
