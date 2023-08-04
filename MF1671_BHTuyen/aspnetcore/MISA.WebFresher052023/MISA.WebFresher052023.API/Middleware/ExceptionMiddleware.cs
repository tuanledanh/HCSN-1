using MISA.WebFresher052023.Domain.Exceptions;

namespace MISA.WebFresher052023.API.Middleware
{
    public class ExceptionMiddleware
    {
        #region Fields
        /// <summary>
        /// 
        /// </summary>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        private readonly RequestDelegate _next;
        #endregion

        #region Methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="next"></param>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception exception)
            {
                await HandleExceptionAsync(httpContext, exception);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="httpContext"></param>
        /// <param name="exception"></param>
        /// <returns></returns>
        private static async Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
        {
            Console.WriteLine(exception);
            httpContext.Response.ContentType = "application/json";

            switch (exception)
            {
                case NotFoundException:
                    httpContext.Response.StatusCode = StatusCodes.Status404NotFound;
                    await httpContext.Response.WriteAsync(
                        text: new BaseException()
                        {
                            ErrorCode = StatusCodes.Status404NotFound,
                            UserMessage = "Không tìm thấy tài nguyên",
                            DevMessage = exception.Message,
                            TraceId = httpContext.TraceIdentifier,
                            MoreInfo = exception.HelpLink
                        }.ToString() ?? ""
                     );
                    break;
                case ConflictException:
                    httpContext.Response.StatusCode = StatusCodes.Status409Conflict;
                    await httpContext.Response.WriteAsync(text: new BaseException()
                    {
                        ErrorCode = StatusCodes.Status409Conflict,
                        UserMessage = ((ConflictException)exception).Message,
                        DevMessage = exception.Message,
                        TraceId = httpContext.TraceIdentifier,
                        MoreInfo = exception.HelpLink
                    }.ToString() ?? "");
                    break;

                default:
                    httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    await httpContext.Response.WriteAsync(text: new BaseException()
                    {
                        ErrorCode = httpContext.Response.StatusCode,
                        UserMessage = "Lỗi hệ thống",
#if DEBUG
                        DevMessage = exception.Message,
#else
                    DevMessage = "",
#endif
                        TraceId = httpContext.TraceIdentifier,
                        MoreInfo = exception.HelpLink
                    }.ToString() ?? "");
                    break;
            }
            #endregion
        }
    }
}
