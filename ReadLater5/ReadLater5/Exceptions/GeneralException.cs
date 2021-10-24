using System;

namespace ReadLater5.Exceptions
{
    public class GeneralException : Exception
    {
        public GeneralException(string message)
        : base(message)
        {
        }
    }
}
