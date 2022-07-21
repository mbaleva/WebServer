namespace WebServer.HTTP.Enums
{
    public enum HttpStatusCode
    {
        OK = 200,
        MovedPermanently = 301,
        FOUND = 302,
        TemporaryRedirect = 307,
        Unauthorized = 401,
        Forbidden = 403,
        NotFound = 404,
        InternalServerError = 500,
        BadGateway = 502
    }
}
