using Microsoft.AspNetCore.Mvc;

namespace CardGame.ExceptionService
{
    /// <summary>
    /// Allows sending custom HTTP status codes by overriding <see cref="ObjectResult"/>
    /// </summary>
    public class ExceptionObjectResult : ObjectResult
    {
        public ExceptionObjectResult(ProblemDetails value) : base(value)
        {
            StatusCode = value.Status;
        }
    }
}
