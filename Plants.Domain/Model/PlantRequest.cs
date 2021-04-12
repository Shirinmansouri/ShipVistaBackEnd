using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Plants.Domain.Model
{
    public class PlantRequest : BaseRequest
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public DateTime LastWateringDate { get; set; }
        public byte[] plantImage { get; set; }
        public IFormFile formFile { get; set; }

    }
}
