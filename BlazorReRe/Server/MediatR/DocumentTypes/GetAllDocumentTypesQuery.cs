using AutoMapper;
using BlazorReRe.Shared.Wrapper;
using Infrastructure.Contexts;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace BlazorReRe.Server.MediatR.DocumentTypes
{
    public class GetAllDocumentTypesQuery : IRequest<Result<List<GetAllDocumentTypesResponse>>>
    {
        public GetAllDocumentTypesQuery()
        {
        }
    }

    internal class GetAllDocumentTypesQueryHandler : IRequestHandler<GetAllDocumentTypesQuery, Result<List<GetAllDocumentTypesResponse>>>
    {
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _dbContext;

        public GetAllDocumentTypesQueryHandler(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<Result<List<GetAllDocumentTypesResponse>>> Handle(GetAllDocumentTypesQuery request, CancellationToken cancellationToken)
        {
            var mappedDocumentTypes = _mapper.Map<List<GetAllDocumentTypesResponse>>(await _dbContext.DocumentTypes.ToListAsync());
            return await Result<List<GetAllDocumentTypesResponse>>.SuccessAsync(mappedDocumentTypes);
        }
    }
}