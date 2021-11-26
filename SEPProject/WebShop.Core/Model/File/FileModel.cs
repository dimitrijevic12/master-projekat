using Microsoft.AspNetCore.Http;

namespace WebShop.Core.Model.File
{
    public class FileModel
    {
        public string FileName { get; set; }
        public IFormFile FormFile { get; set; }
    }
}
