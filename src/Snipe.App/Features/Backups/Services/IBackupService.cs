﻿using Snipe.App.Features.Backups.Queries.GetBackupFiles;
using System.Threading;
using System.Threading.Tasks;

namespace Snipe.App.Features.Backups.Services
{
    public interface IBackupService
    {
        Task CreateAsync(string name, CancellationToken cancellationToken);
        Task DeleteAsync(string name, CancellationToken cancellationToken);
        Task RenameAsync(string oldName, string newName, CancellationToken cancellationToken);
        Task RestoreAsync(string name, CancellationToken cancellationToken);
        Task<BackupFilesListDto> GetAsync(int offset, int batchSize, CancellationToken cancellationToken);
    }
}
