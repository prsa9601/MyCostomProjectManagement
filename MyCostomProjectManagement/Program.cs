using BackEnd.Core;
using BackEnd.Data.DB.Initializer;
using BackEnd.Infrastructure.Auth.Middlewares;
using Microsoft.AspNetCore.Components.Authorization;
using MyCostomProjectManagement.Components;
using MyCostomProjectManagement.Facade;
using MyCostomProjectManagement.Infrastructure;
using MyCostomProjectManagement.Shared.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services.AddRazorPages();   // <-- این خط را اضافه کنید
builder.Services.AddServerSideBlazor();

builder.Services.AddScoped<UserAuthentication>();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();

builder.Services.AddHttpContextAccessor();

builder.Services.FacadeConfig();
builder.Services.AddScoped<AlertService>();
builder.Services.AddScoped<JsAlertService>();
builder.Services.AddScoped<FileExtensions>();
builder.Services.CoreConfig(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);

app.UseHttpsRedirection();

//app.UseRouting();
//app.MapControllers();
app.UseAntiforgery();

app.UseMiddleware<AuthRefreshTokenMiddleware>();
app.UseAuthentication();
app.UseAuthorization();

app.UseStaticFiles();

//کامنت کردم این رو ویدیو کامل و درست پلی شد
//app.MapStaticAssets();

app.MapRazorPages();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

await DbInitializer.InitializeAsync(app.Services);

app.Run();
