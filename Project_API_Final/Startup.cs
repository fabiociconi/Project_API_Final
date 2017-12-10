using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;

using Microsoft.EntityFrameworkCore;
using Project_API_Final.Models;
using Microsoft.Extensions.PlatformAbstractions;
using System.IO;
using Microsoft.Net.Http.Headers;
//using Project_API_Final.Models;

namespace Project_API_Final
{
	public class Startup
	{
		//public Startup(IHostingEnvironment env)
		//{
		//	var builder = new ConfigurationBuilder()
		//		.SetBasePath(env.ContentRootPath)
		//		.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
		//		.AddJsonFile($"appsettings.Development.json", optional: true);
		//	//if (env.IsDevelopment())
		//	//{
		//	//	builder.AddUserSecrets<Startup>();
		//	//}

		//	builder.AddEnvironmentVariables();
		//	Configuration = builder.Build();

		//}

		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

	
		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddDbContext<DBForumContext>(options =>
		options.UseSqlServer(Configuration.GetConnectionString("ForumDataBaseConnection")));

			//DBForumContext.ConnectionString = Configuration.GetConnectionString("ForumDataBaseConnection");

			// XML and JSON Format Support
			services.AddMvc(options =>
			{
				options.FormatterMappings.SetMediaTypeMappingForFormat("xml", MediaTypeHeaderValue.Parse("application/xml"));
				options.FormatterMappings.SetMediaTypeMappingForFormat("config", MediaTypeHeaderValue.Parse("application/xml"));
				options.FormatterMappings.SetMediaTypeMappingForFormat("js", MediaTypeHeaderValue.Parse("application/json"));
			}).AddXmlSerializerFormatters();


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
			//	.UseMvc(routes =>
			//	{
			//		routes.MapRoute(
			//		name: "api",
			//		template: "api/{controller}/{action}/{id?}");
			//	});

			//fim-teste
			app
				.UseMvc()
				.UseSwagger()
				.UseSwaggerUI(c =>
				{
					c.ShowJsonEditor();
					c.SwaggerEndpoint("/swagger/v1/swagger.json", "FinalProject API");
				});
		}
	}
}
