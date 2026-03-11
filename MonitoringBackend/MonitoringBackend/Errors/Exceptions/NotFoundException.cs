using System.Net;

namespace InfotecsBackend.Errors.Exceptions;

public class NotFoundException(string message) : BaseException(message, HttpStatusCode.NotFound);

