using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BloggerAPICSharp.DataLayer.DomainModel;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace BloggerAPICSharp
{
  public class Startup
  {
    public Startup(IHostingEnvironment env)
    {
      var builder = new ConfigurationBuilder()
          .SetBasePath(env.ContentRootPath)
          .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
          .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
          .AddEnvironmentVariables();
      Configuration = builder.Build();
    }

    public IConfigurationRoot Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      // Add framework services.
      services.AddMvc();
      
      //var connection = @"Server=(localdb)\mssqllocaldb;Database=Blogger.AspNetCore.NewDb;Trusted_Connection=True;";
      services.AddDbContext<BloggerDbContext>(options => options.UseInMemoryDatabase());
      services.AddIdentity<ApplicationUser, IdentityRole>(options =>
      {
        options.Password.RequireDigit = true;
        options.Password.RequireLowercase = true;
        options.Password.RequireUppercase = true;
        options.Password.RequireNonAlphanumeric = true;
        options.Password.RequiredLength = 6;

      })
      .AddEntityFrameworkStores<BloggerDbContext>()
      .AddDefaultTokenProviders();
      services.AddIdentityServer(options=> {
        options.UserInteraction.LoginUrl = "/login";
        options.UserInteraction.LogoutUrl = "/logout";


      })
      .AddConfigurationStore(options => options.UseInMemoryDatabase())
      .AddOperationalStore(options => options.UseInMemoryDatabase())
      .AddAspNetIdentity<ApplicationUser>();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
    {
      loggerFactory.AddConsole(Configuration.GetSection("Logging"));
      loggerFactory.AddDebug();

      app.UseMvc();
      app.UseIdentity();
      app.UseIdentityServer();
    }
  }
}
