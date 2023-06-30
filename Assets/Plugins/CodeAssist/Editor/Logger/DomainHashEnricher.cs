#nullable enable


using Serilog.Core;
using Serilog.Events;

namespace Plugins.CodeAssist.Editor.Logger
{
    public class DomainHashEnricher : ILogEventEnricher
    {
        static readonly int domainHash;

        static DomainHashEnricher()
        {
            var guid = UnityEditor.GUID.Generate();
            domainHash = guid.GetHashCode();
        }

        public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
        {
            logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty(
                    "DomainHash", domainHash));
        }
    }

}