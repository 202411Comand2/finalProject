using Ocelot.DependencyInjection;
using Ocelot.Middleware;

namespace OcelotWebApiGr
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            // Добавляем Ocelot и загружаем конфигурацию
            builder.Configuration.AddJsonFile("Properties/ocelot.json");
            builder.Services.AddOcelot(builder.Configuration);
            var app = builder.Build();
            app.UseOcelot().Wait();
            app.Run();

            /* var builder = WebApplication.CreateBuilder(args);

             // Add services to the container.
             builder.Services.AddRazorPages();

             var app = builder.Build();

             // Configure the HTTP request pipeline.
             if (!app.Environment.IsDevelopment())
             {
                 app.UseExceptionHandler("/Error");
                 // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                 app.UseHsts();
             }

             app.UseHttpsRedirection();
             app.UseStaticFiles();

             app.UseRouting();

             app.UseAuthorization();

             app.MapRazorPages();

             app.Run();*/
        }
    }
}
