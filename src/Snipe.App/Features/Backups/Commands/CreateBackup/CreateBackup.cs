using Snipe.App.Core.Commands;
using Snipe.App.Features.Backups.Services;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Snipe.App.Features.Backups.Commands.CreateBackup
{
    public class CreateBackup : Command
    {
        public string Name { get; set; }
    }

    public class CreateBackupHandler : ICommandHandler<CreateBackup>
    {
        private readonly IBackupService _backupService;

        public CreateBackupHandler(IBackupService backupService)
        {
            _backupService = backupService;
        }

        public async Task<Guid> Handle(CreateBackup command, CancellationToken cancellationToken)
        {
            await _backupService.CreateAsync(command.Name, cancellationToken);
            return command.CorrelationId;
        }
    }
}
