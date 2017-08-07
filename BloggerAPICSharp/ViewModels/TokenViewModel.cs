using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloggerAPICSharp.ViewModels
{
    public class TokenViewModel
    {
        public string token { get; set; }
        public DateTime expiration { get; set; }
    }
}
