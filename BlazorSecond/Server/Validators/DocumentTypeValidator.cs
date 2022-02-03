using BlazorSecond.Shared.Model.DocumentTypes;
using FluentValidation;

namespace BlazorSecond.Server.Validators
{
    public class DocumentTypeValidator : AbstractValidator<DocumentTypeDto>
    {
        public DocumentTypeValidator()
        {
            RuleFor(data => data.Name).NotNull().MaximumLength(20).WithMessage("名前は20文字以内にしてくださーい");
        }
    }
}
