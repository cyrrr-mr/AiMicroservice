using AiMicroservice.Domain.Models;
using FluentValidation;

namespace AiMicroservice.Application.Validators;

public class AIRequestValidator : AbstractValidator<AIRequest>
{
    public AIRequestValidator()
    {
        RuleFor(x => x.RequestId)
            .NotEmpty().WithMessage("requestId est obligatoire.");

        RuleFor(x => x.Capability)
            .NotEmpty().WithMessage("capability est obligatoire.");

        RuleFor(x => x.Messages)
            .NotEmpty().WithMessage("La liste messages ne peut pas être vide.");

        RuleForEach(x => x.Messages).ChildRules(message =>
        {
            message.RuleFor(m => m.Role)
                .NotEmpty().WithMessage("Chaque message doit avoir un role.");

            message.RuleFor(m => m.Content)
                .NotEmpty().WithMessage("Chaque message doit avoir un content.");
        });
    }
}