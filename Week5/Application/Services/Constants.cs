namespace Week5.Application.Constants
{
    public static class ResponseMessages
    {
        public const string StudentsRetrievedSuccessfully = "Students retrieved successfully";
        public const string StudentNotFound = "Student not found";
        public const string StudentAddedSuccessfully = "Student added successfully";
        public const string InvalidProfessorOrMajorID = "Invalid ProfessorID or MajorID";
        public const string StudentUpdatedSuccessfully = "Student updated successfully";
        public const string StudentDeletedSuccessfully = "Student deleted successfully";
    }
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }

        public ApiResponse(bool success, string message, T data = default)
        {
            Success = success;
            Message = message;
            Data = data;
        }
    }
}
