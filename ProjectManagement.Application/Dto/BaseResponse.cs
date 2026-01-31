using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectManagement.Application.Dto;

public class BaseResponse<T>
{
    public bool Success { get; private set; }
    public T? Data { get; private set; }
    public string? Message { get; private set; }

    public static BaseResponse<T> SuccessResponse(T data, string? message = null)
    {
        return new BaseResponse<T>
        {
            Success = true,
            Data = data,
            Message = message
        };
    }

    public static BaseResponse<T> FailureResponse(string? message = null)
    {
        return new BaseResponse<T>
        {
            Success = false,
            Data = default,
            Message = message
        };
    }
}
