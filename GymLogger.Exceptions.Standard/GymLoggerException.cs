using System;
using System.Collections.Generic;
using System.Text;

namespace GymLogger.Exceptions
{
    public class GymLoggerException : Exception
    {
        public string Key { get; private set; }

        public GymLoggerException()
        {
            this.Key = this.GetDefaultKey();
        }

        public GymLoggerException(string message) : base(message)
        {
            this.Key = this.GetDefaultKey();
        }

        public GymLoggerException(string message, Exception innerException)
            : base(message, innerException)
        {
            this.Key = this.GetDefaultKey();
        }

        public GymLoggerException(string message, string? key = null, Exception? innerException = null)
            : base(message, innerException)
        {
            this.Key = key == null ? this.GetDefaultKey() : GenerateKey(this.GetDefaultKey(), key);
        }

        protected static string GenerateDefaultKey(string baseDefaultKey, string defaultKey)
        {
            return $"{baseDefaultKey}.{defaultKey}";
        }

        private static string GenerateKey(string defaultKey, string key)
        {
            return $"{defaultKey}-{key}";
        }

        protected virtual string GetDefaultKey()
        {
            return "GymLogger";
        }
    }
}
