using AutoMapper;
using BlazorReRe.Shared.Wrapper;
using Domain.Entities.Misc;
using Infrastructure.Contexts;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BlazorReRe.Server.MediatR.DocumentTypes
{
    public class AddEditDocumentTypeCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Description { get; set; }
    }

    internal class AddEditDocumentTypeCommandHandler : IRequestHandler<AddEditDocumentTypeCommand, Result<int>>
    {
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<AddEditDocumentTypeCommandHandler> _localizer;
        private readonly ApplicationDbContext _dbContext;

        public AddEditDocumentTypeCommandHandler(ApplicationDbContext dbContext, IMapper mapper, IStringLocalizer<AddEditDocumentTypeCommandHandler> localizer)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _localizer = localizer;
        }

        public async Task<Result<int>> Handle(AddEditDocumentTypeCommand command, CancellationToken cancellationToken)
        {
            if (await _dbContext.DocumentTypes.Where(p => p.Id != command.Id)
                .AnyAsync(p => p.Name == command.Name, cancellationToken))
            {
                return await Result<int>.FailAsync(_localizer["Document type with this name already exists."]);
            }

            if (command.Id == 0)    // 追加の場合
            {
                var documentType = _mapper.Map<DocumentType>(command);                      // AutoMapperで変換
                await _dbContext.DocumentTypes.AddAsync(documentType);
                await _dbContext.SaveChangesAsync();
                return await Result<int>.SuccessAsync(documentType.Id, _localizer["Document Type Saved"]);
            }
            else
            {
                var documentType = await _dbContext.DocumentTypes.FindAsync(command.Id);
                if (documentType != null)
                {
                    documentType.Name = command.Name ?? documentType.Name;
                    documentType.Description = command.Description ?? documentType.Description;
                    await _dbContext.SaveChangesAsync();
                    return await Result<int>.SuccessAsync(documentType.Id, _localizer["Document Type Updated"]);
                }
                else
                {
                    return await Result<int>.FailAsync(_localizer["Document Type Not Found!"]);
                }
            }
        }
    }
}