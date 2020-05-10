using System;
using System.Security.Policy;
using API.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace API
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
      services.AddControllers()
        .AddNewtonsoftJson(options => {
          options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
        });

      var baseApiUrl = Configuration["BaseApiUrl"];
      services.AddHttpClient("API Client", client => {
        client.BaseAddress = new Uri(baseApiUrl);
        client.DefaultRequestHeaders.Add("Accept", "application/json");
      });

      services.AddApplicationServices();

      services.AddSwaggerGen(options => {
        options.SwaggerDoc("v1",
          new OpenApiInfo {
            Title = "JobAdder Coding Challenge API",
            Version = "v1"
          });
      });
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }

      app.UseHttpsRedirection();

      app.UseRouting();

      app.UseAuthorization();

      app.UseSwagger();
      app.UseSwaggerUI(options => {
        options.SwaggerEndpoint(
          "/swagger/v1/swagger.json",
          "JobAdder Coding Challenge API v1");
      });

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
      });
    }
  }
}
