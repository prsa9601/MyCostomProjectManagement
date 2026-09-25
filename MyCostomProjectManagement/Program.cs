using BackEnd.Core;
using BackEnd.Core.Abstraction.Jwt.Interfaces;
using BackEnd.Data.DB;
using BackEnd.Data.DB.Initializer;
using BackEnd.Data.Entities.Role;
using BackEnd.Data.Infrastructure.Tutorial;
using BackEnd.Infrastructure.Auth.Jwt;
using BackEnd.Infrastructure.Auth.Middlewares;
using BackEnd.Infrastructure.Security.Hash.service;
using BackEnd.Infrastructure.Security.Hash.strategies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.Circuits;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.IdentityModel.Tokens;
using MyCostomProjectManagement.Components;
using MyCostomProjectManagement.Facade;
using MyCostomProjectManagement.Facade.Role;
using MyCostomProjectManagement.Facade.User;
using MyCostomProjectManagement.Infrastructure;
using MyCostomProjectManagement.Shared.ExceptionHandler;
using MyCostomProjectManagement.Shared.Extensions;
using MyCostomProjectManagement.Shared.Middleware;
using MyCostomProjectManagement.Shared.Utilities.PageManagement;
using System.Collections.Immutable;
using System.Security.Claims;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();


builder.Services.AddRazorPages();   // <-- این خط را اضافه کنید
//builder.Services.AddControllers();
builder.Services.AddServerSideBlazor();
builder.Services.AddAuthentication(option =>
{
    option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    option.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
    option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(option =>
{
    option.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidAudience = builder.Configuration["JwtConfig:Audience"],
        ValidIssuer = builder.Configuration["JwtConfig:Issuer"],
        IssuerSigningKey =
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtConfig:SecretKey"]!)),
        ValidateLifetime = true,
        ValidateAudience = true,
        ValidateIssuer = true,
        ValidateIssuerSigningKey = true
    };
}).AddCookie(options =>
{
    options.Cookie.Name = "auth-Token";
    options.Cookie.HttpOnly = true;
    options.Cookie.SecurePolicy = Microsoft.AspNetCore.Http.CookieSecurePolicy.Always;
    options.Cookie.SameSite = Microsoft.AspNetCore.Http.SameSiteMode.Strict;
    options.Cookie.MaxAge = TimeSpan.FromMinutes(30);

    //options.Events = new CookieAuthenticationEvents
    //{
    //    // ۶. هنگامی که کاربر لاگین نکرده
    //    OnRedirectToLogin = async context =>
    //    {
    //        // مدیریت redirect به صفحه لاگین
    //        if (context.Request.Path.StartsWithSegments("/api"))
    //        {
    //            context.Response.StatusCode = 401;
    //            await context.Response.WriteAsync("Unauthorized");
    //            context.Response.Redirect($"/Auth/VerificationPhoneNumber?action=Login");

    //        }
    //        else
    //        {
    //            context.Response.Redirect("/auth");
    //        }
    //    },
    //    //
    //    //// ۷. هنگامی که کاربر از لاگ اوت بازدید می‌کند
    //    //OnRedirectToLogout = async context =>
    //    //{
    //    //    // مدیریت redirect به صفحه خروج
    //    //    context.Response.Redirect("/Logout");
    //    //},
    //    //
    //    //// ۸. هنگامی که کوکی منقضی شده
    //    //OnRedirectToReturnUrl = async context =>
    //    //{
    //    //    // مدیریت بازگشت به URL اصلی
    //    //    context.Response.Redirect(context.RedirectUri);
    //    //}

    //};


    //option.Events = new JwtBearerEvents
    //{
    //    OnChallenge = async context =>
    //    {
    //        // جلوگیری از پاسخ پیش‌فرض
    //        context.HandleResponse();

    //        //if (context.Request.Path.StartsWithSegments("/api"))
    //        //{
    //        //    context.Response.StatusCode = 401;
    //        //    await context.Response.WriteAsync("Unauthorized");
    //        //}
    //        if ((int)context.Response.StatusCode == 401)
    //        {
    //            context.Response.Redirect($"/Auth/VerificationPhoneNumber?action={ForAuthAction.Login}");
    //        }
    //    }
    //};
});

// در Program.cs
builder.Services.Configure<FormOptions>(options =>
{
    options.ValueLengthLimit = int.MaxValue;
    options.MultipartBodyLengthLimit = int.MaxValue;
    options.MemoryBufferThreshold = int.MaxValue;
});

builder.WebHost.ConfigureKestrel(options =>
{
    options.Limits.MaxRequestBodySize = 200 * 1024 * 1024; // 200 MB
});

// همچنین اگر از AddServerSideBlazor جداگانه استفاده می‌کنید، آن را نیز تنظیم کنید
builder.Services.AddServerSideBlazor()
    .AddHubOptions(options =>
    {
        options.MaximumReceiveMessageSize = 200 * 1024 * 1024;
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("DashboardPolicy", policy =>
        policy.RequireAuthenticatedUser());
});

builder.Services.AddScoped<ITutorialApiService, TutorialApiService>();


builder.Services.AddScoped<UserAuthentication>();
builder.Services.AddScoped<PageManagementUtil>();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
builder.Services.AddScoped<CircuitHandler, CustomCircuitHandler>();

builder.Services.AddHttpContextAccessor();

builder.Services.FacadeConfig();
builder.Services.AddScoped<AlertService>();
builder.Services.AddScoped<UserAlertService>();
builder.Services.AddScoped<JsAlertService>();
builder.Services.AddScoped<GetUserPermission>();
builder.Services.AddScoped<FileExtensions>();
builder.Services.CoreConfig(builder.Configuration);

builder.Services.AddHttpClient();
var app = builder.Build();
// اجرای خودکار Migration ها در زمان بالا آمدن برنامه
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<Context>();
    dbContext.Database.Migrate();  // همه Migrationهای اعمال‌نشده را اجرا می‌کند
}


