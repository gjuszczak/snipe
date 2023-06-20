﻿using System;

namespace Snipe.App.Features.Backups.Queries.GetBackupFiles
{
    public class BackupFileDto
    {
        public string Name { get; set; }
        public long Size { get; set; }
        public DateTimeOffset CreatedDateTime { get; set; }
    }
}
