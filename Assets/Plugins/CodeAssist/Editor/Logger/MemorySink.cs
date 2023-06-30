#nullable enable


using System;
using System.Collections.Concurrent;
using System.IO;
using System.Text;
using Serilog.Core;
using Serilog.Events;

namespace Plugins.CodeAssist.Editor.Logger
{
    //**--
    // remove this in unity???
    // need to serialize/deserialize data to survive domain reload, which will effect performance
    // right now data is lost during domain reloads, which makes its function kinda useless
    // or maybe move it to a external process like com.unity.process-server
    public class MemorySink : ILogEventSink
    {
        readonly ConcurrentQueue<LogEvent> _logs;
        readonly ConcurrentQueue<LogEvent[]> _warningLogs;
        readonly ConcurrentQueue<LogEvent[]> _errorLogs;

        const int LogsLimit = 30;
        const int WarningLimit = 5;
        const int ErrorLimit = 3;

        readonly string _outputTemplate;

        public MemorySink(string outputTemplate)
        {
            this._outputTemplate = outputTemplate;

            _logs = new ConcurrentQueue<LogEvent>();
            _warningLogs = new ConcurrentQueue<LogEvent[]>();
            _errorLogs = new ConcurrentQueue<LogEvent[]>();
        }

        public void Emit(LogEvent logEvent)
        {
            if (logEvent == null)
                return;

            _logs.Enqueue(logEvent);
            if (_logs.Count > LogsLimit)
                _logs.TryDequeue(out _);

            if (logEvent.Level == LogEventLevel.Warning)
            {
                var warningAndLeadingLogs = _logs.ToArray();
                _warningLogs.Enqueue(warningAndLeadingLogs);
                if (_warningLogs.Count > WarningLimit)
                    _warningLogs.TryDequeue(out _);
            }

            if (logEvent.Level == LogEventLevel.Error)
            {
                var errorAndLeadingLogs = _logs.ToArray();
                _errorLogs.Enqueue(errorAndLeadingLogs);
                if (_errorLogs.Count > ErrorLimit)
                    _errorLogs.TryDequeue(out _);
            }
        }

        public bool HasError => !_errorLogs.IsEmpty;
        public bool HasWarning => !_warningLogs.IsEmpty;
        public int ErrorCount => _errorLogs.Count;
        public int WarningCount => _warningLogs.Count;

        public string Export()
        {
            IFormatProvider? formatProvider = null;
            var formatter = new Serilog.Formatting.Display.MessageTemplateTextFormatter(
                _outputTemplate, formatProvider);

            var result = string.Empty;

            using (var outputStream = new MemoryStream())
            {
                var encoding = new UTF8Encoding(encoderShouldEmitUTF8Identifier: false);
                using var output = new StreamWriter(outputStream, encoding);
                if (!_errorLogs.IsEmpty)
                {
                    var errorArray = _errorLogs.ToArray();
                    foreach (var error in errorArray)
                    {
                        foreach (var logEvent in error)
                        {
                            formatter.Format(logEvent, output);
                        }
                    }
                }

                if (!_warningLogs.IsEmpty)
                {
                    var warningArray = _warningLogs.ToArray();
                    foreach (var warning in warningArray)
                    {
                        foreach (var logEvent in warning)
                        {
                            formatter.Format(logEvent, output);
                        }
                    }
                }

                if (!_logs.IsEmpty)
                {
                    var logArray = _logs.ToArray();
                    foreach (var logEvent in logArray)
                    {
                        formatter.Format(logEvent, output);
                    }
                }

                output.Flush();

                outputStream.Seek(0, SeekOrigin.Begin);
                using var streamReader = new StreamReader(outputStream, encoding);
                result = streamReader.ReadToEnd();


            }

            return result;
        }


    }
}
