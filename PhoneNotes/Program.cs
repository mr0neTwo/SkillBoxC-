using DatabasePN;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PhoneNoteApplication.Models;
using PhoneNotes.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var appConnectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<DatabaseContext>(options => options.UseNpgsql(appConnectionString));

builder.Services.AddIdentity<User, Role>
	   (
		   options =>
		   {
			   options.Password.RequiredLength = 6;
			   options.Password.RequireDigit = false;
			   options.Password.RequireLowercase = false;
			   options.Password.RequireUppercase = false;
			   options.Password.RequireNonAlphanumeric = false;

			   options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10);
			   options.Lockout.MaxFailedAccessAttempts = 10;
			   options.Lockout.AllowedForNewUsers = true;

			   options.User.AllowedUserNameCharacters =
				   "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";

			   options.User.RequireUniqueEmail = false;
		   }
	   )
	   .AddEntityFrameworkStores<DatabaseContext>()
	   .AddDefaultTokenProviders();

builder.Services.ConfigureApplicationCookie
(
	options =>
	{
		options.Cookie.HttpOnly = true;
		options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
		options.LoginPath = "/Account/Login";
		options.LogoutPath = "/Account/Logout";
		options.AccessDeniedPath = "/Account/AccessDenied";
		options.SlidingExpiration = true;
	}
);

WebApplication app = builder.Build();

using (var scope = app.Services.CreateScope())
{
	IServiceProvider serviceProvider = scope.ServiceProvider;

	try
	{
		DatabaseContext context = serviceProvider.GetRequiredService<DatabaseContext>();
		UserManager<User> userManager = serviceProvider.GetRequiredService<UserManager<User>>();
		RoleManager<Role> roleManager = serviceProvider.GetRequiredService<RoleManager<Role>>();
		
		await DatabaseInitializer.Rebuild(context, userManager, roleManager);
	}
	catch (Exception exception)
	{
		Console.WriteLine("Error");
	}
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();// Ensure this is before UseAuthorization
app.UseAuthorization();

app.MapControllerRoute(name : "default", pattern : "{controller=Note}/{action=List}/{id?}");

app.Run();
