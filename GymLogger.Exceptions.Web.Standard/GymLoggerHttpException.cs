namespace GymLogger.Exceptions.Web
{
    public class GymLoggerHttpException
    {

        public string Error { get; set; }
        public string Message { get; set; }
        public int StatusCode { get; set; }
        public GymLoggerException Exception { get; set; }
        public GymLoggerHttpException(string error, int httpCode, string errorDescription)
        {
            this.Message = errorDescription;
            this.StatusCode = httpCode;
            this.Error = error;
        }
                
        public GymLoggerHttpException(string error, string errorDescription, GymLoggerException exception)
        {
            this.Message = errorDescription;
            this.Error = error;
            this.Exception = exception;
        }
        
        public GymLoggerHttpException(string error, int httpCode, string errorDescription, GymLoggerException exception)
        {
            this.Message = errorDescription;
            this.StatusCode = httpCode;
            this.Error = error;
            this.Exception = exception;
        }
    }
}
