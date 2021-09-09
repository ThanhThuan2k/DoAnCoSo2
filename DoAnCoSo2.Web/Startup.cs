using DoAnCoSo2.Data.Constant;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Serialization;

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

			// Json Serialize
			services.AddControllersWithViews().AddNewtonsoftJson(options =>
				options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore)
				.AddNewtonsoftJson(options => options.SerializerSettings.ContractResolver = new DefaultContractResolver());

			services.AddControllers();
			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new OpenApiInfo { Title = "DoAnCoSo2.Web", Version = "v1" });
			});
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
			}

			app.UseHttpsRedirection();

			app.UseRouting();

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
					});
			});
		}
	}
}