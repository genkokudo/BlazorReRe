using BlazorReRe.Shared.Model.DocumentTypes;
using FluentValidation;

namespace BlazorReRe.Server.Validators
{
    public class DocumentTypeValidator : AbstractValidator<DocumentTypeDto>
    {
        public DocumentTypeValidator()
        {
            RuleFor(data => data.Name).NotNull().MaximumLength(20).WithMessage("名前は20文字以内にしてくださーい");
        }
    }
}
