using System;
using System.Net;

namespace CardGame.ExceptionService
{
    public class ApiException : Exception
    {
        public HttpStatusCode Status { get; set; }
        public string Detail { get; set; }

        //Getting an exception as parameter then converting it into string to display as message
        public ApiException(Exceptions exception, HttpStatusCode status, string detail)
            : base(Enum.GetName(typeof(Exceptions), exception))
        {
            Status = status;
            Detail = detail;
        }
    }
    //TODO- Add an exceptions enum because we want message to stay constant
    public enum Exceptions
    {
        //To be used for any exception for incorrect service
        UserNotFound,
        IncorrectPassword,
        InvalidGoogleToken,
        GoogleEmailDoesNotMatch,
        UserEmailExists,
        UsernameExists
    }

}
