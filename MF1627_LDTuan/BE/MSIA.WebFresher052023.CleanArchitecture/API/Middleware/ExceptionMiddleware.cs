using Domain.Exceptions;

namespace API.Middleware
{
    public class ExceptionMiddleware
    {
        #region Fields
        private readonly RequestDelegate _next;
        #endregion

        #region Constructor
        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        #endregion

        #region Methods
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }
        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";

            switch (exception)
            {
                case NotFoundException notFoundException:
                    context.Response.StatusCode = StatusCodes.Status404NotFound;
                    await context.Response.WriteAsync(new BaseException()
                    {
                        ErrorCode = notFoundException.ErrorCode,
                        UserMessage = "Không tìm thấy tài nguyên",
                        DevMessage = exception.Message,
                        TraceId = context.TraceIdentifier,
                        MoreInfo = exception.HelpLink
                    }.ToString() ?? "");
                    break;
                // Trùng mã
                case DuplicateExeption:
                    context.Response.StatusCode = StatusCodes.Status409Conflict;

                    await context.Response.WriteAsync(
                            new BaseException()
                            {
                                ErrorCode = StatusCodes.Status409Conflict,
                                UserMessage = exception.Message,
                                DevMessage = exception.Message,
                                TraceId = context.TraceIdentifier,
                                MoreInfo = exception.HelpLink
                            }.ToString() ?? "");
                    break;

                default:
                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    await context.Response.WriteAsync(new BaseException()
                    {
                        ErrorCode = StatusCodes.Status500InternalServerError,
                        UserMessage = "Lỗi hệ thống",
                        DevMessage = exception.Message,
                        TraceId = context.TraceIdentifier,
                        MoreInfo = exception.HelpLink
                    }.ToString() ?? "");
                    break;
            }
        }
        #endregion

    }
}
