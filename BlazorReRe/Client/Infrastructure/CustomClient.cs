namespace BlazorReRe.Client.Infrastructure
{
    public abstract class CustomClient
    {
        public HttpClient Client { get; }

        public CustomClient(HttpClient httpClient)
        {
            Client = httpClient;
        }
    }

    /// <summary>
    /// サインイン不要API用のクライアント
    /// BaseAddressAuthorizationMessageHandlerを使用したクライアントだと、サインインが必須になるので
    /// サインイン不要のAPIを叩くとエラーになってしまう。
    /// そのため、サインイン不要のクライアントを別に用意する。
    /// </summary>
    public class PublicClient: CustomClient
    {
        public PublicClient(HttpClient httpClient) : base(httpClient)
        {
        }
    }

    /// <summary>
    /// サインインが必要なAPI用のクライアント
    /// Razor側で両者を明示的に呼び出せるようにするため、PublicClientと同じ内容でPrivateClientを作成する
    /// </summary>
    public class PrivateClient : CustomClient
    {
        public PrivateClient(HttpClient httpClient) : base(httpClient)
        {
        }
    }
}
