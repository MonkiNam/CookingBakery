using EventManagementFPT.Model;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using EventManagementFPT.Modules.CommentModule.Interface;
using EventManagementFPT.Modules.CommentModule;
using EventManagementFPT.Modules.EventModule.Interface;
using EventManagementFPT.Modules.EventModule;
using EventManagementFPT.Modules.UserModule.Interface;
using EventManagementFPT.Modules.UserModule;
using EventManagementFPT.Modules.CategoryModule.Interface;
using EventManagementFPT.Modules.CategoryModule;
using EventManagementFPT.Modules.EventLikeModule.Interface;
using EventManagementFPT.Modules.EventLikeModule;
using EventManagementFPT.Modules.UserEventModule.Interface;
using EventManagementFPT.Modules.UserEventModule;
using EventManagementFPT.Modules.VenueModule.Interface;
using EventManagementFPT.Modules.VenueModule;

namespace EventManagementFPT
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
            services.AddRazorPages();
            services.AddSession();
            //Add scope and dependency injection
            //User Module
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserService, UserService>();
            //Category Module
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ICategoryService, CategoryService>();
            //UserEvent Module
            services.AddScoped<IUserEventRepository, UserEventRepository>();
            services.AddScoped<IUserEventService, UserEventService>();
            //Venue Module
            services.AddScoped<IVenueRepository, VenueRepository>();
            services.AddScoped<IVenueService, VenueService>();
            //Event Module
            services.AddScoped<IEventRepository, EventRepository>();
            services.AddScoped<IEventService, EventService>();
            //EventLike Module
            services.AddScoped<IEventLikeRepository, EventLikeRepository>();
            services.AddScoped<IEventLikeService, EventLikeService>();
            //Comment Module
            services.AddScoped<ICommentRepository, CommentRepository>();
            services.AddScoped<ICommentService, CommentService>();

            services.AddDbContext<EventManagementContext>(
                opt => opt.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")
                )
            );
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(opt =>
            {
                opt.LoginPath = "/Authentication/Index";
                opt.AccessDeniedPath = "/Denied";
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }
    }
}