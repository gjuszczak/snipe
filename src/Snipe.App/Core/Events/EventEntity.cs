﻿using System;
using System.Text.Json;

namespace Snipe.App.Core.Events
{
    public class EventEntity
	{
		public long EventId { get; set; }
		public DateTimeOffset TimeStamp { get; set; }
		public string EventType { get; set; }
		public JsonElement Data { get; set; }
		public Guid AggregateId { get; set; }
		public string AggregateType { get; set; }
		public int Version { get; set; }
		public Guid CorrelationId { get; set; }
	}
}
