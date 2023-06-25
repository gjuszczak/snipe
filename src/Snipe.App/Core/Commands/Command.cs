﻿namespace Snipe.App.Core.Commands
{
    public abstract class Command : ICommand
    {
		public Guid Id { get; set; }
		public Guid CorrelationId { get; set; }
		public int ExpectedVersion { get; set; }

		protected Command()
        {
			Id = Guid.NewGuid();
			CorrelationId = Guid.Empty;
			ExpectedVersion = 1;
		}
	}
}
