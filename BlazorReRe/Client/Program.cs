using BlazorReRe.Client;
using BlazorReRe.Client.Extentions;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder
    .CreateDefault(args)
    .AddRootComponents()          // 拡張メソッド："#app"と"<head>"を追加する処理;
    .AddClientServices()
    ;

await builder.Build().RunAsync();

// 上でサービスの追加が完了しているので、もし必要なら取り出して初期処理をする
// BlazorHeroではユーザのブラウザのローカルストレージから、このアプリケーションの表示設定を取得とかやってる。
// ※Blazored.LocalStorage のNugetインストールが必要
