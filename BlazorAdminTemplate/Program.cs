using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.AspNetCore.Components.Authorization;
using BlazorAdminTemplate;
using BlazorAdminTemplate.Application;
using BlazorAdminTemplate.Infrastructure;
using BlazorAdminTemplate.Domain.Configuration;
using MudBlazor.Services;
using Microsoft.Extensions.Configuration;
using Blazored.LocalStorage;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Add configuration
var http = new HttpClient() { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) };
builder.Services.AddScoped(sp => http);

using var response = await http.GetAsync("appsettings.json");
using var stream = await response.Content.ReadAsStreamAsync();
builder.Configuration.AddJsonStream(stream);

// Load environment-specific configuration
var environment = builder.HostEnvironment.Environment;
try
{
    using var envResponse = await http.GetAsync($"appsettings.{environment}.json");
    if (envResponse.IsSuccessStatusCode)
    {
        using var envStream = await envResponse.Content.ReadAsStreamAsync();
        builder.Configuration.AddJsonStream(envStream);
    }
}
catch
{
    // Environment-specific config file not found, continue with base config
}

// Bind configuration to strongly-typed classes
var appConfig = new AppConfiguration();
builder.Configuration.Bind(appConfig);
builder.Services.AddSingleton(appConfig);
builder.Services.AddSingleton(appConfig.ApiConfiguration);

// Configure HttpClient with API base URL
builder.Services.AddScoped(sp =>
{
    var apiConfig = sp.GetRequiredService<ApiConfiguration>();
    return new HttpClient { BaseAddress = new Uri(apiConfig.BaseUrl) };
});

// Add Blazored LocalStorage
builder.Services.AddBlazoredLocalStorage();

// Add authorization services
builder.Services.AddAuthorizationCore(options => {
    options.AddPolicy("StaffOnly", policy =>
        policy.RequireAssertion(context =>
        {
            // First check: Must be authenticated
            if (!context.User.Identity?.IsAuthenticated ?? true)
            {
                Console.WriteLine("StaffOnly Policy: User not authenticated - DENIED");
                return false;
            }

            var roleClaim = context.User.FindFirst("http://schemas.microsoft.com/ws/2008/06/identity/claims/role")?.Value;
            var memberTypeClaim = context.User.FindFirst("MemberType")?.Value;

            Console.WriteLine($"StaffOnly Policy - Role: '{roleClaim}', MemberType: '{memberTypeClaim}'");

            // Deny access to regular members (unless they have staff-level MemberType)
            if (roleClaim == "Member" &&
                memberTypeClaim != "Staff" &&
                memberTypeClaim != "Admin" &&
                memberTypeClaim != "Manager")
            {
                Console.WriteLine("StaffOnly Policy: Regular member without staff privileges - DENIED");
                return false;
            }

            Console.WriteLine("StaffOnly Policy: Access granted");
            return true;
        }));

    options.AddPolicy("AdminOnly", policy =>
        policy.RequireAssertion(context =>
        {
            // Must be authenticated
            if (!context.User.Identity?.IsAuthenticated ?? true)
                return false;

            var memberTypeClaim = context.User.FindFirst("MemberType")?.Value;
            return memberTypeClaim == "Admin";
        }));
});

builder.Services.AddMudServices();

// Register clean architecture layers
builder.Services.AddApplication();
builder.Services.AddInfrastructure();

await builder.Build().RunAsync();
