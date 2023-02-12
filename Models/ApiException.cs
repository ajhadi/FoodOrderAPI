namespace FoodOrderAPI.Models
{
    public class ApiException : Exception
    {
        public readonly Error Error;
        public override string Message { get; }
        public LogLevel LogLevel { get; }

        public ApiException(Error error, LogLevel logLevel = LogLevel.Error)
        {
            this.Error = error;
            this.Message = error.Message;
            this.LogLevel = logLevel;
        }
    }
}