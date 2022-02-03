using BlazorSecond.Client;
using BlazorSecond.Client.Infrastructure;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder
    .CreateDefault(args)
    .AddRootComponents()          // �g�����\�b�h�F"#app"��"<head>"��ǉ����鏈��;
    .AddClientServices()
    ;
await builder.Build().RunAsync();

/// <summary>
/// WebAssemblyHostBuilder���g�����邱�Ƃ�
/// ���܂�StartUp�ōs�Ȃ��Ă����K�v�ȏ����ݒ�������ōs��
/// </summary>
public static class WebAssemblyHostBuilderExtensions
{
    private const string PrivateClientName = "private";
    private const string PublicClientName = "public";

    public static WebAssemblyHostBuilder AddRootComponents(this WebAssemblyHostBuilder builder)
    {
        builder.RootComponents.Add<App>("#app");
        builder.RootComponents.Add<HeadOutlet>("head::after");      // _Layout.cshtml��<head>�R���e���c�������_�����O����炵��

        return builder;
    }

    /// <summary>
    /// Main����Ăяo��
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    public static WebAssemblyHostBuilder AddClientServices(this WebAssemblyHostBuilder builder)
    {
        // HTTP�N���C�A���g�𕡐��g�p����̂ŁA@inject HttpClient�͎g�p�֎~�B@inject IHttpClientFactory���g�p���邱�ƁB
        // �T�C���C�����s�v��API�p��HTTP�N���C�A���g
        // @inject PublicClient Http �Ŏg�p����
        builder.Services
            .AddScoped(sp => sp
                .GetRequiredService<IHttpClientFactory>()
                .CreateClient(PublicClientName))
            .AddHttpClient<PublicClient>(PublicClientName, client =>
            {
                client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress);
            });

        // �T�C���C�����K�v��API�p��HTTP�N���C�A���g
        // @inject HttpClient Http �Ŏg�p����i�ォ��AddHttpClient�������̃C���X�^���X���Ă΂��j
        builder.Services
            // �T�[�o�[�v���W�F�N�g�ւ̃��N�G�X�g���ɃA�N�Z�X�g�[�N�����܂�HttpClient�C���X�^���X��񋟂���B
            .AddScoped(sp => sp
                .GetRequiredService<IHttpClientFactory>()
                .CreateClient(PrivateClientName))                  // ���炭�N���C�A���g�̃C���X�^���X�𗭂߂Ă����A���������Ɏg���܂킵����K�؂ȃ^�C�~���O�Ŕj��������F�X����Ă����
            .AddHttpClient<PrivateClient>(PrivateClientName, client =>
            {
                client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress);
            })
            // AuthorizationMessageHandler���p�����Ă��邪�A���ꂪ���M���N�G�X�g�̔F�؃w�b�_�[�Ƀg�[�N�����Ȃ��ꍇ�͗�O�𓊂��Ă���B
            // ���ꂪ����Ƃ��ɃT�C���C��������API��@���ƃG���[�ɂȂ�̂Œ��ӁB
            .AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>()
        ;

        builder.Services.AddApiAuthorization();     // SPA �A�v���P�[�V�����̔F�؂��T�|�[�g�B�ڂ����͂킩���iBlazorHero�ɂ͖����j

        return builder;
    }
}