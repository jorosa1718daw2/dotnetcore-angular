using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SMECService.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace SMECService
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
            services.AddDbContext<CEMSContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddMvc().AddJsonOptions(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });
            //ASP.NET Identity support
           /* services.AddIdentity<ApplicationUser, IdentityRole>(
                opts =>
                {
                    opts.Password.RequireDigit = true;
                    opts.Password.RequireLowercase = true;
                    opts.Password.RequireLowercase = true;
                    opts.Password.RequireNonAlphanumeric = false;
                    opts.Password.RequiredLength = 7;

                }).AddEntityFrameworkStores<ApplicationDbContext>();*/

                //Auth with JWT Tokens
                services.AddAuthentication(opts =>
                { 
                    opts.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                    opts.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    opts.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(cfg =>
                { cfg.RequireHttpsMetadata = false;
                cfg.SaveToken = true;
                cfg.TokenValidationParameters = new TokenValidationParameters()
                {
                    //standard config
                    ValidIssuer = Configuration["Auth:Jwt:Issuer"],
                    ValidAudience = Configuration["Auth:Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey (
                        Encoding.UTF8.GetBytes(Configuration["Auth:Jwt:Key"])
                    ),
                   // ClockSkew = TimeSpan.Zero,
                    //security switches
                    RequireExpirationTime = true,
                    ValidateIssuer = true,
                    ValidateIssuerSigningKey = true,
                    ValidateAudience = true
                };
                });



            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {


            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseSpaStaticFiles();
            //Authentication middleware to the pipeline
            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501

                spa.Options.SourcePath = "ClientApp";

                 /*if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                }*/
                spa.UseProxyToSpaDevelopmentServer("http://localhost:4200");
            });
        }
    }
}
