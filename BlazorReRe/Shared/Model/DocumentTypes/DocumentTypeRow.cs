namespace BlazorReRe.Shared.Model.DocumentTypes
{
    /// <summary>
    /// 1行分のデータ
    /// GetAllDocumentTypesResponse
    /// GetDocumentTypeByIdResponse と同じ構造
    /// </summary>
    public class DocumentTypeRow
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
    }
}