app.UseMiddleware<CustomExceptionHandler>();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);

app.UseHttpsRedirection();

app.UseRouting();
//app.MapControllers();
app.UseAntiforgery();


app.Use(async (context, next) =>
{
    var service = context.RequestServices.GetRequiredService<PageManagementUtil>();
    var pagesUnderConstruction = await service.GetPagesItem();
    var result = pagesUnderConstruction.Where(i => i.IsUnderConstruction == true).ToList();
    if (result.Any(i => i.Url.Equals(context.Request.Path.Value, StringComparison.OrdinalIgnoreCase)))
    {
        context.Response.Redirect("/PageUpgrading");
        return;
    }
    await next();
});


app.Use(async (context, next) =>
{
    var refreshToken = context.Request.Cookies["refresh-Token"];
    if (string.IsNullOrEmpty(refreshToken))
    {
        await next();
        return;
    }

    var jwtFactory = context.RequestServices.GetRequiredService<IJwtSettingsFactory>();
    var dbContextFactory = context.RequestServices.GetRequiredService<IDbContextFactory<Context>>();
    var hashManager = new HashManager(new Sha256Hasher()); // بهتر است از DI استفاده شود

    var service = jwtFactory.CreateSetting(BackEnd.Core.Abstraction.Jwt.Enum.TokenType.AuthRefreshToken);
    var principal = service.ValidateToken(refreshToken);

    // اگر توکن معتبر نبود، کوکی را حذف کن
    if (principal == null)
    {
        context.Response.Cookies.Delete("refresh-Token");
        context.Response.Cookies.Delete("auth-Token");
        await next();
        return;
    }

    var userIdClaim = principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
    if (!Guid.TryParse(userIdClaim, out var userId))
    {
        context.Response.Cookies.Delete("refresh-Token");
        context.Response.Cookies.Delete("auth-Token");
        await next();
        return;
    }

    await using var dbContext = await dbContextFactory.CreateDbContextAsync();
    var user = await dbContext.Users
        .AsNoTracking()
        .Include(u => u.UserSessionBlackList)
        .FirstOrDefaultAsync(u => u.Id == userId);

    // اگر کاربر وجود نداشت، کوکی را حذف کن
    if (user == null)
    {
        context.Response.Cookies.Delete("refresh-Token");
        context.Response.Cookies.Delete("auth-Token");
        await next();
        return;
    }

    var hashToken = hashManager.Hash(refreshToken);
    if (user.UserSessionBlackList.Any(s => s.HashToken == hashToken))
    {
        context.Response.Cookies.Delete("refresh-Token");
        context.Response.Cookies.Delete("auth-Token");
    }

    await next();
});
// ✅ این خط رو حتماً اضافه کن (قبل از هر UseAuthentication)
app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
});
app.UseMiddleware<AuthRefreshTokenMiddleware>();
app.UseAuthentication();
app.UseAuthorization();
app.UseWhen(context => context.Request.Path.StartsWithSegments("/dashboard"), appBuilder =>
{
    appBuilder.Use(async (context, next) =>
    {
        // دریافت سرویس مجوزدهی
        var authService = context.RequestServices.GetRequiredService<IAuthorizationService>();
        var jwtFactory = context.RequestServices.GetRequiredService<IJwtSettingsFactory>();
        var dbContextFactory = context.RequestServices.GetRequiredService<IDbContextFactory<Context>>();
        var _context = await dbContextFactory.CreateDbContextAsync();
        var authTokenService = jwtFactory.CreateSetting(BackEnd.Core.Abstraction.Jwt.Enum.TokenType.AuthToken);
        var refreshTokenService = jwtFactory.CreateSetting(BackEnd.Core.Abstraction.Jwt.Enum.TokenType.AuthRefreshToken);


        var userToken = context.Request.Cookies["auth-Token"];
        if (userToken == null)
        {   // اگر مجاز نبود، خطای 403 برگردان
            var userRefreshToken = context.Request.Cookies["refresh-Token"];
            if (userRefreshToken != null)
            {
                var userRefresh = refreshTokenService.ValidateToken(userRefreshToken);
                Guid.TryParse(userRefresh.FindFirst(ClaimTypes.NameIdentifier)?.Value, out var userRefreshId);

                await next();
                return;
            }
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;

            // برای متن ساده
            context.Response.ContentType = "text/plain; charset=utf-8";

            //// یا برای اچ‌تی‌ام‌ال
            //context.Response.ContentType = "text/html; charset=utf-8";

            // (اختیاری) پیام خطا یا هدایت به صفحه‌ی خاص
            //await context.Response.WriteAsync("لطفا اول وارد شوید.");

            // از ادامه‌ی پردازش جلوگیری کن
            //return;
            // انتقال پیام از طریق پارامتر آدرس (Query String)
            //context.Response.Redirect("auth?error=unauthorized");
            context.Response.Redirect("auth");
            return;
        }
        var user = authTokenService.ValidateToken(userToken);
        Guid.TryParse(user.FindFirst(ClaimTypes.NameIdentifier)?.Value, out var userId);
        var userRoles = user.FindAll(ClaimTypes.Role)?.Select(r => r.Value).ToList();
        bool userIsExist = _context.Users.Any(i => i.Id == userId);
        // کاربر جاری (که توسط کوکی JWT شما احراز هویت شده)
        //var user = context.User;

        // بررسی Policy
        var authResult = await authService.AuthorizeAsync(user, "DashboardPolicy");

        if (userIsExist)
        {
            // اگر مجاز بود، درخواست را ادامه بده
            await next();
        }
        else
        {
            // اگر مجاز نبود، خطای 403 برگردان
            context.Response.StatusCode = StatusCodes.Status403Forbidden;

            // (اختیاری) پیام خطا یا هدایت به صفحه‌ی خاص
            await context.Response.WriteAsync("شما دسترسی به این بخش را ندارید.");

            // از ادامه‌ی پردازش جلوگیری کن
            return;
        }
    });

    // بعد از Middleware، فایل‌های استاتیک داخل wwwroot/dashboard سرو می‌شوند
    //appBuilder.UseStaticFiles();
});

