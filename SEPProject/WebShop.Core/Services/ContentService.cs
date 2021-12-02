using System;
using System.IO;
using WebShop.Core.DTOs;
using WebShop.Core.Model.File;

namespace WebShop.Core.Services
{
    public class ContentService
    {
        public Content GetImage(string path, string fileName)
        {
            var type = Path.GetExtension(fileName);
            path = path + "\\images\\" + fileName;
            return new Content() { Bytes = System.IO.File.ReadAllBytes(path), Type = type };
        }

        public string ImageToSave(string path, FileModel file)
        {
            try
            {
                using (Stream stream = new FileStream(path + "\\images\\" + file.FileName, FileMode.Create))
                {
                    file.FormFile.CopyTo(stream);
                }
                return file.FileName;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
