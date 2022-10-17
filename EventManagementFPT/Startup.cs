using CookingBakery.BakeryModules.CategoryModule;
using CookingBakery.BakeryModules.CategoryModule.Interface;
using CookingBakery.BakeryModules.CommentModule;
using CookingBakery.BakeryModules.CommentModule.Interface;
using CookingBakery.BakeryModules.PostDetailModule;
using CookingBakery.BakeryModules.PostDetailModule.Interface;
using CookingBakery.BakeryModules.PostModule;
using CookingBakery.BakeryModules.PostModule.Interface;
using CookingBakery.BakeryModules.PostReactionModule;
using CookingBakery.BakeryModules.PostReactionModule.Interface;
using CookingBakery.BakeryModules.ProductModule;
using CookingBakery.BakeryModules.ProductModule.Interface;
using CookingBakery.BakeryModules.UserModule;
using CookingBakery.BakeryModules.UserModule.Interface;
using CookingBakery.Hubs;
using CookingBakery.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CookingBakery
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
            services
                .AddSignalR(e =>
                {
                    e.EnableDetailedErrors = true;
                    e.MaximumReceiveMessageSize = 102400000;
                });
            //Add scope and dependency injection
            //User Module
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserService, UserService>();
            //Category Module
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ICategoryService, CategoryService>();
            //Comment Module
            services.AddScoped<ICommentRepository, CommentRepository>();
            services.AddScoped<ICommentService, CommentService>();
            //Post Detail Module
            services.AddScoped<IPostDetailRepository, PostDetailRepository>();
            services.AddScoped<IPostDetailService, PostDetailService>();
            //Post Module
            services.AddScoped<IPostRepository, PostRepository>();
            services.AddScoped<IPostService, PostService>();
            //Post Reaction Module
            services.AddScoped<IPostReactionRepository, PostReactionRepository>();
            services.AddScoped<IPostReactionService, PostReactionService>();
            //Product Module
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductService, ProductService>();

            services.AddDbContext<CookingBakeryContext>(
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
                endpoints.MapHub<SignalRServer>("/signalrServer");
            });
        }
    }
}