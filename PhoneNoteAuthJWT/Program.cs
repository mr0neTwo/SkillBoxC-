using System.Text;
using DatabasePN;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PhoneNoteAuthJWT.Data;

var builder = WebApplication.CreateBuilder(args);

var appConnectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<DatabaseContext>(options => options.UseNpgsql(appConnectionString));


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
	   .AddJwtBearer
	   (
		   JwtBearerDefaults.AuthenticationScheme, options =>
		   {
			   options.TokenValidationParameters = new TokenValidationParameters
			   {
				   ValidateIssuer = false,
				   ValidateAudience = false,
				   ValidateLifetime = true,
				   ValidateIssuerSigningKey = true,
				   IssuerSigningKey = new SymmetricSecurityKey
					   (Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
			   };
		   }
	   );

builder.Services.AddAuthorization();
builder.Services.AddControllers();

WebApplication app = builder.Build();

using (var scope = app.Services.CreateScope())
{
	IServiceProvider serviceProvider = scope.ServiceProvider;
	DatabaseContext context = serviceProvider.GetRequiredService<DatabaseContext>();

	await DatabaseInitializer.Rebuild(context);
}

if (app.Environment.IsDevelopment())
{
	app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
