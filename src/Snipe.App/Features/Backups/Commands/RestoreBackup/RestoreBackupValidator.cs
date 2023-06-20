using FluentValidation;

namespace Snipe.App.Features.Backups.Commands.RestoreBackup
{
    public class RestoreBackupValidator : AbstractValidator<RestoreBackup>
    {
        public RestoreBackupValidator()
        {
            RuleFor(v => v.Name)
                .MaximumLength(50)
                .NotEmpty();
        }
    }
}
