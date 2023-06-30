#nullable enable


using Serilog.Core;
using Serilog.Events;

namespace Plugins.CodeAssist.Editor.Logger
{
    public class DomainHashEnricher : ILogEventEnricher
    {
        static readonly int DomainHash;

        static DomainHashEnricher()
        {
            var guid = UnityEditor.GUID.Generate();
            DomainHash = guid.GetHashCode();
        }

        public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
        {
            logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty(
                    "DomainHash", DomainHash));
        }
    }

}