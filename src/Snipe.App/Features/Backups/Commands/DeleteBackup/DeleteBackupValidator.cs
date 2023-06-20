using FluentValidation;

namespace Snipe.App.Features.Backups.Commands.DeleteBackup
{
    public class DeleteBackupValidator : AbstractValidator<DeleteBackup>
    {
        public DeleteBackupValidator()
        {
            RuleFor(v => v.Name)
                .MaximumLength(50)
                .NotEmpty();
        }
    }
}
