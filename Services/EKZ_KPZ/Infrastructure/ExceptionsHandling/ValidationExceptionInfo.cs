using System.Collections.Generic;

namespace EKZ_KPZ.Infrastructure.ExceptionsHandling
{
    public class ValidationExceptionInfo : ExceptionInfo
    {
        public IDictionary<string, string[]> Errors { get; set; }
    }
}
