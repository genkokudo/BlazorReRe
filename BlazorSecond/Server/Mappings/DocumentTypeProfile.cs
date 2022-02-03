using AutoMapper;
using Domain.Entities.Misc;
using BlazorSecond.Server.MediatR.DocumentTypes;
using BlazorSecond.Shared.Wrapper;
using BlazorSecond.Shared.Model.DocumentTypes;

namespace BlazorSecond.Server.Mappings
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