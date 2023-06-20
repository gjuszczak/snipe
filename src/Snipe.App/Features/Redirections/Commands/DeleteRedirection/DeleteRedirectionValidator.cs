﻿using FluentValidation;

namespace Snipe.App.Features.Redirections.Commands.DeleteRedirection
{
    public class DeleteRedirectionValidator : AbstractValidator<DeleteRedirection>
    {
        public DeleteRedirectionValidator()
        {
            RuleFor(v => v.Id)
                .NotEmpty();
        }
    }
}
