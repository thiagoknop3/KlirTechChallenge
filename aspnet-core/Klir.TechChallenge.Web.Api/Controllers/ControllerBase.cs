using Klir.TechChallenge.Infra.CrossCutting;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Klir.TechChallenge.Web.Api.Controllers
{
    public abstract class ControllerBase : Microsoft.AspNetCore.Mvc.ControllerBase
    {
        protected ActionResult MapToActionResult<T>(CommomResponse<T> result)
        {
            return HttpUtilities.MapToActionResult<T>(this, result);
        }

        protected ActionResult MapToActionResult(CommomResponse result)
        {
            return HttpUtilities.MapToActionResult(this, result);
        }
    }

    public static class HttpUtilities
    {
        public static ActionResult MapToActionResult<T>(this ControllerBase controller, CommomResponse<T> result) =>
        result.Success ? new OkObjectResult(result.Data) : MapToErrorActionResult(controller, result);
        public static ActionResult MapToActionResult(this ControllerBase controller, CommomResponse result) =>
            result.Success ? new OkResult() : MapToErrorActionResult(controller, result);

        private static ActionResult MapToErrorActionResult(ControllerBase controller, CommomResponse result)
        {
            return result.FailureDetails switch {

                FailureDetails.Exception => controller.StatusCode((int)HttpStatusCode.InternalServerError, new ProblemDetails {
                    Title = "Unexpected error",
                    Detail = result.ErrorMessage
                }),
                FailureDetails.NotFound => controller.NotFound(new ProblemDetails {
                    Title = "Resource not found",
                    Detail = result.ErrorMessage
                }),
                FailureDetails.ArgumentIsEmpty => controller.BadRequest(new ProblemDetails {
                    Title = "Argument is empty",
                    Detail = result.ErrorMessage
                }),
                FailureDetails.ValidationError => controller.BadRequest(new ProblemDetails {
                    Title = "Validation Failed",
                    Detail = result.ErrorMessage
                }),
                _ => controller.StatusCode((int)HttpStatusCode.InternalServerError, new ProblemDetails {
                    Title = "Unexpected error",
                    Detail = result.ErrorMessage
                }),
            };
        }
    }
}

