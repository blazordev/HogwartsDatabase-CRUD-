using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Hogwarts.Client.Services;


namespace Hogwarts.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");
           
            builder.Services.AddTransient(sp => new HttpClient { BaseAddress = new Uri("https://localhost:5001/") });
            builder.Services.AddScoped<StaffDataService>();
            builder.Services.AddScoped<StudentDataService>();
            builder.Services.AddScoped<RolesDataService>();
            builder.Services.AddScoped<HouseDataService>();
            builder.Services.AddScoped<CourseDataService>();
            builder.Services.AddScoped<HouseHeadDataService>();
            var host = builder.Build();

           

            await builder.Build().RunAsync();
        }
    }
}
