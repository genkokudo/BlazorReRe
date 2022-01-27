using AutoMapper;
using Domain.Entities.Misc;
using BlazorReRe.Server.MediatR.DocumentTypes;
using BlazorReRe.Shared.Model.DocumentTypes;
using BlazorReRe.Shared.Wrapper;

namespace BlazorReRe.Server.Mappings
{
    public class DocumentTypeProfile : Profile
    {
        public DocumentTypeProfile()
        {
            CreateMap<AddEditDocumentTypeCommand, DocumentTypeDto>().ReverseMap();
            CreateMap<AddEditDocumentTypeCommand, DocumentType>().ReverseMap();
            CreateMap<GetDocumentTypeByIdResponse, DocumentType>().ReverseMap();
            CreateMap<GetAllDocumentTypesResponse, DocumentType>().ReverseMap();

            // Result<List<GetAllDocumentTypesResponse>>, Result<List<DocumentTypeRow>>の変換をするには、以下の両方必要
            CreateMap(typeof(Result<>), typeof(Result<>)).ReverseMap();             // TODO:これは汎用なので別ソースファイルにすること
            CreateMap<GetAllDocumentTypesResponse, DocumentTypeDto>().ReverseMap();
        }
    }
}