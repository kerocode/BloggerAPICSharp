using BloggerAPICSharp.DataLayer.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace BloggerAPICSharp.Services
{
    public class BloggerProfileService : AspNetIdentityProfileService<ApplicationUser>
    {
    }
}
