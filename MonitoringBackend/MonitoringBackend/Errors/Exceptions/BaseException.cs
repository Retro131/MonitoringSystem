using System.Net;

#pragma warning disable CS1591

namespace InfotecsBackend.Errors.Exceptions;

public class BaseException(string message, HttpStatusCode httpStatusCode = HttpStatusCode.InternalServerError) : Exception(message)
{
    public HttpStatusCode HttpStatusCode { get; } = httpStatusCode;
}