﻿namespace Snipe.App.Features.EventLog.Services.DetailsProviding
{
    public class AggregateDetails
    {
        public string AggregateType { get; set; }
        public string AggregateTypeDisplayName { get; set; }
        public object MaskedPayload { get; set; }
    }
}
