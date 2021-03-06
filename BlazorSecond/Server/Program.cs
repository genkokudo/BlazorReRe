using BlazorSecond.Server.Data;
using BlazorSecond.Server.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using BlazorSecond.Server.Middlewares;
using System.Globalization;
using System.Reflection;
using MediatR;
using FluentValidation.AspNetCore;
using BlazorSecond.Server.Validators;
using BlazorSecond.Shared.Localization;
using BlazorSecond.Server.Services;
using BlazorSecond.Server.Services.Interfaces;

var builder = WebApplication
    .CreateBuilder(args)
    .ConfigureServices()        // 拡張してサービスを追加する
    ;

var app = builder
    .Build()
    .Configure()        // 拡張してアプリケーションの設定をする
    ;

app.Run();

internal static class WebApplicationBuilderExtensions
{
    public static WebApplicationBuilder ConfigureServices(this WebApplicationBuilder builder)
    {
        // コンテナにサービスを追加する。
        builder.Services.AddLocalization(options => // ローカライズファイルのパスを指定する、IStringLocalizerがDIできるようにする
        {
            options.ResourcesPath = "Resources";
        });
        builder.Services.AddCurrentUserService();                       // 現在のユーザのClainやIDを取得するアクセサをサービス登録する
        builder.Services.AddDatabase(builder.Configuration);
        builder.Services.AddDatabaseDeveloperPageExceptionFilter();
        builder.Services.AddApplicationLayer();                         // AutoMapperとMediatRを使用する

        // 認証
        builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
            .AddEntityFrameworkStores<ApplicationDbContext>();
        builder.Services.AddIdentityServer()
            .AddApiAuthorization<ApplicationUser, ApplicationDbContext>();
        builder.Services.AddAuthentication()
            .AddIdentityServerJwt();                // JsonWebTokenだっけ？

        // コントローラとRazor
        builder.Services.AddControllersWithViews()
            .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<DocumentTypeValidator>())   // FluentValidationを使用する、型引数はValidatorがある場所のクラスなら何でもよい
            ;

        builder.Services.AddRazorPages();


        return builder;
    }
    /// <summary>
    /// AutoMapperとMediatRを使用する
    /// </summary>
    /// <param name="services"></param>
    public static void AddApplicationLayer(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        //services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddMediatR(Assembly.GetExecutingAssembly());
        //services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
    }

    /// <summary>
    /// データベースをサービス登録する
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <returns></returns>
    internal static IServiceCollection AddDatabase(
        this IServiceCollection services,
        IConfiguration configuration)
        => services
            .AddDbContext<ApplicationDbContext>(options => options
                .UseSqlServer(configuration.GetConnectionString("DefaultConnection")))
        .AddTransient<IDatabaseSeeder, DatabaseSeeder>()
        ;

}

static class WebApplicationExtensions
{
    public static WebApplication Configure(this WebApplication app)
    {
        // HTTPリクエストパイプラインを設定します。
        if (app.Environment.IsDevelopment())
        {
            app.UseMigrationsEndPoint();
            app.UseWebAssemblyDebugging();
        }
        else
        {
            app.UseExceptionHandler("/Error");
            // HSTSのデフォルト値は30日です。本番環境では、この値を変更することができます。 https://aka.ms/aspnetcore-hsts
            app.UseHsts();
        }

        app.UseHttpsRedirection();

        app.UseBlazorFrameworkFiles();
        app.UseStaticFiles();                           // wwwroot以下を静的ファイルとするはずだけど、Serverにないなあ…

        app.UseRequestLocalizationByCulture();          // IStringLocalizer<T>の仕様で必要
        app.UseRouting();                               // ルーティング

        app.UseIdentityServer();
        app.UseAuthentication();                        // ユーザーがセキュリティで保護されたリソースにアクセスする前に、ユーザーの認証が試行されます。
        app.UseAuthorization();                         // ユーザーがセキュリティで保護されたリソースにアクセスすることが承認されます。

        app.UseEndpoints();
        app.Initialize();                               // IDatabaseSeederで初期化（DB初期データがまだない場合にデータを入れる）
        return app;
    }

    internal static IApplicationBuilder UseEndpoints(this IApplicationBuilder app)
        => app.UseEndpoints(endpoints =>
        {
            endpoints.MapRazorPages();
            endpoints.MapControllers();                 // 属性ルーティングを有効にする
                endpoints.MapFallbackToFile("index.html");
                //endpoints.MapHub<SignalRHub>(ApplicationConstants.SignalR.HubUrl);
            });

    /// <summary>
    /// 現在のユーザのClainやIDを取得するアクセサを登録する
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    internal static IServiceCollection AddCurrentUserService(this IServiceCollection services)
    {
        services.AddHttpContextAccessor();  // IHttpContextAccessorについてデフォルトの実装を追加
        services.AddScoped<ICurrentUserService, CurrentUserService>();
        return services;
    }

    /// <summary>
    /// IDatabaseSeederで初期化する
    /// </summary>
    /// <param name="app"></param>
    /// <param name="_configuration"></param>
    /// <returns></returns>
    internal static IApplicationBuilder Initialize(this IApplicationBuilder app)
    {
        using var serviceScope = app.ApplicationServices.CreateScope();

        var initializers = serviceScope.ServiceProvider.GetServices<IDatabaseSeeder>();

        foreach (var initializer in initializers)
        {
            initializer.Initialize();
        }

        return app;
    }

    //要らないかもしれない
    //internal static IServiceCollection AddServerLocalization(this IServiceCollection services)
    //{
    //    services.TryAddTransient(typeof(IStringLocalizer<>), typeof(ServerLocalizer<>));
    //    return services;
    //}
    /// <summary>
    /// IStringLocalizerの使用で必要
    /// </summary>
    /// <param name="app"></param>
    /// <returns></returns>
    internal static IApplicationBuilder UseRequestLocalizationByCulture(this IApplicationBuilder app)
    {
        var supportedCultures = LocalizationConstants.SupportedLanguages.Select(l => new CultureInfo(l.Code!)).ToArray();
        app.UseRequestLocalization(options =>
        {
            options.SupportedUICultures = supportedCultures;
            options.SupportedCultures = supportedCultures;
            options.DefaultRequestCulture = new RequestCulture(supportedCultures.First());
            options.ApplyCurrentCultureToResponseHeaders = true;    // 応答の Content-Language ヘッダーにCurrentUICulture が適用されるかどうか
        });

        // リクエストに特定のカルチャ情報がある場合、CultureInfoの設定値を変更する
        app.UseMiddleware<RequestCultureMiddleware>();

        return app;
    }
}