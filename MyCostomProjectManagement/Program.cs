using BackEnd.Core;
using BackEnd.Infrastructure.Auth.Middlewares;
using Microsoft.AspNetCore.Components.Authorization;
using MyCostomProjectManagement.Components;
using MyCostomProjectManagement.Facade;
using MyCostomProjectManagement.Infrastructure;

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

app.UseHttpsRedirection();

//app.UseRouting();
//app.MapControllers();
app.UseAntiforgery();

app.UseMiddleware<AuthRefreshTokenMiddleware>();
app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
