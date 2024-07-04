using System;

namespace GymLogger.Exceptions
{
    public class GymLoggerForbiddenException : GymLoggerException
    {
        public GymLoggerForbiddenException()
        {
        }

        public GymLoggerForbiddenException(string message) : base(message)
        {
        }

        public GymLoggerForbiddenException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public GymLoggerForbiddenException(string message, string? key = null, Exception? innerException = null)
            : base(message, key, innerException)
        {
        }

        protected override string GetDefaultKey()
        {
            return GenerateDefaultKey(base.GetDefaultKey(), "Forbidden");
        }
    }
}
