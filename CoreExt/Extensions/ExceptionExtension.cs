using System;

namespace CoreExt.Extensions
{
    public static class ExceptionExtension
    {
        /// <summary>
        /// Get error message of an exception
        /// </summary>
        /// <param name="ex">The exception</param>
        /// <param name="trace">if set to <c>true</c> [trace].</param>
        /// <returns>
        /// a string of message
        /// </returns>
        public static string ToErrorMessage(this Exception ex, bool trace = true)
        {
            if (ex == null)
                return string.Empty;

            var msg = ex.Message;
            var stacktrace = trace && !string.IsNullOrWhiteSpace(ex.StackTrace) ? Environment.NewLine + ex.StackTrace : string.Empty;

            if (ex.InnerException != null)
            {
                msg += Environment.NewLine + ex.InnerException.ToErrorMessage(false);
            }

            return msg + stacktrace;
        }
    }
}
