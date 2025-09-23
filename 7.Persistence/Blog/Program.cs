using Blog.Data;

namespace Blog
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            var databaseConfig = builder.Configuration.GetSection("DatabaseConfig").Get<DatabaseConfig>();
            if (databaseConfig!.UseInMemoryDatabase)
            {
                builder.Services.AddSingleton<IArticleRepository, MemoryArticleRepository>();
            }
            else
            {
                builder.Services.AddSingleton<IArticleRepository>(services =>
                {
                    var config = services.GetRequiredService<IConfiguration>();
                    var repository = new ArticleRepository(databaseConfig);

                    repository.EnsureCreated();

                    return repository;
                });
            }

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Articles}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
