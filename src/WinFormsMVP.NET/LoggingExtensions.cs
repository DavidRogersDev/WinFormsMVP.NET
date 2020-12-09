using System;
using Microsoft.Extensions.Logging;

namespace WinFormsMVP.NET
{
    public static class LoggingExtensions
    {
        private static readonly Action<ILogger, string, Exception> FindingBindingsTrace;
        private static readonly Action<ILogger, string, Exception> BindingsInfoTrace;
        private static readonly Action<ILogger, string, string, string, Exception> ActualBindingDetailsTrace;

        static LoggingExtensions()
        {

            BindingsInfoTrace = LoggerMessage.Define<string>(
                LogLevel.Debug,
                new EventId((int)TraceEventIdentifiers.BindingsInfoTrace, nameof(TraceBindingInfo)),
                "{@message}"
                );

            FindingBindingsTrace = LoggerMessage.Define<string>(
                LogLevel.Debug,
                new EventId((int)TraceEventIdentifiers.FindingBindingsMessageTrace, nameof(TraceMessageFindingBindings)),
                "Finding presenter bindings using '{@name}'"
                );

            ActualBindingDetailsTrace = LoggerMessage.Define<string, string, string>(
                LogLevel.Debug,
                new EventId((int)TraceEventIdentifiers.ActualBindingDetailsTrace, nameof(TraceActualBindingDetails)),
                "Creating presenter of type '{@presenterTypeName}' for view of type '{@viewTypeName}'. (The actual view instance is of type '{@viewInstanceName}'.)"
                );

        }

        public static void TraceBindingInfo(this ILogger logger, string message)
        {
            BindingsInfoTrace(logger, message, null);
        }
        public static void TraceMessageFindingBindings(this ILogger logger, string name)
        {
            FindingBindingsTrace(logger, name, null);
        }

        public static void TraceActualBindingDetails(this ILogger logger, string presenterTypeName, string viewTypeName, string viewInstanceName)
        {
            ActualBindingDetailsTrace(logger, presenterTypeName, viewTypeName, viewInstanceName, null);
        }
    }
}
