using System;
using System.Collections.Generic;

namespace Snipe.App.Features.EventLog.Services.DetailsProviding
{
    public interface ISensitiveDataMaskConfiguration
    {
        string MaskString { get; }

        IEnumerable<string> GetMaskedProperties(Type type);
        bool IsTypeMasked(Type type);
    }
}