using Courserio.Api;
using Courserio.Core.Extensions.Hangfire;
using Courserio.Core.Middlewares.ExceptionMiddleware;
using Courserio.Infrastructure.Seeders;
using Hangfire;
using NLog.Web;

var builder = WebApplication.CreateBuilder(args);

var configurationBuilder = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .AddUserSecrets<Program>();

builder.WebHost
    .ConfigureLogging(logging =>
    {
        logging.ClearProviders();
    })
    .UseNLog();


if (!builder.Environment.IsDevelopment())
{
    builder.WebHost.UseKestrel(configure => { configure.Listen(System.Net.IPAddress.Any, 5000); });
    configurationBuilder.AddEnvironmentVariables();
}


IConfiguration configuration = configurationBuilder.Build();


// Add services to the container.

builder.Services.AddCorsPolicies();
builder.Services.AddMapper();
builder.Services.AddDatabaseContext(configuration);
builder.Services.AddHangfire(configuration);

//// KEYCLOAK
builder.Services.AddKeycloak(configuration);

builder.Services.AddSwagger();
builder.Services.AddServices();
builder.Services.AddTransient<InitialSeeder>();
builder.Services.AddMapper();
builder.Services.AddControllers();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseStaticFiles();
    app.UseDeveloperExceptionPage();
    app.UseSwagger(c => { });
    app.UseSwaggerUI(
        c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "Courserio.Api v1");
            c.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None);
            c.InjectStylesheet("/Content/SwaggerDark.css");
            c.OAuthClientId("courserio");
            c.OAuthAppName("Courserio");
            c.OAuthScopeSeparator(" ");
        });
    app.UseHangfireDashboard("/hangfire", new DashboardOptions { Authorization = new[] { new HangfireAuth() } });
}

app.UseMiddleware<ExceptionMiddleware>();

app.UseHttpsRedirection();

app.UseRouting();

app.UseCors("allowAll");

app.UseAuthentication();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    if (app.Environment.IsDevelopment())
    {
        endpoints.MapControllers();
    }
    else
    {
        endpoints.MapControllers();
    }
});

HangfireSetup.AddJobs();

app.Run();
