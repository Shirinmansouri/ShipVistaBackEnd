using System;
using System.Collections.Generic;
using System.Text;

namespace Plants.Domain.Model
{
    public class PlantResponse : BaseResponse
    {
        public List<PlantRow> plants { get; set; }
        public int ResultCount { get; set; }
    }
}
