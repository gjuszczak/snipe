using System;

namespace Snipe.App.Features.Redirections.Exceptions
{
    public class RedirectionDeletedException : Exception
    {
        public RedirectionDeletedException() :
            base("Redirection is deleted")
        {
        }
    }
}
