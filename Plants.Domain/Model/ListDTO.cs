using System;
using System.Collections.Generic;
using System.Text;

namespace Plants.Domain.Model
{
    public class ListDTO : BaseRequest
    {
        public int PageNum { get; set; }
        public int PageSize { get; set; }
        public string Filter { get; set; }
        public bool IsRequestCount { get; set; }
 
    }
}
