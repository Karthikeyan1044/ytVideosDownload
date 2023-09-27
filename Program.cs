//var builder = WebApplication.CreateBuilder(args);

//// Add services to the container.

//builder.Services.AddControllers();
//// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

//var app = builder.Build();

//// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();

//    app.UseCors(options => options.SetIsOriginAllowed(x => _ = true).AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
//    app.UseHttpsRedirection();
//}

//app.UseAuthorization();

//app.MapControllers();

//app.Run();

//app.UseEndpoints(endpoints =>
//{
//    endpoints.MapControllers();
//});
using Microsoft.AspNetCore.Hosting;
 

namespace ytVideosDownload
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //Log.Logger = new LoggerConfiguration()
            //       .MinimumLevel.Information()
            //       .WriteTo.Debug(new RenderedCompactJsonFormatter())
            //       .WriteTo.File(@"EmployeeManagementLogs/EmployeeManagement_LogFiles_.txt", rollingInterval: RollingInterval.Day)
            //       .CreateLogger();

            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
               // .UseSerilog()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<StartUp>();
                });
    }
}
