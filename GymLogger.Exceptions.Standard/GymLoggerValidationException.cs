using System;
using System.Collections.Generic;

namespace GymLogger.Exceptions
{
    public class GymLoggerValidationException : GymLoggerException
    {
        public Dictionary<string, string> FieldErrors { get; private set; } = new Dictionary<string, string>();

        public GymLoggerValidationException()
        {
        }

        public GymLoggerValidationException(string message) : base(message)
        {
        }

        public GymLoggerValidationException(string message, Exception innerException, Dictionary<string, string> fieldErrors = null)
            : base(message, innerException)
        {
            if (fieldErrors != null)
            {
                this.FieldErrors = fieldErrors;
            }
        }

        public GymLoggerValidationException(string message, Dictionary<string, string> fieldErrors = null, string key = null, Exception innerException = null)
            : base(message, key, innerException)
        {
            if (fieldErrors != null)
            {
                this.FieldErrors = fieldErrors;
            }
        }

        
        protected override string GetDefaultKey()
        {
            return GenerateDefaultKey(base.GetDefaultKey(), "Validation");
        }

        
        public GymLoggerValidationException AddError(string field, string error)
        {
            this.FieldErrors.Add(field, error);
            return this;
        }
    }
}
