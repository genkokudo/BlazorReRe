using BlazorReRe.Server.Extensions;

// 拡張してそっちで初期化処理やっているけど、大した量じゃないし結局好みだよねって思いました
// かえって分かりにくい

var builder = WebApplication
    .CreateBuilder(args)
    .ConfigureServices()        // 拡張してサービスを追加する
    ;

var app = builder
    .Build()
    .Configure()        // 拡張してアプリケーションの設定をする
    ;

app.Run();
