using DoAnCoSo2.Data;
using DoAnCoSo2.Data.Common;
using DoAnCoSo2.Data.Constant;
using DoAnCoSo2.Data.Interfaces.Repositories;
using DoAnCoSo2.Data.Interfaces.Repositories.Auth;
using DoAnCoSo2.Data.Repositories.Auth;
using DoAnCoSo2.Data.Repositories.Sys;
using DoAnCoSo2.Data.Services.CRUDService;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Serialization;
using System;
using System.Text;

namespace DoAnCoSo2.Web
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
			// enable cors
			services.AddCors(cors =>
			{
				cors.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin()
					.AllowAnyMethod().AllowAnyHeader());
			});

			services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
				.AddJwtBearer(opt => {
					opt.TokenValidationParameters = new TokenValidationParameters
					{
						ValidateIssuer = true,
						ValidateAudience = true,
						ValidateLifetime = true,
						ValidateIssuerSigningKey = true,
						ValidIssuer = AppConstant.ISSUER,
						ValidAudience = AppConstant.ISSUER,
						IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(AppConstant.SECURITY_KEY))
					};
				});

			// Json Serialize
			services.AddControllersWithViews().AddNewtonsoftJson(options =>
				options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore)
				.AddNewtonsoftJson(options => options.SerializerSettings.ContractResolver = new DefaultContractResolver());

			services.AddControllers();
			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new OpenApiInfo { Title = "DoAnCoSo2.Web", Version = "v1" });
			});

			services.AddDbContext<DoAnCoSo2DbContext>();
			services.AddScoped<JwtService>();
			services.AddScoped<IAdminRepository, AdminRepository>();
			services.AddScoped<ISysErrorRepository, SysErrorRepository>();
			services.AddScoped(typeof(CRUDService));
			services.AddScoped<ICustomerRepository, CustomerRepository>();

			services.AddControllersWithViews().AddNewtonsoftJson(
				option =>
			option.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
		);
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				app.UseSwagger();
				app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "DoAnCoSo2.Web v1"));

				app.UseHttpsRedirection();
				app.UseCors(option => option.AllowAnyHeader().AllowAnyMethod().AllowCredentials());
			}

			app.UseHttpsRedirection();

			app.UseRouting();

			app.UseAuthentication();
			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
				app.UseEndpoints(endpoints =>
					{
						endpoints.MapControllerRoute(
						name: "areas",
						pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
						 );

						endpoints.MapAreaControllerRoute(
						name: AppConstant.ADMIN_AREA_NAME,
						areaName: AppConstant.ADMIN_AREA_NAME,
						pattern: AppConstant.ADMIN_AREA_NAME + "/{controller=Home}/{action=Index}/{id?}"
						);

						endpoints.MapAreaControllerRoute(
								name: AppConstant.SHOP_AREA_NAME,
								areaName: AppConstant.SHOP_AREA_NAME,
								pattern: AppConstant.SHOP_AREA_NAME + "/{controller=Home}/{action=Index}/{id?}"
						);

					});
			});
		}
	}
}