using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Plants.Domain.IService
{
    public interface IFileService
    {
        string Upload(IFormFile file);
    }
}
