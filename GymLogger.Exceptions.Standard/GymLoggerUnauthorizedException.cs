using System;

namespace GymLogger.Exceptions
{
    public class GymLoggerUnauthorizedException : GymLoggerException
    {
        public GymLoggerUnauthorizedException()
        {
        }

        public GymLoggerUnauthorizedException(string message) : base(message)
        {
        }

        public GymLoggerUnauthorizedException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public GymLoggerUnauthorizedException(string message, string key = null, Exception innerException = null)
            : base(message, key, innerException)
        {
        }

        protected override string GetDefaultKey()
        {
            return GenerateDefaultKey(base.GetDefaultKey(), "Unauthorized");
        }
    }
}
