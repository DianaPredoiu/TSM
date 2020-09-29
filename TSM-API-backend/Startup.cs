/*******************************************************
 * file\
 * Startup.cs file is the root folder of the project.It contains
 * Startup class, which is a default ASP.NET Core class with 
 * configuration methods.
 * 
 ******************************************************/

//namespaces used in Startup class
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using WebApi.Helpers;
using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using WebApi.Services;
//list of namespaces



/***********************************************************
 * 
 * WebApi namespace is called after the name of the backend 
 * API and it contains the Startup class.
 * 
 **********************************************************/
namespace WebApi
{
    

    /*******************************************************
     * 
     * Startup class is a default class in ASP.NET
     * Core and,it is executed first when the application 
     * starts.
     * 
     ******************************************************/
    public class Startup
    {
        

        /*******************************************************
         * 
         * Startup Constructor takes an IConfiguration type
         * parameter to set the configuration of the application.
         * 
         ******************************************************/
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        ///Property used in the Startup Constructor
        public IConfiguration Configuration { get; }

        

        /*************************************************************************************************************************************************************************************
         * 
         * ConfigureServices configures the app's services.
         * Services are registered in this method and consumed
         * across the app via DI or ApplicationServices.
         * 
         * For more details go to: https://docs.microsoft.com/en-us/aspnet/core/fundamentals/startup?view=aspnetcore-3.1
         * 
         * App's configure services(from IServiceCollection) contain:
         *   + AddCors: Cors(cross origin resource sharing services)
         *   + AddDbCotext: Registers the given context(DataContext) as a service 
         *   + AddMvc: Adds MVC services
         *   + AddAutoMapper: Adds auto-mapper services
         *   + AddAuthentication: Completes authentication-related actions
         *   + AddJwtBearer: Sets up the jwt bearer authentication
         *   + AddScoped: Adds a scoped serivce
         *   
         * For more details about IServiceCollection: https://docs.microsoft.com/en-us/dotnet/api/microsoft.extensions.dependencyinjection.iservicecollection?view=dotnet-plat-ext-3.1
         * For more details about Jwt Bearer: https://docs.microsoft.com/en-us/dotnet/api/microsoft.extensions.dependencyinjection.jwtbearerextensions?view=aspnetcore-3.0
         *   
         * This Method also configures (byDI) services from Services file:
         *   + UserService
         *   + TimesheetService
         *   + TimesheetActivityService
         *   + TeamService
         *   + RoleService
         *   + ProjectService
         *   + ProjectManagerSerivce
         *   + ProjectAssignmentsService
         *   + LocationService
         *   
         * For more details go to Services paragraph of the documentation
         * 
         **************************************************************************************************************************************************************************************/
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            services.AddDbContext<DataContext>(opts => opts.UseSqlServer(Configuration["ConnectionStrings:DefaultConnection"]));
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddAutoMapper();
            

            // configure strongly typed settings objects
            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);

            // configure jwt authentication
            var appSettings = appSettingsSection.Get<AppSettings>();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.Events = new JwtBearerEvents
                {
                    OnTokenValidated = context =>
                    {
                        var userService = context.HttpContext.RequestServices.GetRequiredService<IUserService>();
                        var userId = int.Parse(context.Principal.Identity.Name);
                        var user = userService.GetById(userId);
                        if (user == null)
                        {
                            // return unauthorized if user no longer exists
                            context.Fail("Unauthorized");
                        }
                        return Task.CompletedTask;
                    }
                };
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

           //configure DI for application services
           services.AddScoped<IUserService, UserService>();
           services.AddScoped<ITimesheetService, TimesheetService>();
           services.AddScoped<IProjectService, ProjectService>();
           services.AddScoped<IRoleService, RoleService>();
           services.AddScoped<ITeamService, TeamService>();
           services.AddScoped<IProjectService, ProjectService>();
           services.AddScoped<ILocationService, LocationService>();
           services.AddScoped<ITimesheetService, TimesheetService>();
           services.AddScoped<ITimesheetActivityService, TimesheetActivityService>();
           services.AddScoped<IProjectManagerService, ProjectManagerService>();
           services.AddScoped<IProjectAssignmentsService, ProjectAssignmentsService>();

        }//METHOD ConfigureServices


        
        /*******************************************************
         * 
         * Configure Method is userd to specify how the app 
         * responds to HTTP requests.
         * 
         * This method contains app parameter of type IApplicationBuilder
         * App acceses the following extensions:
         *   + UseCors: Adds CorsMiddleware(AllowAnyOrigin,AllowAnyMethod,AllowAnyHeader)
         *   + UseAuthentication: Enables authentication abilities
         *   + UseMvc: Adds Mvc to AppBuilder
         *   
         * For more details go to: https://docs.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.builder.applicationbuilder?view=aspnetcore-3.1
         * 
         ******************************************************/
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            // global cors policy
            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.UseAuthentication();

            app.UseMvc();

        }//METHOD Configure

    }//CLASS Startup 

}//NAMESPACE WebApi
