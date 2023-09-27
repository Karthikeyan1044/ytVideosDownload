using Microsoft.AspNetCore.DataProtection;
using Microsoft.Azure.Mobile.Server.Config;
using Microsoft.OpenApi.Models;
using System.Globalization;

namespace ytVideosDownload
{


    public class StartUp
    {
        public StartUp(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            //services.AddDbContext<DemoDBContext>(opts =>
            //{
            //    opts.UseMySql(Configuration.GetConnectionString("DefaultConnection"));
            //});
            //string connectionString = Configuration.GetConnectionString("DefaultConnection");
            //services.AddDbContext<DemoDBContext>(options =>

            // options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString).
            // mysqlOptions.MigrationsAssembly("DemoProject");

            // ));
            string connectionString = Configuration.GetConnectionString("DefaultConnection");

            //services.AddDbContext<DemoDBContext>(options =>
            //{
            //    var serverVersion = ServerVersion.AutoDetect(connectionString);

            //    options.UseMySql(connectionString, serverVersion, mysqlOptions =>
            //    {
            //        mysqlOptions.MigrationsAssembly("DemoProject");
            //    });
            //});

            //services.AddTransient<ICustomerService, CustomerService>();
            //services.AddTransient<ICustomerRepository, CustomerRepository>();
            // services.AddTransient<>();
            services.AddSwaggerGen(context =>
            {
                context.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Test API",
                    Version = "v1"
                });
            });

            services.AddCors(options =>
            {
                options.AddPolicy("AllowOrigin",
                    builder => builder.AllowAnyOrigin()
                                      .AllowAnyMethod()
                                      .AllowAnyHeader());
            });



            services.AddHttpContextAccessor();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(context =>
                {
                    context.SwaggerEndpoint("v1/swagger.json", "My SmartTools API V1");
                });
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseCors(x => x
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());


            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}