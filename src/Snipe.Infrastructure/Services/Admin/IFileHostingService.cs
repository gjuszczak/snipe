﻿using Snipe.App.Features.Backups.Queries.GetBackupFiles;
using System.Threading;
using System.Threading.Tasks;

namespace Snipe.Infrastructure.Services.Admin
{
    public interface IFileHostingService
    {
        Task UploadAsync(string localFilePath, string remoteFileName, CancellationToken cancellationToken);
        Task DeleteAsync(string remoteFileName, CancellationToken cancellationToken);
        Task RenameAsync(string oldRemoteFileName, string newRemoteFileName, CancellationToken cancellationToken);
        Task<TemporaryPath> DownloadAsync(string remoteFileName, CancellationToken cancellationToken);
        Task<BackupFilesListDto> ListAsync(int offset, int batchSize, CancellationToken cancellationToken);
    }
}
