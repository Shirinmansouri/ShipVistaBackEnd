using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Plants.API.Configuration
{
    public class JwtConfig
    {
        public string Secret { get; set; }
        public int TokenExpirationHours { get; set; }

    }
}
