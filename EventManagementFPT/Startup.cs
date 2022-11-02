using BussinessObject.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Repositories.BakeryModules.CategoryModule;
using Repositories.BakeryModules.CategoryModule.Interface;
using Repositories.BakeryModules.CommentModule;
using Repositories.BakeryModules.CommentModule.Interface;
using Repositories.BakeryModules.PostDetailModule;
using Repositories.BakeryModules.PostDetailModule.Interface;
using Repositories.BakeryModules.PostModule;
using Repositories.BakeryModules.PostModule.Interface;
using Repositories.BakeryModules.PostReactionModule;
using Repositories.BakeryModules.PostReactionModule.Interface;
using Repositories.BakeryModules.ProductModule;
using Repositories.BakeryModules.ProductModule.Interface;
using Repositories.BakeryModules.UserModule;
using Repositories.BakeryModules.UserModule.Interface;

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
            services.AddMemoryCache();
            services.AddMvc();
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

            services.AddDbContext<BussinessObject.Models.CookingBakeryContext>(
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
            app.UseSession();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                //endpoints.MapHub<SignalRServer>("/signalrServer");
            });
        }
    }
}