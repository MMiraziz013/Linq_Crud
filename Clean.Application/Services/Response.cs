using System.Net;

namespace Clean.Application.Services;

public class Response<T>
{
    public int StatusCode { get; set; }
    public List<string> Descriptions { get; set; }
    public T? Data { get; set; }

    //error
    public Response(HttpStatusCode statusCode, string message)
    {
        StatusCode = (int)statusCode;
    }
        
    public Response(T data)
    {
        StatusCode = 200;
        Data = data;
    }

    public Response()
    {
        
    }
}