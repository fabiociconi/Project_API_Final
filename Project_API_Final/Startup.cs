using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using Project_API_Final.Models.Auto;
using Swashbuckle.AspNetCore.Swagger;
using System.Text;

namespace Project_API_Final
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

			services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

			services.AddDbContext<DBForumContext>(options =>
		options.UseSqlServer(Configuration.GetConnectionString("ForumDataBaseConnection")));

			// XML and JSON Format Support
			services.AddMvc(options =>
			{
				options.FormatterMappings.SetMediaTypeMappingForFormat("xml", MediaTypeHeaderValue.Parse("application/xml"));
				options.FormatterMappings.SetMediaTypeMappingForFormat("config", MediaTypeHeaderValue.Parse("application/xml"));
				options.FormatterMappings.SetMediaTypeMappingForFormat("js", MediaTypeHeaderValue.Parse("application/json"));
			}).AddXmlSerializerFormatters();


			services
			
				.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
				.AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, cfg =>
				{
					cfg.RequireHttpsMetadata = false;
					cfg.SaveToken = true;

					cfg.TokenValidationParameters = new TokenValidationParameters()
					{
						ValidIssuer = AppConstants.TokenAudience,
						ValidAudience = AppConstants.TokenAudience,
						IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(AppConstants.TokenKey))
					};
				});


			// Register the Swagger generator, defining one or more Swagger documents
			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new Info
				{
					Title = "FinalProject API",
					Version = "v1",
					Description = "Forum Control",
					TermsOfService = "None",
					Contact = new
					Contact
					{ Name = "Fabio Alexandre Ciconi & Rodrigo Geronimo", Email = "", Url = "" },


				});

				c.AddSecurityDefinition("Bearer", new ApiKeyScheme()
				{
					Description = "JWT Authorization header using the Bearer scheme. Example: \"Bearer {token}\"",
					Name = "Authorization",
					In = "header",
					Type = "apiKey"
				});
			});
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			#region
			//teste
			//app
			//	.UseAuthentication()
			//	.Use(async (context, next) =>
			//	{
			//		await next();

			//		if (context.Response.StatusCode == 404 && !Path.HasExtension(context.Request.Path.Value))
			//		{
			//			context.Request.Path = "/index.html";
			//			await next();
			//		}
			//	})
			//	.UseDefaultFiles(new DefaultFilesOptions { DefaultFileNames = new List<string> { "index.html" } })
			//	.UseStaticFiles()//www.root
			#endregion
			app.UseMvc(routes =>
				{
					routes.MapRoute(
					name: "api",
					template: "api/{controller}/{action}/{id?}");
				});

			//fim-teste
			app
			
				.UseSwagger()
				.UseSwaggerUI(c =>
				{
					c.ShowJsonEditor();
					c.SwaggerEndpoint("/swagger/v1/swagger.json", "FinalProject API");
				});
		}
	}
}
