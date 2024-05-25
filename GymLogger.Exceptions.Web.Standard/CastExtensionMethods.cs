using Microsoft.AspNetCore.Mvc;

namespace GymLogger.Exceptions.Web
{
    public static class CastExtensionMethods
    {
        public static GymLoggerHttpException AsHttpException(this GymLoggerException gymLoggerException) =>
            ExceptionsHelper.ToGymLoggerHttpException(gymLoggerException);

        public static IActionResult AsActionResult(this GymLoggerHttpException gymLoggerHttpException) =>
            new GymLoggerHttpExceptionResult(gymLoggerHttpException);
    }
}
