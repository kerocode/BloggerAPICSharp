using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace BloggerAPICSharp.DataLayer.DomainModel
{
    public class ApplicationUser : IdentityUser<Guid>
    {
    

    public ICollection<Post> Posts { get; set; }   
    }
}
