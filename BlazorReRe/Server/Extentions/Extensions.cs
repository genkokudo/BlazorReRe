using Infrastructure.Contexts;
using Infrastructure.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BlazorReRe.Server.Extensions
{
    internal static class WebApplicationBuilderExtensions
    {
        public static WebApplicationBuilder ConfigureServices(this WebApplicationBuilder builder)
        {
            // コンテナにサービスを追加する。
            builder.Services.AddDatabase(builder.Configuration);
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            // 認証
            builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>();
            builder.Services.AddIdentityServer()
                .AddApiAuthorization<ApplicationUser, ApplicationDbContext>();
            builder.Services.AddAuthentication()
                .AddIdentityServerJwt();                // JsonWebTokenだっけ？

            // コントローラとRazor
            builder.Services.AddControllersWithViews();
            builder.Services.AddRazorPages();
            return builder;
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
            //.AddTransient<IDatabaseSeeder, DatabaseSeeder>()
            ;
    }

    internal static class WebApplicationExtensions
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

            app.UseRouting();                               // ルーティング

            app.UseIdentityServer();
            app.UseAuthentication();                        // ユーザーがセキュリティで保護されたリソースにアクセスする前に、ユーザーの認証が試行されます。
            app.UseAuthorization();                         // ユーザーがセキュリティで保護されたリソースにアクセスすることが承認されます。

            app.UseEndpoints();

            return app;
        }

        internal static IApplicationBuilder UseEndpoints(this IApplicationBuilder app)
            => app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
                endpoints.MapFallbackToFile("index.html");
                //endpoints.MapHub<SignalRHub>(ApplicationConstants.SignalR.HubUrl);
            });
    }
}