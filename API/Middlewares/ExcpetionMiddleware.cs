using SharedKernel.Exceptions;
using SharedKernel.Extensions;
using System.Net;

namespace API.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly IConfiguration _config;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IConfiguration config)
        {
            _next = next;
            _logger = logger;
            _config = config;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (CustomException ex)
            {
                context.Response.StatusCode = (int)ex.StatusCode;
                _logger.LogError($"Error Message: {ex.Message}\n Error Detail: {ex}");

                await context.Response.WriteAsJsonAsync(new ResponseModel()
                {
                    Status = false,
                    StatusCode = context.Response.StatusCode,
                    Message = ex.Message
                });
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError; ;
                _logger.LogError($"Error Message: {ex.Message}\n Error Detail: {ex}");

                await context.Response.WriteAsJsonAsync(new ResponseModel()
                {
                    Status = false,
                    StatusCode = context.Response.StatusCode,
                    Message = "Internal server error."
                });
            }
        }
    }
}