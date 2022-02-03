namespace BlazorSecond.Server.MediatR.DocumentTypes
{
    // GetDocumentTypeByIdResponseと一緒。
    public class GetAllDocumentTypesResponse
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
    }
}