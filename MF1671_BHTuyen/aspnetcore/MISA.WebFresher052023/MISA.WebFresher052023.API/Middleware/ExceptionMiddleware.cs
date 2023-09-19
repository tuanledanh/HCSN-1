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

            var baseException = new BaseException()
            {
                UserMessage = exception.Message,
                TraceId = httpContext.TraceIdentifier,
                HelpLink = exception.HelpLink,
#if DEBUG
                DevMessage = exception.Message,
#else
                DevMessage = ''
#endif
            };
            switch (exception)
            {
                case UserException:
                    httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                    baseException.ErrorCode = StatusCodes.Status400BadRequest;
                    baseException.MoreInfo = ((UserException)exception).MoreInfo;
                    break;
                case NotFoundException:
                    httpContext.Response.StatusCode = StatusCodes.Status404NotFound;
                    baseException.ErrorCode = StatusCodes.Status404NotFound;
                    break;
                case ConflictException:
                    httpContext.Response.StatusCode = StatusCodes.Status409Conflict;
                    baseException.ErrorCode = StatusCodes.Status409Conflict;
                    break;
                case Exception:
                    httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                    baseException.ErrorCode = StatusCodes.Status400BadRequest;
                    break;
                default:
                    httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    baseException.ErrorCode = StatusCodes.Status500InternalServerError;
                    break;
            }

            await httpContext.Response.WriteAsync(baseException.ToString());
        }
        #endregion
    }
}
