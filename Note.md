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
Server�̏���������AddAutoMapper��AddMediatR��ǉ��B  
BlazorHero����ڂ��BApplication.Features����AInfrastructure.MediatR�ցB  
BlazorHero�̓t�H���_���������Ȃ̂ŁA�e�[�u�����ƂɃt�H���_��؂�B  
������Query��Response�ADB�X�V��Command�Ƃ������O��t����B  

�ǂ����S�Ă�API���ʂ�Succeeded�t���O��Messages�͂��������������̂ŁABlazorHero�݂�����Result�Ń��b�v���������֗��B   
�Ƃ����킯��BlazorPractice.Shared.Wrapper����Result���ڂ��Ă���B

* AutoMapper���g�p����  
���キ�����BBlazorHero����ʂ��Ă��邱��

* Localizer����������  
���������ǂ����ǁA�����̗��ꂪ�������Ă��Ȃ��̂ŃR�����g�A�E�g���Ă�����CRUD���o���������B

* �f�[�^�ꗗ�̎擾�i�N���C�A���g���j  
Client��Pages�ɐV�����y�[�W���쐬�B  
NavMenu.razor�Ƀ����N��ǉ�����B  

# ��邱��
* ���[�U���f�[�^��o�^����(CRUD)  
https://www.c-sharpcorner.com/article/crud-operations-using-blazor-net-6-0-entity-framework-core/
* �e�e�[�u���Ƀ��[�UID���������āA�o�^���Ɏ����œ����i�C���^�t�F�[�X��ǉ����邪�A�č����ڃC���^�t�F�[�X�Ƃ͕�����j
* �O���[�o���N�G���t�B���^���������āA���[�U���o�^�����f�[�^�����\������  

Server��Infrastructure�ɑ΂���  
PM> Install-Package AutoMapper  
PM> Install-Package MediatR  
PM> Install-Package MediatR.Extensions.Microsoft.DependencyInjection  
PM> Install-Package AutoMapper.Extensions.Microsoft.DependencyInjection  

* FluentValidation

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
* Microsoft.AspNetCore.App �̃����^�C�� �p�b�N������܂���ł����B  
���̃G���[��Microsoft.AspNetCore.ApiAuthorization.IdentityServer ��������̂��v���W�F�N�g�Q�Ƃ���Əo��B
Infrastructure��DB�X�V�Ɏg���Ă���̂ŁA�����ȊO�Ƀ��X�|���X�̌^���ړ������邩�A���f���N���X�������Map����K�v������B
WeatherForecast�ɕ키�ꍇ�AShared��Model�����̂��K�؂��B

������WeatherForecast�ɕ����API�𒼂��܂��傤�B
���܂��́AModel�͍�炸��Shared��MediatoR�̓��ꕨ���ڂ�������OK�H
�����͏o�����Model�w������Ă��������B�BPizza�̓f�[�^���T�[�o�ɑ��鎞�ǂ�����Ă��H
�����f���Ƃ���炸�ɑS��Shared��Entity�������Ă�BMediatoR���ז��E�E�E









