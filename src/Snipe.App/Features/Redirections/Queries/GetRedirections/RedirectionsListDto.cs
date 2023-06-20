using Snipe.App.Features.Common.Models;
using System.Collections.Generic;

namespace Snipe.App.Features.Redirections.Queries.GetRedirections
{
    public class RedirectionsListDto : PaginatedList<RedirectionDto>
    {
        public RedirectionsListDto(List<RedirectionDto> items, int totalRecords, int first, int rows)
            : base(items, totalRecords, first, rows) { }
    }
}
