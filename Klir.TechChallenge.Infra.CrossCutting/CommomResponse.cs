using Microsoft.AspNetCore.Mvc;

namespace Klir.TechChallenge.Infra.CrossCutting
{
    public class CommomResponse
    {
        public string ErrorMessage { get; set; }

        public FailureDetails? FailureDetails { get; set; }

        public bool Success { get; set; }

    }

    public class CommomResponse<T> : CommomResponse
    {
        public T Data { get; set; }

        public CommomResponse() { }

        public CommomResponse(T data, bool success)
        {
            Data = data;
            Success = success;
        }
        public CommomResponse(FailureDetails details, bool success, string error)
        {
            FailureDetails = details;
            Success = success;
            ErrorMessage = error;
        }
    }

    public enum FailureDetails
    {
        Exception,
        NotFound,
        ArgumentIsEmpty,
        ValidationError
    }
}
