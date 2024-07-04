namespace GymLogger.Exceptions.Web
{
    public static class ExceptionsHelper
    {        
        public static GymLoggerHttpException ToGymLoggerHttpException(
            GymLoggerException gymLoggerException)
        {
            var httpCode = (int)System.Net.HttpStatusCode.BadRequest;

            switch (gymLoggerException)
            {
                case GymLoggerValidationException _:
                    httpCode = (int)System.Net.HttpStatusCode.BadRequest;
                    break;
                case GymLoggerForbiddenException _:
                    httpCode = (int)System.Net.HttpStatusCode.Forbidden;
                    break;
                case GymLoggerUnauthorizedException _:
                    httpCode = (int)System.Net.HttpStatusCode.Unauthorized;
                    break;
                case GymLoggerEntityNotFoundException _:
                    httpCode = (int)System.Net.HttpStatusCode.NotFound;
                    break;
                case GymLoggerException _:
                    httpCode = (int)System.Net.HttpStatusCode.InternalServerError;
                    break;
            }

            return new GymLoggerHttpException(
                gymLoggerException.Key,
                httpCode,
                gymLoggerException.Message,
                gymLoggerException);
        }
    }
}
