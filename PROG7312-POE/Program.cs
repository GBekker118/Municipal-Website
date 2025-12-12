using PROG7312_POE.Data;
using PROG7312_POE.Models;
namespace PROG7312_POE
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            // Use singletons so the same instance is used throughout the app
            builder.Services.AddSingleton<EventQueue>();
            builder.Services.AddSingleton<AnnouncementQueue>();
            builder.Services.AddSingleton<EventsAndAnnouncements>();

            // Add session services
            builder.Services.AddDistributedMemoryCache();
            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            var app = builder.Build();

            // Call Events and Announcements Dummy Data 
            using (var scope = app.Services.CreateScope())
            {
                var eventQueue = scope.ServiceProvider.GetRequiredService<EventQueue>();
                var announcementQueue = scope.ServiceProvider.GetRequiredService<AnnouncementQueue>();

                // Call dummy data seeder
                DummyData.EventsAndAnnouncementsData(eventQueue, announcementQueue);
            }

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseSession();
            app.UseRouting();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}