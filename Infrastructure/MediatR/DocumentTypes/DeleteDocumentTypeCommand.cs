using BlazorReRe.Shared.Wrapper;
using Domain.Entities.Misc;
using Infrastructure.Contexts;
using MediatR;
using Microsoft.Extensions.Localization;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.MediatR.DocumentTypes
{
    public class DeleteDocumentTypeCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }
    }

    internal class DeleteDocumentTypeCommandHandler : IRequestHandler<DeleteDocumentTypeCommand, Result<int>>
    {
        //private readonly IDocumentRepository _documentRepository;
        private readonly IStringLocalizer<DeleteDocumentTypeCommandHandler> _localizer;
        private readonly ApplicationDbContext _dbContext;

        public DeleteDocumentTypeCommandHandler(ApplicationDbContext dbContext, IStringLocalizer<DeleteDocumentTypeCommandHandler> localizer)
        {
            _dbContext = dbContext;
            //_documentRepository = documentRepository;
            _localizer = localizer;
        }

        public async Task<Result<int>> Handle(DeleteDocumentTypeCommand command, CancellationToken cancellationToken)
        {
            // 使用中のデータか確認する
            //var isDocumentTypeUsed = _dbContext.Documents.AnyAsync(b => b.DocumentTypeId == documentTypeId);
            //if (!isDocumentTypeUsed)
            //{
                var documentType = await _dbContext.DocumentTypes.FindAsync(command.Id);
                if (documentType != null)
                {
                    _dbContext.Remove(documentType);
                    await _dbContext.SaveChangesAsync();
                    return await Result<int>.SuccessAsync(documentType.Id, _localizer["Document Type Deleted"]);
                }
                else
                {
                    return await Result<int>.FailAsync(_localizer["Document Type Not Found!"]);
                }
            //}
            //else
            //{
            //    return await Result<int>.FailAsync(_localizer["Deletion Not Allowed"]);
            //}
        }
    }
}