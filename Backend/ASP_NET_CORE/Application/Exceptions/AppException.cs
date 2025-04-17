﻿using System.Net;

namespace Application.Exceptions;

public class AppException : Exception
{
    public HttpStatusCode StatusCode { get; }

    public AppException(string message, HttpStatusCode statusCode = HttpStatusCode.InternalServerError)
        : base(message)
    {
        StatusCode = statusCode;
    }
}