using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloggerAPICSharp.DataLayer.DomainModel
{
    public class ApplicationRole : IdentityRole<Guid>
    {
    }
}
