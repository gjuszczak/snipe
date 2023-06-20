using Snipe.App.Core.Aggregates;
using Snipe.App.EventLog.Services.DetailsProviding;
using System;
using System.Text.Json;

namespace Snipe.App.Features.EventLog.Services.DetailsProviding
{
    public class AggregateDetailsProvider : DetailsProvider<IAggregateRoot, AggregateDetails>, IAggregateDetailsProvider
    {
        public AggregateDetailsProvider(ISensitiveDataMaskConfiguration configuration)
            : base(configuration) { }

        protected override Func<IAggregateRoot, AggregateDetails> GetDetailsFactory(IAggregateRoot firstAggregate)
        {
            var aggregateType = firstAggregate.GetType();
            var aggregateTypeString = aggregateType.FullName;
            var aggregateTypeDisplayName = GetDisplayName(aggregateType);
            var maskSensitiveDataSerializerOptions = GetMaskSensitiveDataSerializerOptions();
            return (aggregate) => new AggregateDetails
            {
                AggregateType = aggregateTypeString,
                AggregateTypeDisplayName = aggregateTypeDisplayName,
                MaskedPayload = JsonSerializer.SerializeToDocument(aggregate, aggregateType, maskSensitiveDataSerializerOptions)
            };
        }
    }
}
