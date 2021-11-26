using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebShop.Core.Model.File;
using WebShop.Core.Services;

namespace WebShop.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContentsController : ControllerBase
    {
        private readonly ItemService itemService;
        private readonly IWebHostEnvironment _env;

        public ContentsController(ItemService itemService, IWebHostEnvironment env)
        {
            this.itemService = itemService;
            _env = env;
        }

        [HttpPost]
        public IActionResult SaveImg([FromForm] FileModel file)
        {
            string fileName = itemService.ImageToSave(_env.WebRootPath, file);

            return Ok(fileName);
        }

        [HttpGet("{fileName}")]
        public IActionResult GetImage(string fileName)
        {
            FileContentResult fileContentResult = File(itemService.GetImage(_env.WebRootPath, fileName).Bytes,
                "image/jpeg");
            return Ok(fileContentResult);
        }

        [HttpPost("images")]
        public IActionResult GetImages(List<string> contentPaths)
        {
            List<FileContentResult> fileContentResults = new List<FileContentResult>();
            foreach (string contentPath in contentPaths)
            {
                var content = itemService.GetImage(_env.WebRootPath, contentPath);
                if (content.Type.Equals(".mp4")) content.Type = "video/mp4";
                else content.Type = "image/jpeg";
                fileContentResults.Add(File(content.Bytes, content.Type));
            }
            return Ok(fileContentResults);
        }
    }
}
