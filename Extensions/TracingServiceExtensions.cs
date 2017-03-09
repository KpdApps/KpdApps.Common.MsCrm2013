using System;
using Microsoft.Xrm.Sdk;

namespace KpdApps.Common.MsCrm2013.Extensions
{
    public static class TracingServiceExtensions
    {
        public static void TraceError(this ITracingService tracingService, Exception exception)
        {
            tracingService.Trace($"[Error] {exception.Source}:{exception.Message}\r\n{exception.StackTrace}\r\n");
            if (exception.InnerException != null)
                tracingService.TraceError(exception.InnerException);
        }

        public static void TraceWarning(this ITracingService tracingService, string format, params object[] args)
        {
            tracingService.Trace($"[Warning] {string.Format(format, args)}");
        }

        public static void TraceInfo(this ITracingService tracingService, string format, params object[] args)
        {
            tracingService.Trace($"[Info] {string.Format(format, args)}");
        }
    }
}
