using System;

namespace Snipe.App.Core.Events
{
    public interface IEvent
	{		
		long EventId { get; set; }
		Guid AggregateId { get; set; }
        Type AggregateType { get; set; }
        Guid CorrelationId { get; set; }
		int Version { get; set; }
		DateTimeOffset TimeStamp { get; set; }
	}
}
