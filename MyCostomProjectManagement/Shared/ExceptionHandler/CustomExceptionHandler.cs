using BackEnd.Data.DB;
using Microsoft.EntityFrameworkCore;

namespace MyCostomProjectManagement.Shared.ExceptionHandler
{
    public class CustomExceptionHandler
    {
        private readonly RequestDelegate _next;
        private readonly IDbContextFactory<Context> _dbContextFactory;
        private readonly ILogger<CustomExceptionHandler> _logger;

        public CustomExceptionHandler(
            RequestDelegate next,
            ILogger<CustomExceptionHandler> logger, IDbContextFactory<Context> dbContextFactory)
        {
            _next = next;
            _dbContextFactory = dbContextFactory;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                await using var db = await _dbContextFactory.CreateDbContextAsync();
                db.Logs.Add(new BackEnd.Data.DB.Models.Logs(ex.ToString(), ex.Message));
                await db.SaveChangesAsync();
                await HandleExceptionAsync(context, ex);
            }
        }

        private static async Task HandleExceptionAsync(
            HttpContext context,
            Exception exception)
        {
            context.Response.ContentType = "application/json";

            var statusCode = exception switch
            {
                ArgumentException => StatusCodes.Status400BadRequest,
                //ArgumentNullException => StatusCodes.Status400BadRequest,
                FormatException => StatusCodes.Status400BadRequest,

                UnauthorizedAccessException => StatusCodes.Status401Unauthorized,

                KeyNotFoundException => StatusCodes.Status404NotFound,
                FileNotFoundException => StatusCodes.Status404NotFound,
                DirectoryNotFoundException => StatusCodes.Status404NotFound,

                NotImplementedException => StatusCodes.Status501NotImplemented,

                TimeoutException => StatusCodes.Status408RequestTimeout,

                InvalidOperationException => StatusCodes.Status409Conflict,

                _ => StatusCodes.Status500InternalServerError
            };

            context.Response.StatusCode = statusCode;
            //البته میشه به OperationResult هم مپ کردش
            var response = new
            {
                StatusCode = statusCode,
                Exception = exception.GetType().Name,
                Message = exception.Message
            };

            await context.Response.WriteAsJsonAsync(response);
        }
    }
}
