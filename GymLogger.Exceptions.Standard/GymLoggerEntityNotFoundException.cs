using System;

namespace GymLogger.Exceptions
{
    public class GymLoggerEntityNotFoundException : GymLoggerException
    {
        public GymLoggerEntityNotFoundException()
        {
        }

        public GymLoggerEntityNotFoundException(string message) : base(message)
        {
        }

        public GymLoggerEntityNotFoundException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public GymLoggerEntityNotFoundException(string message, string? key = null, Exception? innerException = null)
            : base(message, key, innerException)
        {
        }

        protected override string GetDefaultKey()
        {
            return GenerateDefaultKey(base.GetDefaultKey(), "EntityNotFound");
        }
    }
}
