using Snipe.App.Core.Commands;

namespace Snipe.App.Features.Redirections.Commands
{
    public abstract class RedirectionBaseCommand : Command
    {
        public string Name { get; set; }
        public string Url { get; set; }
    }
}
