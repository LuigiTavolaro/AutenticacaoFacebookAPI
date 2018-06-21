using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using teste.Model;
using EmployeeService.Facebook;

namespace teste
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            string ConnAspNetCore = @"Data Source=HEFESTO\SQLEXPRESS;Initial Catalog=AspNetCore;User ID=luigi;Password=1234";
            services.AddDbContext<AspNetCoreContext>(options => options.UseSqlServer(ConnAspNetCore));

            services.AddAuthentication().AddFacebook(facebookOptions =>
            {
                facebookOptions.AppId = Configuration["1793395334022366"];
                facebookOptions.AppSecret = Configuration["ef95de51ba8dbbea76862f5cecaba4db"];
                facebookOptions.Scope.Add("email");
                facebookOptions.UserInformationEndpoint = "graph.facebook.com/me?fields=email";
                facebookOptions.BackchannelHttpHandler = new FacebookBackChannelHandler();
                
            });
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
