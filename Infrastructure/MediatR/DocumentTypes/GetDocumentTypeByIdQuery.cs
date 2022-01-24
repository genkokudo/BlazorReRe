using AutoMapper;
using BlazorReRe.Shared.Wrapper;
using Infrastructure.Contexts;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.MediatR.DocumentTypes
{
    public class GetDocumentTypeByIdQuery : IRequest<Result<GetDocumentTypeByIdResponse>>
    {
        public int Id { get; set; }
    }

    internal class GetDocumentTypeByIdQueryHandler : IRequestHandler<GetDocumentTypeByIdQuery, Result<GetDocumentTypeByIdResponse>>
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetDocumentTypeByIdQueryHandler(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<Result<GetDocumentTypeByIdResponse>> Handle(GetDocumentTypeByIdQuery query, CancellationToken cancellationToken)
        {
            var documentType = await _dbContext.DocumentTypes.FindAsync(query.Id);
            var mappedDocumentType = _mapper.Map<GetDocumentTypeByIdResponse>(documentType);
            return await Result<GetDocumentTypeByIdResponse>.SuccessAsync(mappedDocumentType);
        }
    }
}