using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SchoolWep.Core;
using SchoolWep.Core.Middleware;
using SchoolWep.Infrustructure.Data;
using SchoolWep.Services;
using System.Globalization;
using Microsoft.AspNetCore.Identity;
using SchoolWep.Infrustructure.Seeder;
using SchoolWep.Infrustructure;

var builder = WebApplication.CreateBuilder(args);


#region DependenciesInjections
// Add DbContext Serveices
builder.Services.AddDbServices(builder.Configuration)
    // add AutoMapper Services
        .AddAuthServices(builder.Configuration)
    .AddCoreDependencies()
    .AddServicesInjections();
#endregion



#region Localization
builder.Services.AddLocalization(option =>
{
    option.ResourcesPath = "";
});


builder.Services.Configure<RequestLocalizationOptions>(options =>
{

    List<CultureInfo> SupportedCulture = new List<CultureInfo>
    {
        new CultureInfo("en-US"),
        new CultureInfo("ar-EG")
    };
    options.DefaultRequestCulture = new RequestCulture("en-US");
    options.SupportedCultures = SupportedCulture;
    options.SupportedUICultures = SupportedCulture;

});
#endregion



#region AllowCORS
var CORS = "_cors";
builder.Services.AddCors(option =>
{
    option.AddPolicy(CORS, policy => {
        policy.AllowAnyHeader();
        policy.WithMethods("GET", "POST", "PUT");
        policy.WithOrigins("http://127.0.0.1:5500", "https://localhost:4000");
        policy.AllowCredentials();
    });
});

#endregion

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


// create seeder
    using (var scope = app.Services.CreateScope())
    {
        var UserManger = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        var RoleManger = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
   
        await RoleSeeder.SeedAsync(RoleManger);
        await UserSeeder.SeedAsync(UserManger);
        await PermissionSeeder.SeedClaimsAsync(RoleManger);
    }



    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }


app.UseCors(CORS);

#region LocalizationMiddleware
var options = app.Services.GetService<IOptions<RequestLocalizationOptions>>();
if (options != null)
    app.UseRequestLocalization(options.Value);
#endregion


app.UseMiddleware<ErrorHandlerMiddleware>();

app.UseHttpsRedirection();


app.UseAuthorization();
app.UseMiddleware<ClaimsValidationMiddleware>();
app.MapControllers();

app.Run();
