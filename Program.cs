using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using WebApplicationSports.Data;



namespace WebApplicationSports
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

           

            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<IdentityRole>() //added 
                .AddEntityFrameworkStores<ApplicationDbContext>();
            builder.Services.AddControllersWithViews();

            //added then after running the install oackage.......
            builder.Services.AddAuthentication()
                    .AddGoogle(options =>
                        {
                            IConfigurationSection googleAuthNSection = builder.Configuration.GetSection("Authentication:Google");

                            options.ClientId = googleAuthNSection["ClientId"];
                            options.ClientSecret = googleAuthNSection["ClientSecret"];

                        })

                    //not working- because waiting for aproval
                    ////.AddFacebook(options =>
                    ////{
                    ////    IConfigurationSection FBAuthNSection = builder.Configuration.GetSection("Authentication:FB");
                    ////    options.ClientId = FBAuthNSection["ClientId"];
                    ////    options.ClientSecret = FBAuthNSection["ClientSecret"];
                    ////})
                    

                    //Not working--- I dont know exactly why but has something to do with gerogian college account
                    .AddMicrosoftAccount(microsoftOptions =>
                    {
                        microsoftOptions.ClientId = builder.Configuration["Authentication:Microsoft:ClientId"];
                        microsoftOptions.ClientSecret = builder.Configuration["Authentication:Microsoft:ClientSecret"];
                    })
                    .AddGitHub(options =>
                    {

                        options.ClientId = builder.Configuration["Authentication:GitHub:ClientId"];
                        options.ClientSecret = builder.Configuration["Authentication:GitHub:ClientSecret"];
                        

                    });

                     var app = builder.Build();

                   

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            app.MapRazorPages();

            app.Run();
        }
    }
}
