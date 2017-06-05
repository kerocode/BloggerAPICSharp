using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace BloggerAPICSharp.DataLayer.DomainModel
{
    public class ApplicationUser:IdentityUser
    {

    public bool IsAdmin { get; }
    public ICollection<Post> Posts { get; set; }   
    }
}
