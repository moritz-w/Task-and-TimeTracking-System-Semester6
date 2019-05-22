using System.Collections.Generic;

namespace TaskAndTimeTracking.Controller.DTO.response
{
    public class ResponseDTO<T>
    {
        public string ErrorMessage { get; set; }

        public bool Success { get; set; }
        
        public T Data { get; set; }

        public ResponseDTO()
        {
            Success = true;
        }

        /**
         * Constructor that might be required when additionally to the error message
         * some data should be added (e.g. a stacktrace). Otherwise the simple
         * one argument constructors can be used. 
         */
        public ResponseDTO(T data, bool success, string errorMessage)
        {
            Data = data;
            Success = success;
            ErrorMessage = errorMessage;
        }

        public ResponseDTO(T data)
        {
            Data = data;
            Success = true;
        }

        public ResponseDTO(string errorMessage)
        {
            ErrorMessage = errorMessage;
            Success = false;
        }
        
    }
}