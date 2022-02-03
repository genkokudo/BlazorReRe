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
    .ConfigureServices()        // �g�����ăT�[�r�X��ǉ�����
    ;

var app = builder
    .Build()
    .Configure()        // �g�����ăA�v���P�[�V�����̐ݒ������
    ;

app.Run();

internal static class WebApplicationBuilderExtensions
{
    public static WebApplicationBuilder ConfigureServices(this WebApplicationBuilder builder)
    {
        // �R���e�i�ɃT�[�r�X��ǉ�����B
        builder.Services.AddLocalization(options => // ���[�J���C�Y�t�@�C���̃p�X���w�肷��AIStringLocalizer��DI�ł���悤�ɂ���
        {
            options.ResourcesPath = "Resources";
        });
        builder.Services.AddCurrentUserService();                       // ���݂̃��[�U��Clain��ID���擾����A�N�Z�T���T�[�r�X�o�^����
        builder.Services.AddDatabase(builder.Configuration);
        builder.Services.AddDatabaseDeveloperPageExceptionFilter();
        builder.Services.AddApplicationLayer();                         // AutoMapper��MediatR���g�p����

        // �F��
        builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
            .AddEntityFrameworkStores<ApplicationDbContext>();
        builder.Services.AddIdentityServer()
            .AddApiAuthorization<ApplicationUser, ApplicationDbContext>();
        builder.Services.AddAuthentication()
            .AddIdentityServerJwt();                // JsonWebToken�������H

        // �R���g���[����Razor
        builder.Services.AddControllersWithViews()
            .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<DocumentTypeValidator>())   // FluentValidation���g�p����A�^������Validator������ꏊ�̃N���X�Ȃ牽�ł��悢
            ;

        builder.Services.AddRazorPages();


        return builder;
    }
    /// <summary>
    /// AutoMapper��MediatR���g�p����
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
    /// �f�[�^�x�[�X���T�[�r�X�o�^����
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
        // HTTP���N�G�X�g�p�C�v���C����ݒ肵�܂��B
        if (app.Environment.IsDevelopment())
        {
            app.UseMigrationsEndPoint();
            app.UseWebAssemblyDebugging();
        }
        else
        {
            app.UseExceptionHandler("/Error");
            // HSTS�̃f�t�H���g�l��30���ł��B�{�Ԋ��ł́A���̒l��ύX���邱�Ƃ��ł��܂��B https://aka.ms/aspnetcore-hsts
            app.UseHsts();
        }

        app.UseHttpsRedirection();

        app.UseBlazorFrameworkFiles();
        app.UseStaticFiles();                           // wwwroot�ȉ���ÓI�t�@�C���Ƃ���͂������ǁAServer�ɂȂ��Ȃ��c

        app.UseRequestLocalizationByCulture();          // IStringLocalizer<T>�̎d�l�ŕK�v
        app.UseRouting();                               // ���[�e�B���O

        app.UseIdentityServer();
        app.UseAuthentication();                        // ���[�U�[���Z�L�����e�B�ŕی삳�ꂽ���\�[�X�ɃA�N�Z�X����O�ɁA���[�U�[�̔F�؂����s����܂��B
        app.UseAuthorization();                         // ���[�U�[���Z�L�����e�B�ŕی삳�ꂽ���\�[�X�ɃA�N�Z�X���邱�Ƃ����F����܂��B

        app.UseEndpoints();
        app.Initialize();                               // IDatabaseSeeder�ŏ������iDB�����f�[�^���܂��Ȃ��ꍇ�Ƀf�[�^������j
        return app;
    }

    internal static IApplicationBuilder UseEndpoints(this IApplicationBuilder app)
        => app.UseEndpoints(endpoints =>
        {
            endpoints.MapRazorPages();
            endpoints.MapControllers();                 // �������[�e�B���O��L���ɂ���
                endpoints.MapFallbackToFile("index.html");
                //endpoints.MapHub<SignalRHub>(ApplicationConstants.SignalR.HubUrl);
            });

    /// <summary>
    /// ���݂̃��[�U��Clain��ID���擾����A�N�Z�T��o�^����
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    internal static IServiceCollection AddCurrentUserService(this IServiceCollection services)
    {
        services.AddHttpContextAccessor();  // IHttpContextAccessor�ɂ��ăf�t�H���g�̎�����ǉ�
        services.AddScoped<ICurrentUserService, CurrentUserService>();
        return services;
    }

    /// <summary>
    /// IDatabaseSeeder�ŏ���������
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

    //�v��Ȃ���������Ȃ�
    //internal static IServiceCollection AddServerLocalization(this IServiceCollection services)
    //{
    //    services.TryAddTransient(typeof(IStringLocalizer<>), typeof(ServerLocalizer<>));
    //    return services;
    //}
    /// <summary>
    /// IStringLocalizer�̎g�p�ŕK�v
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
            options.ApplyCurrentCultureToResponseHeaders = true;    // ������ Content-Language �w�b�_�[��CurrentUICulture ���K�p����邩�ǂ���
        });

        // ���N�G�X�g�ɓ���̃J���`����񂪂���ꍇ�ACultureInfo�̐ݒ�l��ύX����
        app.UseMiddleware<RequestCultureMiddleware>();

        return app;
    }
}