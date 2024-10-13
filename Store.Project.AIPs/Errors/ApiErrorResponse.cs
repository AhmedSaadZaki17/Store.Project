namespace Store.Project.APIs.Errors
{
    public class ApiErrorResponse
    {
        public int StatusCode { get; set; }
        public string? Message { get; set; }

        public ApiErrorResponse(int statusCode, string? message = null)
        {
            StatusCode = statusCode;
            Message = message ?? GetDefaultMessageForStatusCode(statusCode);
        }

        private string? GetDefaultMessageForStatusCode(int statusCode)
        {
            switch (statusCode)
            {
                case 400:
                    return "Bad Request: The server could not understand the request due to invalid syntax.";
                case 401:
                    return "Unauthorized: Access to the requested resource requires authentication.";
                case 403:
                    return "Forbidden: You do not have permission to access this resource.";
                case 404:
                    return "Not Found: The requested resource could not be found.";
                case 405:
                    return "Method Not Allowed: The request method is not supported for the requested resource.";
                case 408:
                    return "Request Timeout: The server timed out waiting for the request.";
                case 500:
                    return "Internal Server Error: The server encountered an unexpected condition.";
                case 502:
                    return "Bad Gateway: The server received an invalid response from the upstream server.";
                case 503:
                    return "Service Unavailable: The server is currently unable to handle the request.";
                case 504:
                    return "Gateway Timeout: The server did not receive a timely response from the upstream server.";
                default:
                    return "Unknown Status Code: An unexpected status code was received.";
            }
        }
    }
}
