using Snipe.App.Features.Common.Models;
using System.Collections.Generic;

namespace Snipe.App.Features.Backups.Queries.GetBackupFiles
{
    public class BackupFilesListDto : PaginatedList<BackupFileDto>
    {
        public BackupFilesListDto(List<BackupFileDto> items, int totalRecords, int first, int rows)
            : base(items, totalRecords, first, rows) { }
    }
}
