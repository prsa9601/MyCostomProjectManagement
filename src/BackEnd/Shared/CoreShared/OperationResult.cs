namespace BackEnd.Shared.CoreShared
{
    public class OperationResult<TData>
    {
        public const string SuccessMessage = "عملیات با موفقیت انجام شد";
        public const string ErrorMessage = "عملیات با شکست مواجه شد";
        public const string BadRequestMessage = "Bad Request";

        public string Message { get; set; }
        public string Title { get; set; } = null;
        public OperationResultStatus Status { get; set; }
        public TData Data { get; set; }
        public static OperationResult<TData> Success(TData data)
        {
            return new OperationResult<TData>()
            {
                Status = OperationResultStatus.Success,
                Message = SuccessMessage,
                Data = data,
            };
        }
        public static OperationResult<TData> NotFound()
        {
            return new OperationResult<TData>()
            {
                Status = OperationResultStatus.NotFound,
                Title = "NotFound",
                Data = default(TData),
            };
        }
        public static OperationResult<TData> NotFound(string message)
        {
            return new OperationResult<TData>()
            {
                Status = OperationResultStatus.NotFound,
                Title = "NotFound",
                Message = message,
                Data = default(TData),
            };
        }
        public static OperationResult<TData> NotFound(string message, TData? data)
        {
            return new OperationResult<TData>()
            {
                Status = OperationResultStatus.NotFound,
                Title = "NotFound",
                Message = message,
                Data = data ?? default(TData),
            };
        }
        public static OperationResult<TData> BadRequest(string? message, TData? data)
        {
            return new OperationResult<TData>()
            {
                Status = OperationResultStatus.BadRequest,
                Title = "BadRequest",
                Message = String.IsNullOrWhiteSpace(message) ? BadRequestMessage : message,
                Data = data ?? default(TData),
            };
        }
        public static OperationResult<TData> BadRequest()
        {
            return new OperationResult<TData>()
            {
                Status = OperationResultStatus.BadRequest,
                Title = "BadRequest",
                Message = BadRequestMessage,
                Data = default(TData),
            };
        }
        public static OperationResult<TData> NotFound(TData data)
        {
            return new OperationResult<TData>()
            {
                Status = OperationResultStatus.NotFound,
                Title = "NotFound",
                Data = data,
            };
        }
        public static OperationResult<TData> NotFound(TData data, string message)
        {
            return new OperationResult<TData>()
            {
                Status = OperationResultStatus.NotFound,
                Title = "اطلاعات یافت نشد",
                Message = message,
                Data = data,
            };
        }
        public static OperationResult<TData> Error(string message = ErrorMessage)
        {
            return new OperationResult<TData>()
            {
                Status = OperationResultStatus.Error,
                Title = "مشکلی در عملیات رخ داده",
                Data = default(TData),
                Message = message
            };
        }
        public static OperationResult<TData> Error()
        {
            return new OperationResult<TData>()
            {
                Status = OperationResultStatus.Error,
                Title = "مشکلی در عملیات رخ داده",
                Data = default(TData),
                Message = ErrorMessage
            };
        }
        public static OperationResult<TData> Error(TData data)
        {
            return new OperationResult<TData>()
            {
                Status = OperationResultStatus.Error,
                Title = "مشکلی در عملیات رخ داده",
                Data = data,
                Message = "خطایی رخ داده است!"
            };
        }
        public static OperationResult<TData> Error(TData data, string message)
        {
            return new OperationResult<TData>()
            {
                Status = OperationResultStatus.Error,
                Title = "مشکلی در عملیات رخ داده",
                Data = data,
                Message = message
            };
        }
    }
    public class OperationResult
    {
        public const string SuccessMessage = "عملیات با موفقیت انجام شد";
        public const string ErrorMessage = "عملیات با شکست مواجه شد";
        public const string BadRequestMessage = "Bad Request";
        public const string NotFoundMessage = "اطلاعات یافت نشد";
        public string Message { get; set; }
        public string Title { get; set; } = null;
        public OperationResultStatus Status { get; set; }

        public static OperationResult Error()
        {
            return new OperationResult()
            {
                Status = OperationResultStatus.Error,
                Message = ErrorMessage,
            };
        }
        public static OperationResult NotFound(string message)
        {
            return new OperationResult()
            {
                Status = OperationResultStatus.NotFound,
                Message = message,
            };
        }
        public static OperationResult NotFound()
        {
            return new OperationResult()
            {
                Status = OperationResultStatus.NotFound,
                Message = NotFoundMessage,
            };
        }
        public static OperationResult Error(string message)
        {
            return new OperationResult()
            {
                Status = OperationResultStatus.Error,
                Message = message,
            };
        }
        public static OperationResult Error(string message, OperationResultStatus status)
        {
            return new OperationResult()
            {
                Status = status,
                Message = message,
            };
        }
        public static OperationResult Success()
        {
            return new OperationResult()
            {
                Status = OperationResultStatus.Success,
                Message = SuccessMessage,
            };
        }
        public static OperationResult BadRequest()
        {
            return new OperationResult()
            {
                Status = OperationResultStatus.BadRequest,
                Message = BadRequestMessage,
            };
        }
        public static OperationResult BadRequest(string message)
        {
            return new OperationResult()
            {
                Status = OperationResultStatus.BadRequest,
                Message = message,
                Title = "BadRequest",
            };
        }
        public static OperationResult Success(string message)
        {
            return new OperationResult()
            {
                Status = OperationResultStatus.Success,
                Message = message,
            };
        }
    }


    public enum OperationResultStatus
    {
        BadRequest = 400,
        Error = 10,
        Success = 200,
        NotFound = 404
    }
}
