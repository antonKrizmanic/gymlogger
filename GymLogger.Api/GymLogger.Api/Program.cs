using GymLogger.Api.Configuration;
using GymLogger.Application;
using GymLogger.Exceptions.Web;
using GymLogger.Infrastructure.Database;
using GymLogger.Infrastructure.Database.Models.Identity;
using GymLogger.Infrastructure.Http;
using Microsoft.AspNetCore.Identity;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services
    .AddCustomCors()
    .AddCustomAuthentication()
    .AddCustomAuoMapper()
    .AddInfrastructureDatabase(builder.Configuration, builder.Environment.IsProduction())
    .RegisterInfrastructureDbRepositories()
    .RegisterInfrastructureDbServices()
    .RegisterInfrastructureHttpServices()
    .AddApplication()
    .AddCustomSerilog(builder.Configuration)
    .AddCustomSwagger()
    .AddApiServices()
    .AddAntiforgery();

// TODO: Move to a separate extension method and IdentityNoOpEmailSender to a separate project
// builder.Services.AddSingleton<IEmailSender<DbApplicationUser>, IdentityNoOpEmailSender>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseSerilogRequestLogging(options =>
{
    // Attach additional properties to the request completion event
    options.EnrichDiagnosticContext = (diagnosticContext, httpContext) =>
    {
        diagnosticContext.Set("RequestHost", httpContext.Request.Host.Value);
        diagnosticContext.Set("RequestScheme", httpContext.Request.Scheme);
    };
});

app.UseGymLoggerHttpExceptionMiddleware();

app.UseHttpsRedirection();

app.MapStaticAssets();
app.UseRouting();
app.UseAntiforgery();
app.UseMinimalApi();


app.UseCors("CorsPolicy");

app.UseAuthorization();
app.MapGroup("/api").MapIdentityApi<DbApplicationUser>();

app.UseCustomSwagger();

app.Run();
