# ���������

* Program.cs�̒���Extentions�Ɉړ�  
�w�ǈӖ��Ȃ������B  
�ݒ荀�ڂ������Ă����烁�\�b�h�������猩�₷�����Ȃ��Ē��x�B

* Infrastructure�v���W�F�N�g�쐬  
�N���X���C�u����NET6�ō쐬  
Contexts�t�H���_���쐬����DbContext�������Ɉړ�  
Models���ړ�  
Server��Data�t�H���_�폜  
�i�ړ��Ȃǂ����Ƃ��̓v���W�F�N�g���E�N���b�N���Ė��O��Ԃ̓���������j

* Domain�v���W�F�N�g�쐬  
Infrastructure��.NET6�ŁADomain��.NET Standard2.1  
.NET Standard��.NET6�̐��E�ł͂����v��Ȃ����̂����A.NET�̑��̂��ׂĂ̎����̊ԂŃR�[�h�����L���鎞�Ɏg���B  
������Entity�ƁA����IEntity���쐬����B  
DbContext�ɏ�L��Entity��ǉ�  
��ApplicationUser�́AStandard�ł͂Ȃ��̂�Infrastructure��Models�ɒu�����ƁB

* appsettings.json��DB����ύX
* Add-Migration Initial  
�r���h�G���[���o��̂ŁA�p�b�P�[�W�̃C���X�g�[�������ĉ�������B
* Update-Database
* �����ŋN�����Ă݂āA���[�U�o�^���ł��邱�Ƃ��m�F����B

* DB�̏����f�[�^����  
Infrastructure��Interfaces��ǉ��B  
BlazorPractice��Application�v���W�F�N�g�̂悤�ɕ�����K�v�͖����Ǝv���̂ŁA�����ɂ�����̂�Infrastructure�ɍ��B
* DbContext�ɍX�V���Ɏ����Ŋč����ڂ�����@�\��ǉ�
BlazorHeroContext���R�s�[���Ă��āA����Ȃ��T�[�r�X���R�s�[���Ă���B

* �f�[�^�ꗗ�̎擾�i�T�[�o���j  
Server.Controllers��API�R���g���[�����쐬����
BaseApiController���쐬���A�R���g���[����MediatR���g�p����悤�Ɏw��B�i�v��Ȃ������[�j

* MediatR���g�p����  
Server��Infrastructure�ɑ΂��Ď��s  
```
PM> Install-Package AutoMapper
PM> Install-Package AutoMapper.Extensions.Microsoft.DependencyInjection
```
Server�̏���������AddAutoMapper��AddMediatR��ǉ��B  
�i�o�[�W����������Ȃ��ƌx�����o��̂ŁA���̏ꍇ�͉����邱�Ɓj  
BlazorHero����ڂ��BApplication.Features����AServer.MediatR�ցB  
�iInfrastructure��MediatR��u�����珉��������MediatR�o�^����Ȃ������B�A�Z���u���w����@��������Ȃ��j  
BlazorHero�̓t�H���_���������Ȃ̂ŁA�e�[�u�����ƂɃt�H���_��؂�B  
������Query��Response�ADB�X�V��Command�Ƃ������O��t����̂����܂�B  

�ǂ����S�Ă�API���ʂ�Succeeded�t���O��Messages�͂��������������̂ŁABlazorHero�݂�����Result�Ń��b�v���������֗��B   
�Ƃ����킯��BlazorPractice.Shared.Wrapper����Result���ڂ��Ă���B

* AutoMapper���g�p����  
Server�ɑ΂��Ď��s  
```
PM> Install-Package MediatR  
PM> Install-Package MediatR.Extensions.Microsoft.DependencyInjection
```
ConfigureServices��AddAutoMapper������  
Application��Mappings��Server�v���W�F�N�g�ɃR�s�[�B
BlazorHero�ƈ����Result���}�b�v����̂ŁA`CreateMap(typeof(Result<>), typeof(Result<>)).ReverseMap();`���K�v�B

* Localizer����������  
���������ǂ����ǁA�����̗��ꂪ�������Ă��Ȃ��̂ŃR�����g�A�E�g���Ă�����CRUD���o���������B

* �N���C�A���g�T�[�o�����f�[�^���f���̍쐬
Shared��Model���쐬����B
Model�͉�ʂɍ��킹�č쐬���AController��MediatR�p�N�G���ɕϊ�����B

* �f�[�^�ꗗ�̎擾�i�N���C�A���g���j  
Client��Pages�ɐV�����y�[�W���쐬�B  
NavMenu.razor�Ƀ����N��ǉ�����B  

* Font Awesome 5��ǉ�  
Blazor�ɒ���JS�̃��[�h�w��͏����Ȃ��̂ŁAindex.html�ɏ����B

* ���[�J���C�Y
* https://kuttsun.blogspot.com/2017/09/aspnet-core.html  
Server��Extentions�i���������j�Ɉȉ���ǉ��B�֘A�����N���X���R�s�[���Ă���B  
`app.UseRequestLocalizationByCulture()`  
`services.AddLocalization()` �����IStringLocalizer��DI�ɕK�v�Ȃ̂ŁA���[�J���C�Y�̉\��������V�X�e���͓���Ă����A�ŏ����烍�[�J���C�Y�ɑΉ������L�q�ŃR�[�f�B���O���邱�ƁB  
�����IStringLocalizer��DI�ł���悤�ɂȂ�̂ŁA�Ή����K�v�ɂȂ肻���ȃV�X�e���ł͂���Ă������ƁB

* �X�V�ƍ폜�̎���  
���ʂɂł����B�o�^�ƍX�V�͕����Ȃ��Ă��ǂ��B  

# ��邱��
* �e�e�[�u���Ƀ��[�UID���������āA�o�^���Ɏ����œ����i�C���^�t�F�[�X��ǉ����邪�A�č����ڃC���^�t�F�[�X�Ƃ͕�����j
* �O���[�o���N�G���t�B���^���������āA���[�U���o�^�����f�[�^�����\������  

* FluentValidation  
�N���C�A���g���̃o���f�[�V�����ƃT�[�o���̃o���f�[�V�����������炢���邱��
�N���C�A���g���̃o���f�[�V�����̓Z�L�����e�B�I�ɂ͖��Ӗ��Ȃ̂ŁA���[�U�r���e�B����̂��߂��Ǝv�����ƁB

# ��߂�����
* UnitOfWork�p�^�[��  
���K�͂̃V�X�e���̏ꍇ�A���ʂɗv��Ȃ�

* Repository�p�^�[��  
RepositoryAsync�N���X��CRUD�̃R�[�h�������L�o���邩��֗������ǁc�B  
Include�ł��Ȃ��B�i�ł��邯�Ǔ���H�j  
������Ƃ���ł���ȂɃR�[�f�B���O�ʕς��Ȃ��B

* �������ʂ̃L���b�V��  
���K�Ȃ̂ł��Ȃ��B

# ���
* �uMicrosoft.AspNetCore.App �̃����^�C�� �p�b�N������܂���ł����B�v  
���̃G���[��Client���AMicrosoft.AspNetCore.ApiAuthorization.IdentityServer ������v���W�F�N�g���Q�Ƃ���Əo��B
Infrastructure��DB�X�V�Ɏg���Ă���̂ŁA�����ȊO�Ƀ��X�|���X�̌^���ړ������邩�A���f���N���X�������Map����K�v������B

* �t�����g����CRUD�܂ł̗���
�N���C�A���g�ƃT�[�o�̂����Ɏg���f�[�^���f���́AShared�ɒu���̂���{�B  
�܂��A�N���C�A���g���͂�MediatR��Query��A�N���C�A���g�ƕ\����MediatR��Response���Ή����Ă���Ƃ͌���Ȃ��̂ŁA�����Controller�ŋz�����ׂ��B  
Shared��Model�͉�ʂɍ��킹���`�ō쐬������Y��ɂł���͂��B

* �T�C���C���Ȃ���API�@���ƃG���[  
AccessTokenNotAvailableException  
�u�F�؁v�I�v�V������L���ɂ��č쐬�����v���W�F�N�g�́A�T�C���C�����Ă��邩�ǂ����Ɋւ�炸HTTP ���N�G�X�g�ɏ�ɃA�N�Z�X�g�[�N����Y�t����悤�\������Ă���B  
�����Client��AddHttpClient�̏������������đΉ�����B
@inject HttpClient Http�͎g�p�֎~�B

* API�̃��[�e�B���O����肭�������A�uSorry, there's nothing at this address.�v���o��  
�C�̂����B�Ȃ񂩏���ɒ������B`MapControllers()`�͏����Ă��邩�m�F���邱�ƁB

* ���̑�CRUD�Ŏ��s������
AutoMapper�~�X���Ă�\��������̂Ń��O���悭���邱��















