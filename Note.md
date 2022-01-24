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
BlazorHeroから移す。Application.Featuresから、Infrastructure.MediatRへ。  
BlazorHeroはフォルダ分けすぎなので、テーブルごとにフォルダを切る。  
検索はQueryとResponse、DB更新はCommandという名前を付ける。  

どうせ全てのAPI結果にSucceededフラグとMessagesはあった方がいいので、BlazorHeroみたいにResultでラップした方が便利。   
というわけでBlazorPractice.Shared.WrapperからResultを移してくる。

* AutoMapperを使用する  
しゅくだい。BlazorHeroから写してくること

* Localizerを実装する  
した方が良いけど、実装の流れが分かっていないのでコメントアウトしておいてCRUDが出来たらつける。

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
PM> Install-Package MediatR  
PM> Install-Package MediatR.Extensions.Microsoft.DependencyInjection  
PM> Install-Package AutoMapper.Extensions.Microsoft.DependencyInjection  

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
* Microsoft.AspNetCore.App のランタイム パックがありませんでした。  
このエラーはMicrosoft.AspNetCore.ApiAuthorization.IdentityServer があるものをプロジェクト参照すると出る。
InfrastructureはDB更新に使っているので、ここ以外にレスポンスの型を移動させるか、モデルクラスを作ってMapする必要がある。
WeatherForecastに倣う場合、SharedにModelを作るのが適切か。

→次はWeatherForecastに倣ってAPIを直しましょう。
→または、Modelは作らずにSharedにMediatoRの入れ物を移すだけでOK？
→↑は出来ればModel層を作っておきたい。。Pizzaはデータをサーバに送る時どうやってた？
→モデルとか作らずに全部SharedにEntityを書いてる。MediatoRが邪魔・・・









