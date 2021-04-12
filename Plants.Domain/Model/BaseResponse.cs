using System;
using System.Collections.Generic;
using System.Text;

namespace Plants.Domain.Model
{
  public   class BaseResponse
    {
        public int responseCode { get; set; }
        public string responseMessage { get; set; }
        public bool HasError { get; set; }
    }
}
