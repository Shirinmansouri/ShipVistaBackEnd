using Microsoft.AspNetCore.Http;
using Plants.Domain.IService;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Plants.Domain.Service
{
    public class FileService : IFileService
    {
        public string Upload(IFormFile file)
        {
            var folderName = Path.Combine("Resources", "Images");
            var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

            if (!Directory.Exists(pathToSave))
                Directory.CreateDirectory(pathToSave);

            var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(pathToSave, fileName);

            using (var strem = File.Create(filePath))
            {
                file.CopyTo(strem);
            }
            return fileName;
        }
    }
}
