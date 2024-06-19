using GymLogger.Api.Client;
using GymLogger.Api.Client.Extensions;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.FluentUI.AspNetCore.Components;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddAuthorizationCore();
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddSingleton<AuthenticationStateProvider, PersistentAuthenticationStateProvider>();

builder.Services
    .AddFluentUIComponents()
    .AddCustomHttpClientFactory(builder)
    .AddHttpServices();

await builder.Build().RunAsync();
