using Forum.Server.Common.Interfaces;
using Forum.Server.Common.Services;
using Forum.Server.Data;
using MudBlazor.Services;


var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("appsettings.json");
builder.Configuration.AddJsonFile("appsettings.Development.json", optional: true);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddScoped<ITokenStorageService, TokenStorageService>();
builder.Services.AddSingleton<IAPIClient, APIClient>();
builder.Services.AddScoped<SearchService>();
builder.Services.AddMudServices();

// Register the HttpClient in the IOC
builder.Services.AddHttpClient<APIClient>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
