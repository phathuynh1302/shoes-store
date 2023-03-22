using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PRN211_ShoesStore.Service;
using PRN211_ShoesStore.Models;
using PRN211_ShoesStore.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using PRN211_ShoesStore.Utils.Interface;
using PRN211_ShoesStore.Utils;

namespace PRN211_ShoesStore
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

			services.AddHttpContextAccessor();
			services.AddSession();

			services.AddControllersWithViews();
			services.AddRazorPages();
			//phat add authen
			services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
	.AddCookie(options =>
	{
		options.AccessDeniedPath = "/Account/AccessDenied";
		options.LoginPath = "/Account/Login";
	});

			//phat add authorize
			services.AddAuthorization(options =>
			{
				options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
			});

			//phat add role, identity
			services.AddDbContext<AppDbContext>(option =>
			{
				string connectString = Configuration.GetConnectionString("AppConnectString");
				option.UseSqlServer(connectString);
			});
			//phat ad identity system
			services.AddIdentity<IdentityUser, IdentityRole>()
	.AddEntityFrameworkStores<ApplicationDbContext>()
	.AddDefaultTokenProviders();
			services.AddDbContext<ApplicationDbContext>(options =>
			options.UseSqlServer(Configuration.GetConnectionString("AppConnectString")));

			services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
			services.AddScoped<IShoesService, ShoesService>();
			services.AddScoped<ICategoryService, CategoryService>();
			services.AddScoped<IOrderService, OrderService>();
			services.AddScoped<ICartService, CartService>();
			services.AddSingleton<UserRepository>();
			services.AddSingleton<RoleRepository>();
			services.AddSingleton<ShoesRepository>();
			services.AddSingleton<ShoesImageRepository>();
			services.AddSingleton<UserService>();
			//services.AddSingleton<OrderService>();





			//
			services.AddScoped<AppDbContext>();
			services.AddScoped<IConfig, Config>();
			services.AddScoped<IJwtService, JwtService>();
			services.AddScoped<IUploadFileService, UploadFileService>();
			services.AddScoped<Repository.vH.Interface.IUserRepository, Repository.vH.UserRepository2>();
			services.AddScoped<Service.vH.Interface.IUserService, Service.vH.UserService2>();
			services.AddScoped<Repository.vH.Interface.ICategoryRepository, Repository.vH.CategoryRepository2>();
			services.AddScoped<Service.vH.Interface.ICategoryService, Service.vH.CategoryService2>();
			services.AddScoped<Repository.vH.Interface.ISizeRepository, Repository.vH.SizeRepository2>();
			services.AddScoped<Service.vH.Interface.ISizeService, Service.vH.SizeService2>();
			services.AddScoped<Repository.vH.Interface.IColorRepository, Repository.vH.ColorRepository2>();
			services.AddScoped<Service.vH.Interface.IColorService, Service.vH.ColorService2>();
			services.AddScoped<Repository.vH.Interface.IShoesRepository, Repository.vH.ShoesRepository2>();
			services.AddScoped<Service.vH.Interface.IShoesService, Service.vH.ShoesService2>();
			services.AddScoped<Repository.vH.Interface.IImageRepository, Repository.vH.ImageRepository2>();
			services.AddScoped<Service.vH.Interface.IImageService, Service.vH.ImageService2>();
			services.AddScoped<Repository.vH.Interface.IOrderRepository, Repository.vH.OrderRepository2>();
			services.AddScoped<Service.vH.Interface.IOrderService, Service.vH.OrderService2>();
			services.AddScoped<Repository.vH.Interface.IOrderDetailRepository, Repository.vH.OrderDetailRepository2>();
			services.AddScoped<Service.vH.Interface.IOrderDetailService, Service.vH.OrderDetailService2>();
			services.AddScoped(typeof(Repository.vH.Interface.IRepository<>), typeof(Repository.vH.Repository<>));
			//
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
				app.UseExceptionHandler("/Home/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}

			app.UseSession();
			app.UseHttpsRedirection();
			app.UseStaticFiles();
			//phat use authen. author
			app.UseAuthentication();

			app.UseAuthorization();

			app.UseRouting();

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllerRoute(
					name: "default",
					pattern: "{controller=Home}/{action=Login}/{id?}");
			});

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllerRoute(
					name: "shoes",
					pattern: "{controller=Shoes}/{action=Index}/{id?}");
			});



		}
	}
}