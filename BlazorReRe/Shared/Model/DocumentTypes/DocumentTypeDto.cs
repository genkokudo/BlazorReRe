using System.ComponentModel.DataAnnotations;

namespace BlazorReRe.Shared.Model.DocumentTypes
{
    /// <summary>
    /// 1行分のデータ
    /// GetAllDocumentTypesResponse
    /// GetDocumentTypeByIdResponse と同じ構造
    /// </summary>
    public class DocumentTypeDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "{0}は必須入力だ！")]
        [StringLength(20)]
        public string? Name { get; set; }

        public string? Description { get; set; }
    }
}
