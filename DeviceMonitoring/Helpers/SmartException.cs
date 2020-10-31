using System;

namespace DeviceMonitoring.Helpers
{
    public class SmartException : ApplicationException
    {
        public SmartException(string message = "Smart Exception was thrown") : base(message)
        {
        }
    }
}