app.UseWhen(context => context.Request.Path.StartsWithSegments("/Admin"), appBuilder =>
{
    appBuilder.Use(async (context, next) =>
    {
        // دریافت سرویس مجوزدهی
        var authService = context.RequestServices.GetRequiredService<IAuthorizationService>();
        var _roleFacade = context.RequestServices.GetRequiredService<IRoleFacade>();
        var _userFacade = context.RequestServices.GetRequiredService<IUserFacade>();
        var jwtFactory = context.RequestServices.GetRequiredService<IJwtSettingsFactory>();
        var dbContextFactory = context.RequestServices.GetRequiredService<IDbContextFactory<Context>>();
        var _context = await dbContextFactory.CreateDbContextAsync();
        var authTokenService = jwtFactory.CreateSetting(BackEnd.Core.Abstraction.Jwt.Enum.TokenType.AuthToken);

        // کاربر جاری (که توسط کوکی JWT شما احراز هویت شده)
        var userToken = context.Request.Cookies["auth-Token"];
        var user = authTokenService.ValidateToken(userToken);
        Guid.TryParse(user.FindFirst(ClaimTypes.NameIdentifier)?.Value, out var userId);
        //var userRoles = user.FindAll(ClaimTypes.Role)?.Select(r => r.Value).ToList();
        var cutrrentUser = await _userFacade.GetId(userId);

        var userRoleIds = cutrrentUser.UserRoles.Select(i => i.RoleId);
        var userFromDb = await _context.Users.Include(i => i.UserRoles).FirstOrDefaultAsync(i => i.Id == userId);
        var roles = await _roleFacade.GetAll();


        var allRoleForUsres = roles.Where(i => userRoleIds.Contains(i.Id));


        //bool isAccess = allRoleForUsres.Any(i => i.role == Role.Admin || i.Role == Role.Programmer);
        bool isAccess = allRoleForUsres.Any(i => i.RolePermissions.Any(p => p.Permissions.Equals(Permissions.AccessToAdminPanel)));


        if (isAccess)
        {
            // اگر مجاز بود، درخواست را ادامه بده
            await next();
        }
        else
        {
            // اگر مجاز نبود، خطای 403 برگردان
            context.Response.StatusCode = StatusCodes.Status403Forbidden;
            context.Response.ContentType = "text/plain; charset=utf-8";

            // (اختیاری) پیام خطا یا هدایت به صفحه‌ی خاص
            await context.Response.WriteAsync("شما دسترسی به این بخش را ندارید.");

            // از ادامه‌ی پردازش جلوگیری کن
            return;
        }
    });

    // بعد از Middleware، فایل‌های استاتیک داخل wwwroot/dashboard سرو می‌شوند
    //appBuilder.UseStaticFiles();
});


app.UseStaticFiles();

//کامنت کردم این رو ویدیو کامل و درست پلی شد
//app.MapStaticAssets();

app.UseMiddleware<RobotsMiddleware>();

app.MapRazorPages();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();
app.MapControllers();

await DbInitializer.InitializeAsync(app.Services);

app.Run();
