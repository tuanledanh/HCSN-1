using MISA.WebFresher052023.HCSN.Domain;
using MISA.WebFresher052023.HCSN.Domain.Resource;

namespace MISA.WebFresher052023.HCSN
{
    /// <summary>
    /// Middleware xử lý các ngoại lệ trong ứng dụng
    /// </summary>
    public class ExceptionMiddleware
    {
        #region Fields
        private readonly RequestDelegate _next;
        #endregion

        #region Constructor
        /// <summary>
        /// Khởi tạo một instance mới của ExceptionMiddleware
        /// </summary>
        /// <param name="next">Delegate tiếp theo trong pipeline</param>
        /// Created by: LB.Thành (16/07/2023)
        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        #endregion

        /// <summary>
        /// Phương thức Invoke xử lý ngoại lệ
        /// </summary>
        /// <param name="context">HttpContext</param>
        /// Created by: LB.Thành (16/07/2023)
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

        /// <summary>
        /// Phương thức xử lý ngoại lệ và trả về response
        /// </summary>
        /// <param name="context">HttpContext</param>
        /// <param name="exception">Ngoại lệ</param>
        /// Created by: LB.Thành (16/07/2023)
        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            Console.WriteLine(exception);
            context.Response.ContentType = "application/json";
            int statusCode;
            BaseException baseException = new BaseException()
            {
                DevMessage = exception.Message,
                TraceId = context.TraceIdentifier,
                MoreInfo = exception.HelpLink
            };

            switch (exception)
            {
                case NotFoundException notFoundException:
                    statusCode = StatusCodes.Status404NotFound;
                    baseException.ErrorCode = notFoundException.ErrorCode;
                    baseException.UserMessage = notFoundException.Message;
                    break;

                case ConflictException conflictException:
                    statusCode = StatusCodes.Status409Conflict;
                    baseException.ErrorCode = conflictException.ErrorCode;
                    baseException.UserMessage = conflictException.Message;
                    break;

                case InvalidDataException invalidDataException:
                    statusCode = StatusCodes.Status400BadRequest;
                    baseException.ErrorCode = StatusCodes.Status400BadRequest;
                    baseException.UserMessage = invalidDataException.Message;
                    break;

                case ValidateException validateException:
                    statusCode = StatusCodes.Status406NotAcceptable;
                    baseException.ErrorCode = StatusCodes.Status406NotAcceptable;
                    baseException.UserMessage = validateException.Message;
                    break;

                default:
                    statusCode = StatusCodes.Status500InternalServerError;
                    baseException.ErrorCode = statusCode;
                    baseException.UserMessage = ErrorMessages.ServerError;
                    break;
            }

            context.Response.StatusCode = statusCode;
            await context.Response.WriteAsync(baseException.ToString() ?? "");
        }


    }
}
