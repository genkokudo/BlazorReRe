using BlazorReRe.Server.Extensions;

// �g�����Ă������ŏ�������������Ă��邯�ǁA�債���ʂ���Ȃ������ǍD�݂���˂��Ďv���܂���
// �������ĕ�����ɂ���

var builder = WebApplication
    .CreateBuilder(args)
    .ConfigureServices()        // �g�����ăT�[�r�X��ǉ�����
    ;

var app = builder
    .Build()
    .Configure()        // �g�����ăA�v���P�[�V�����̐ݒ������
    ;

app.Run();
