using System.Net;

namespace ReservationSys.Application.Shared.Responses;

public class BaseResponse<T>
{
    public T? Data { get; set; }
    public string Message { get; set; } = string.Empty;
    public bool Success { get; set; }
    public HttpStatusCode StatusCode { get; set; }

    public BaseResponse(HttpStatusCode statusCode)
    {
        Success = true;
        StatusCode = statusCode;
    }
    public BaseResponse(string message, bool isSucces, HttpStatusCode statusCode)
    {
        Message = message;
        Success = isSucces;
        StatusCode = statusCode;
    }
    public BaseResponse(T? data, bool isSucces, HttpStatusCode statusCode)
    {
        Data = data;
        Success = isSucces;
        StatusCode = statusCode;
    }
    public BaseResponse(string message, HttpStatusCode statusCode)
    {
        Message = message;
        StatusCode = statusCode;
        Success = false;
    }
    public BaseResponse(string message, T? data, bool isSucces, HttpStatusCode statusCode)
    {
        Message = message;
        StatusCode = statusCode;
        Data = data;
        Success = isSucces;
    }
}
