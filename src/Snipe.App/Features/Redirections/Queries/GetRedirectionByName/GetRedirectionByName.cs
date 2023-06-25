using Snipe.App.Core.Queries;
using Snipe.App.Features.Common.Services;
using Microsoft.EntityFrameworkCore;

namespace Snipe.App.Features.Redirections.Queries.GetRedirectionByName
{
    public class GetRedirectionByName : IQuery<RedirectionDto>
    {
        public string Name { get; set; }
    }

    public class GetRedirectionByNameHandler : IQueryHandler<GetRedirectionByName, RedirectionDto>
    {
        private readonly IAppDbContext _context;

        public GetRedirectionByNameHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<RedirectionDto> Handle(GetRedirectionByName request, CancellationToken cancellationToken)
        {
            var redirection = await _context.Redirections
                .SingleOrDefaultAsync(entity => entity.Name == request.Name, cancellationToken);
            
            if (redirection == null)
            {
                return null;
            }

            return RedirectionDto.FromEntity(redirection);
        }
    }
}
