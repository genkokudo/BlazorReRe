# やったこと

* Program.csの中をExtentionsに移動  
殆ど意味なかった。  
設定項目が増えてきたらメソッド分けたら見やすいかなって程度。

* Infrastructureプロジェクト作成  
クラスライブラリNET6で作成  
Contextsフォルダを作成してDbContextをここに移動  
Modelsも移動  
ServerのDataフォルダ削除  
（移動などしたときはプロジェクトを右クリックして名前空間の同期をする）

* Domainプロジェクト作成  
Infrastructureは.NET6で、Domainは.NET Standard2.1  
.NET Standardは.NET6の世界ではもう要らないものだが、.NETの他のすべての実装の間でコードを共有する時に使う。  
ここにEntityと、そのIEntityを作成する。  
DbContextに上記のEntityを追加  
※ApplicationUserは、StandardではないのでInfrastructureのModelsに置くこと。

* appsettings.jsonのDB名を変更
* Add-Migration Initial  
ビルドエラーが出るので、パッケージのインストールをして解消する。
* Update-Database
* ここで起動してみて、ユーザ登録ができることを確認する。

* DBの初期データ入力  
InfrastructureにInterfacesを追加。  
BlazorPracticeのApplicationプロジェクトのように分ける必要は無いと思うので、そこにあるものはInfrastructureに作る。
* DbContextに更新時に自動で監査項目を入れる機能を追加
BlazorHeroContextをコピーしてきて、足りないサービスもコピーしてくる。

* データ一覧の取得（サーバ側）  
Server.ControllersにAPIコントローラを作成する
BaseApiControllerを作成し、コントローラはMediatRを使用するように指定。（要らないかもー）

* MediatRを使用する  
Serverの初期処理にAddAutoMapperとAddMediatRを追加。  
（バージョンが合わないと警告が出るので、その場合は下げること）  
BlazorHeroから移す。Application.Featuresから、Server.MediatRへ。  
（InfrastructureにMediatRを置いたら初期処理でMediatR登録されなかった。アセンブリ指定方法が分からない）  
BlazorHeroはフォルダ分けすぎなので、テーブルごとにフォルダを切る。  
検索はQueryとResponse、DB更新はCommandという名前を付けるのが決まり。  

どうせ全てのAPI結果にSucceededフラグとMessagesはあった方がいいので、BlazorHeroみたいにResultでラップした方が便利。   
というわけでBlazorPractice.Shared.WrapperからResultを移してくる。

* AutoMapperを使用する  
ConfigureServicesにAddAutoMapperを書く  
ApplicationのMappingsをServerプロジェクトにコピー。
BlazorHeroと違ってResultもマップするので、`CreateMap(typeof(Result<>), typeof(Result<>)).ReverseMap();`が必要。

* Localizerを実装する  
した方が良いけど、実装の流れが分かっていないのでコメントアウトしておいてCRUDが出来たらつける。

* クライアントサーバやり取りデータモデルの作成
SharedにModelを作成する。
Modelは画面に合わせて作成し、ControllerでMediatR用クエリに変換する。

* データ一覧の取得（クライアント側）  
ClientのPagesに新しくページを作成。  
NavMenu.razorにリンクを追加する。  

# やること
* ユーザがデータを登録する(CRUD)  
https://www.c-sharpcorner.com/article/crud-operations-using-blazor-net-6-0-entity-framework-core/
* 各テーブルにユーザIDを持たせて、登録時に自動で入れる（インタフェースを追加するが、監査項目インタフェースとは分ける）
* グローバルクエリフィルタを実装して、ユーザが登録したデータだけ表示する  

ServerとInfrastructureに対して  
PM> Install-Package AutoMapper  
PM> Install-Package AutoMapper.Extensions.Microsoft.DependencyInjection  

Serverに対して  
PM> Install-Package MediatR  
PM> Install-Package MediatR.Extensions.Microsoft.DependencyInjection  

* FluentValidation

# やめたこと
* UnitOfWorkパターン  
小規模のシステムの場合、普通に要らない

* Repositoryパターン  
RepositoryAsyncクラスでCRUDのコードだけ共有出来るから便利だけど…。  
Includeできない。（できるけど難しい？）  
やったところでそんなにコーディング量変わらない。

* 検索結果のキャッシュ  
練習なのでしない。

# 問題
* 「Microsoft.AspNetCore.App のランタイム パックがありませんでした。」  
このエラーはClientが、Microsoft.AspNetCore.ApiAuthorization.IdentityServer があるプロジェクトを参照すると出る。
InfrastructureはDB更新に使っているので、ここ以外にレスポンスの型を移動させるか、モデルクラスを作ってMapする必要がある。

* フロントからCRUDまでの流れ
クライアントとサーバのやり取りに使うデータモデルは、Sharedに置くのが基本。  
また、クライアント入力とMediatRのQueryや、クライアントと表示とMediatRのResponseが対応しているとは限らないので、それをControllerで吸収すべき。  
SharedのModelは画面に合わせた形で作成すると綺麗にできるのでは？


* ローカライズ（エラーが出るから中止）  
* https://kuttsun.blogspot.com/2017/09/aspnet-core.html  
ServerのExtentions（初期処理）に以下を追加。  
UseRequestLocalizationByCulture  
AddServerLocalization  

SharedのConstantsのLocalizationをコピー

GetAllには無いけど、他の所で使ってるから導入しなきゃいけないなあ

* https://localhost:7061/api/DocumentTypes  










