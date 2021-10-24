using Data;
using Data.Interfaces;
using Data.Repositories;
using Entity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Services.Interfaces;
using Services.Services;

namespace ReadLater5
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
            services.AddDbContext<ReadLaterDataContext>(options =>
               options.UseSqlServer(
                   Configuration.GetConnectionString("DefaultConnection")));

            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddDefaultIdentity<User>()
                .AddEntityFrameworkStores<ReadLaterDataContext>();

            services.ConfigureApplicationCookie(opt =>
            {
                opt.LoginPath = new PathString("/User/Login");
            });

            services.AddControllersWithViews(opt =>
            {
                opt.EnableEndpointRouting = false;
            }).AddRazorRuntimeCompilation();

            services.AddAuthentication()
                .AddGoogle(opt =>
                {
                    opt.ClientId = Configuration.GetValue<string>("GoogleAuthentication:ClientId");
                    opt.ClientSecret = Configuration.GetValue<string>("GoogleAuthentication:ClientSecret");
                })
                .AddFacebook(opt =>
                {
                    opt.AppId = Configuration.GetValue<string>("FacebookAuthentication:AppId");
                    opt.AppSecret = Configuration.GetValue<string>("FacebookAuthentication:AppSecret");
                });

            services.AddTransient<ICategoryRepository, CategoryRepository>();
            services.AddTransient<IBookmarkRepository, BookmarkRepository>();
            services.AddTransient<IClientRepository, ClientRepository>();
            services.AddTransient<IFavouriteBookmarkRepository, FavouriteBookmarkRepository>();

            services.AddTransient<IUserService, UserService>();
            services.AddTransient<ICategoryService, CategoryService>();
            services.AddTransient<IBookmarkService, BookmarkService>();
            services.AddTransient<IFavouriteBookmarkService, FavouriteBookmarkService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
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
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Categories}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
