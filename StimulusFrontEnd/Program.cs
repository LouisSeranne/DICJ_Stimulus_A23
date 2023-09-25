using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using StimulusFrontEnd.Models;
using StimulusFrontEnd.Services.Base;
using Blazored.SessionStorage;
using Blazored.LocalStorage;
using StimulusFrontEnd.Providers;

using System.Security.Cryptography.X509Certificates;
using System;

using Microsoft.AspNetCore.Authentication;
using StimulusFrontEnd;
using Microsoft.AspNetCore.Components.Authorization;
using Serilog;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddRazorPages().AddNewtonsoftJson();
builder.Services.AddServerSideBlazor();

//Initialise les local et session storage
builder.Services.AddBlazoredSessionStorage();
builder.Services.AddBlazoredLocalStorage();

//configure le client utiliser par les autres services avec la validation custom

builder.Services.AddHttpClient<IClient, Client>().ConfigureHttpClient((test) => test.BaseAddress = new Uri(builder.Configuration["API:Use"])).ConfigurePrimaryHttpMessageHandler(() =>
{
    return new HttpClientHandler()
    {
        ClientCertificateOptions = ClientCertificateOption.Manual,
        ServerCertificateCustomValidationCallback = (httpRequestMessage, cert, cetChain, policyErrors) =>
        {
            return true;
        }
    };
});


builder.Services.AddScoped<StimulusFrontEnd.Services.Authentification.IAuthenticationService, StimulusFrontEnd.Services.Authentification.AuthenticationService>();
builder.Services.AddScoped<ApiAuthenticationStateProvider>();
builder.Services.AddScoped<AuthenticationStateProvider>(p =>
            p.GetRequiredService<ApiAuthenticationStateProvider>());
//Initialise la sérialisation de Json

builder.Services.AddMvc().AddJsonOptions(options => options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles);


builder.Services.AddSingleton<IUpdateService,UpdateService>();

builder.Services.AddSingleton<ViewOptionService>();


builder.Host.UseSerilog((ctx, lc) =>
    lc.WriteTo.Console().ReadFrom.Configuration(ctx.Configuration));


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
