using Newtonsoft.Json;

namespace FoodOrderAPI.Middleware
{
    public class ApiExceptionHandlerMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<ApiExceptionHandlerMiddleware> logger;

        public ApiExceptionHandlerMiddleware(RequestDelegate next, ILogger<ApiExceptionHandlerMiddleware> logger)
        {
            this.logger = logger;
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await next(httpContext);
            }
            catch (Exception ex)
            {
                var apiException = ex is ApiException ? ex as ApiException : null;
                if ((apiException != null && logger.IsEnabled(apiException.LogLevel)) || apiException == null)
                {
                    logger.LogError($"Something went wrong: {ex}.");
                }

                await HandleExceptionAsync(httpContext, ex);
                return;
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var apiException = exception is ApiException ? (ApiException)exception : null;
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = apiException != null ?
                apiException.Error.HttpStatusCode : StatusCodes.Status500InternalServerError;

            return context.Response.WriteAsync(JsonConvert.SerializeObject(
                apiException != null ? apiException.Error :
                    Error.Create(context.Response.StatusCode, exception.Message)
            ));
        }
    }
}