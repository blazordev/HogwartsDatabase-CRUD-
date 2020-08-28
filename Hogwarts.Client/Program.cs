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
using Hogwarts.Client.Services.ToastService;

namespace Hogwarts.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");
            ConfigureServices(builder.Services);

            await builder.Build().RunAsync();
        }
        private static void ConfigureServices(IServiceCollection services)
        {
            services.AddOptions();
            services.AddTransient(sp => new HttpClient { BaseAddress = new Uri("https://localhost:5001/") });
            services.AddScoped<StaffDataService>();
            services.AddScoped<StudentDataService>();
            services.AddScoped<RolesDataService>();
            services.AddScoped<HouseDataService>();
            services.AddScoped<CourseDataService>();
            services.AddScoped<HouseHeadDataService>();
            services.AddScoped<ToastService>();

        }
    }
}
