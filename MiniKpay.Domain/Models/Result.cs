namespace MiniKpay.Domain.Models
{
    public class Result<T>
    {
        public bool IsSuccess { get; set; }
        public bool IsError { get { return !IsSuccess; } }
        public bool IsValidationError { get { return Type == EnumRespType.ValidationError; } }
        public bool IsSystemError { get { return Type == EnumRespType.SystemError; } }
        private EnumRespType Type { get; set; }
        public T Data { get; set; }
        public string Message { get; set; }

        public static Result<T> Success(T data, string message = "Success")
        {
            return new Result<T>()
            {
                IsSuccess = true,
                Type = EnumRespType.Success,
                Data = data,
                Message = message
            };
        }

        public static Result<T> DeleteSuccess(string message = "Deleting Successful.")
        {
            return new Result<T>()
            {
                IsSuccess = true,
                Type = EnumRespType.Success,
                Message = message
            };
        }

        public static Result<T> ValidationError(string message, T? data = default)
        {
            return new Result<T>()
            {
                IsSuccess = true,
                Type = EnumRespType.ValidationError,
                Data = data,
                Message = message
            };
        }

        public static Result<T> SystemError(string message, T? data = default)
        {
            return new Result<T>()
            {
                IsSuccess = true,
                Type = EnumRespType.SystemError,
                Data = data,
                Message = message
            };
        }

        public enum EnumRespType
        {
            None,
            Success,
            ValidationError,
            SystemError
        }

    }
}
