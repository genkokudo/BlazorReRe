using BlazorReRe.Client;
using BlazorReRe.Client.Extentions;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder
    .CreateDefault(args)
    .AddRootComponents()          // �g�����\�b�h�F"#app"��"<head>"��ǉ����鏈��;
    .AddClientServices()
    ;

await builder.Build().RunAsync();

// ��ŃT�[�r�X�̒ǉ����������Ă���̂ŁA�����K�v�Ȃ���o���ď�������������
// BlazorHero�ł̓��[�U�̃u���E�U�̃��[�J���X�g���[�W����A���̃A�v���P�[�V�����̕\���ݒ���擾�Ƃ�����Ă�B
// ��Blazored.LocalStorage ��Nuget�C���X�g�[�����K�v
