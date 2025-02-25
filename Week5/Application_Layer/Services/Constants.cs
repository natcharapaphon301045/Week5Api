﻿namespace Week5.Application_Layer.Services
{
    public static class ResponseMessages
    {   
        public const string StudentGetSuccess = "Students retrieved successfully";
        public const string StudentPostSuccess = "Student created successfully";
        public const string StudentGetNotFound = "Student not found";
    }
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public required string Message { get; set; }
        public T? Data { get; set; }
    }
}
