using DatabasePN;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PhoneNoteAuthJWT.Data;
using PhoneNoteAuthJWT.Middlewares;

var builder = WebApplication.CreateBuilder(args);

var appConnectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<DatabaseContext>(options => options.UseNpgsql(appConnectionString));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
	   .AddJwtBearer
	   (
		   options =>
		   {
			   options.TokenValidationParameters = new TokenValidationParameters
			   {
				   ValidIssuer = AuthOptions.ISSUER,
				   ValidAudience = AuthOptions.AUDIENCE,
				   ValidateIssuer = false,
				   ValidateAudience = false,
				   ValidateLifetime = false,
				   ValidateIssuerSigningKey = false,
				   IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey()
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

app.UseMiddleware<TestMiddleware>();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
