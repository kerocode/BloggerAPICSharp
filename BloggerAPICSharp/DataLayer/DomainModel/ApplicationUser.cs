using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace BloggerAPICSharp.DataLayer.DomainModel
{
    public class ApplicationUser : IdentityUser
    {
        public string Password { get; set; }

        public string firstName { get; set; }
        public string lastName { get; set; }
    }
}
