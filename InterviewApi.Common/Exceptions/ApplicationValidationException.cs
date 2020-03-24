using System;
using System.Collections.Generic;
using System.Text;

namespace InterviewApi.Common.Exceptions
{
    [Serializable]
    public class ApplicationValidationException : Exception
    {
        public ApplicationValidationException(string message) : base(message)
        {

        }

        public ApplicationValidationException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
