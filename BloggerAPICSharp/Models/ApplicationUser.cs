using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace BloggerAPICSharp.Models
{
    public class ApplicationUser: IdentityUser
    {

      public bool IsAdmin { get; }
  //      public ApplicationUser()
  //      {
  //      }
  //      public string FirstName
  //      {
  //          get;
  //          set;
  //      }
		//public string LastName
		//{
		//	get;
		//	set;
		//}
		//public string Email
		//{
		//	get;
		//	set;
		//}
		//public string Password
		//{
		//	get;
		//	set;
		//}
    }
}
