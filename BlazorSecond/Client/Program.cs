using BlazorSecond.Client;
using BlazorSecond.Client.Infrastructure;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder
    .CreateDefault(args)
    .AddRootComponents()          // 拡張メソッド："#app"と"<head>"を追加する処理;
    .AddClientServices()
    ;
await builder.Build().RunAsync();

/// <summary>
/// WebAssemblyHostBuilderを拡張することで
/// 今までStartUpで行なっていた必要な初期設定をここで行う
/// </summary>
public static class WebAssemblyHostBuilderExtensions
{
    private const string PrivateClientName = "private";
    private const string PublicClientName = "public";

    public static WebAssemblyHostBuilder AddRootComponents(this WebAssemblyHostBuilder builder)
    {
        builder.RootComponents.Add<App>("#app");
        builder.RootComponents.Add<HeadOutlet>("head::after");      // _Layout.cshtmlに<head>コンテンツをレンダリングするらしい

        return builder;
    }

    /// <summary>
    /// Mainから呼び出す
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    public static WebAssemblyHostBuilder AddClientServices(this WebAssemblyHostBuilder builder)
    {
        // HTTPクライアントを複数使用するので、@inject HttpClientは使用禁止。@inject IHttpClientFactoryを使用すること。
        // サインインが不要なAPI用のHTTPクライアント
        // @inject PublicClient Http で使用する
        builder.Services
            .AddScoped(sp => sp
                .GetRequiredService<IHttpClientFactory>()
                .CreateClient(PublicClientName))
            .AddHttpClient<PublicClient>(PublicClientName, client =>
            {
                client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress);
            });

        // サインインが必要なAPI用のHTTPクライアント
        // @inject HttpClient Http で使用する（後からAddHttpClientした方のインスタンスが呼ばれる）
        builder.Services
            // サーバープロジェクトへのリクエスト時にアクセストークンを含むHttpClientインスタンスを提供する。
            .AddScoped(sp => sp
                .GetRequiredService<IHttpClientFactory>()
                .CreateClient(PrivateClientName))                  // 恐らくクライアントのインスタンスを溜めておき、いい感じに使いまわしたり適切なタイミングで破棄したり色々やってくれる
            .AddHttpClient<PrivateClient>(PrivateClientName, client =>
            {
                client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress);
            })
            // AuthorizationMessageHandlerを継承しているが、これが発信リクエストの認証ヘッダーにトークンがない場合は例外を投げてくる。
            // これがあるときにサインインせずにAPIを叩くとエラーになるので注意。
            .AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>()
        ;

        builder.Services.AddApiAuthorization();     // SPA アプリケーションの認証をサポート。詳しくはわからん（BlazorHeroには無い）

        return builder;
    }
}