using SharedKernel.Exceptions;
using System.Net;
using System.Text.Json.Serialization;

namespace SharedKernel.Extensions;

public static class JSONResponse
{
    public static async Task<ResponseModel<T>> ToResponseAsync<T>(this Task<T> task, bool status = true, int statusCode = 200, string message = "", object data = null)
    {
        var result = await task;
        return new ResponseModel<T>
        {
            Status = status,
            StatusCode = statusCode,
            Message = message,
            Data = result
        };
    }

    public static async Task<ResponseModel> ToResponseAsync(this Task task, bool status = true, int statusCode = 200, string message = "")
    {
        await task;
        return new ResponseModel
        {
            Status = status,
            StatusCode = statusCode,
            Message = message,
        };
    }
}

public class ResponseModel<T>
{
    public bool Status { get; set; } = true;
    public int StatusCode { get; set; } = 200;
    public string Message { get; set; } = string.Empty;
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public T Data { get; set; }
}

public class ResponseModel
{
    public bool Status { get; set; } = true;
    public int StatusCode { get; set; } = 200;
    public string Message { get; set; } = string.Empty;
}
