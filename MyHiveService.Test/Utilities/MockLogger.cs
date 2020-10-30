using System;
using Microsoft.Extensions.Logging;

namespace MyHiveService.Test.Utilities
{
    public class MockLogger<T> : ILogger<T>
    {
        public IDisposable BeginScope<TState>(TState state)
        {
            return new MockDisposable();
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return false;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            // null op
        }
    }
}