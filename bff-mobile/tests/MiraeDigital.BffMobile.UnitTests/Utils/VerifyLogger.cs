using Moq;
using System;
using Microsoft.Extensions.Logging;

namespace MiraeDigital.BffMobile.UnitTests.Utils
{
    internal static class VerifyLogger
    {
        public static void Verify<T>(Mock<ILogger<T>> mockLogger, string message, LogLevel logLevel, Times times)
        {
            mockLogger.Verify(logger => logger.Log(
              It.Is<LogLevel>(_logLevel => _logLevel == logLevel),
            It.IsAny<EventId>(),
            It.Is<It.IsAnyType>((@object, @type) =>
            @object.ToString().Contains(message) && @type.Name == "FormattedLogValues"),
            It.IsAny<Exception>(),
            It.IsAny<Func<It.IsAnyType, Exception, string>>()), times);
        }
    }
}
