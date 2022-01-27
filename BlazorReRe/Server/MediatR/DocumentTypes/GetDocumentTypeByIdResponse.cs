namespace BlazorReRe.Server.MediatR.DocumentTypes
{
    // GetAllDocumentTypesResponseと一緒。
    public class GetDocumentTypeByIdResponse
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
    }
}