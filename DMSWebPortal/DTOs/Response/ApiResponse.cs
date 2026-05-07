namespace DMSWebPortal.DTOs.Response
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public T? Data { get; set; }
        public int StatusCode { get; set; }
        public string? ErrorCode { get; set; }
        public Dictionary<string, string[]>? Errors { get; set; } // field-level validation errors

        // ────────────────────────────────────────────────
        // Success factories
        // ────────────────────────────────────────────────

        public static ApiResponse<T> Ok(T data, string? message = "Success")
            => new() { Success = true, StatusCode = 200, Message = message, Data = data };

        public static ApiResponse<T> Created(T data, string? message = "Created")
            => new() { Success = true, StatusCode = 201, Message = message, Data = data };

        public static ApiResponse<T> NoContent(string? message = "No content")
            => new() { Success = true, StatusCode = 204, Message = message, Data = default };

        // ────────────────────────────────────────────────
        // Error factories – improved consistency
        // ────────────────────────────────────────────────

        /// <summary>
        /// Generic error response – most flexible
        /// </summary>
        public static ApiResponse<T> Error(
            int statusCode,
            string message,
            string? errorCode = null,
            Dictionary<string, string[]>? fieldErrors = null)
        {
            return new ApiResponse<T>
            {
                Success = false,
                StatusCode = statusCode,
                Message = message,
                ErrorCode = errorCode,
                Errors = fieldErrors,
                Data = default
            };
        }

        public static ApiResponse<T> BadRequest(
            string message = "Bad request",
            string? errorCode = ResponseCodes.BAD_REQUEST,
            Dictionary<string, string[]>? fieldErrors = null)
            => Error(400, message, errorCode ?? ResponseCodes.BAD_REQUEST, fieldErrors);

        public static ApiResponse<T> NotFound(
            string? message = "Resource not found",
            string? errorCode = ResponseCodes.NOT_FOUND)
            => Error(404, message ?? "Resource not found", errorCode);

        public static ApiResponse<T> Unauthorized(
            string? message = "Unauthorized access",
            string? errorCode = ResponseCodes.UNAUTHORIZED)
            => Error(401, message, errorCode);

        public static ApiResponse<T> Forbidden(
            string? message = "Forbidden",
            string? errorCode = "FORBIDDEN")
            => Error(403, message, errorCode);

        public static ApiResponse<T> ValidationError(
            Dictionary<string, string[]> fieldErrors,
            string? message = "One or more validation errors occurred")
            => Error(422, message, ResponseCodes.VALIDATION_ERROR, fieldErrors);

        public static ApiResponse<T> Conflict(
            string message = "Conflict occurred",
            string? errorCode = "CONFLICT")
            => Error(409, message, errorCode);

        public static ApiResponse<T> ServerError(
            string? message = "An unexpected error occurred",
            string? errorCode = ResponseCodes.SERVER_ERROR,
            Exception? ex = null)
        {
            var resp = Error(500, message ?? "Internal server error", errorCode);

#if DEBUG
            if (ex != null)
            {
                resp.Message += $" ({ex.Message})";
            }
#endif
            return resp;
        }
    }

    // Non-generic version
    public class ApiResponse
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public int StatusCode { get; set; }
        public string? ErrorCode { get; set; }

        public static ApiResponse Ok(string? message = "Success")
            => new() { Success = true, StatusCode = 200, Message = message };

        public static ApiResponse Error(
            int statusCode,
            string message,
            string? errorCode = null)
        {
            return new ApiResponse
            {
                Success = false,
                StatusCode = statusCode,
                Message = message,
                ErrorCode = errorCode
            };
        }

        public static ApiResponse BadRequest(string message, string? errorCode = ResponseCodes.BAD_REQUEST)
            => Error(400, message, errorCode);

        public static ApiResponse NotFound(string? message = null, string? errorCode = ResponseCodes.NOT_FOUND)
            => Error(404, message ?? "Not found", errorCode);

        public static ApiResponse ServerError(string? message = null, string? errorCode = ResponseCodes.SERVER_ERROR)
            => Error(500, message ?? "Internal server error", errorCode);
    }

    public static class ResponseCodes
    {
        public const string SAVE_SUCCESS = "SAVE_SUCCESS";
        public const string SAVE_FAILED = "SAVE_FAILED";
        public const string UPDATE_SUCCESS = "UPDATE_SUCCESS";
        public const string DELETE_SUCCESS = "DELETE_SUCCESS";
        public const string NOT_FOUND = "NOT_FOUND";
        public const string BAD_REQUEST = "BAD_REQUEST";
        public const string UNAUTHORIZED = "UNAUTHORIZED";
        public const string FORBIDDEN = "FORBIDDEN";
        public const string VALIDATION_ERROR = "VALIDATION_ERROR";
        public const string SERVER_ERROR = "SERVER_ERROR";
        public const string CONFLICT = "CONFLICT";
        public const string DUPLICATE = "DUPLICATE";
        public const string INVALID_STATE = "INVALID_STATE";
        // Add more domain-specific codes here
    }
}