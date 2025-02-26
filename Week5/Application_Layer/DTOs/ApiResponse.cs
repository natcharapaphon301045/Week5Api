namespace Week5.Application_Layer.DTOs
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public T? Data { get; set; }

        public ApiResponse(bool success, string message, T? data = default) => 
            (Success, Message, Data)=(success, message, data);
        

        public static ApiResponse<T> SuccessResponse(T data, string message = "Success") =>
            new (true, message, data);

        public static ApiResponse<T> FailResponse(string message = "Failed") =>
            new (false, message);
    }
}
