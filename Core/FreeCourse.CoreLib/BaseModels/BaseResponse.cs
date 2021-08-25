using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace FreeCourse.CoreLib.BaseModels
{
    public class BaseResponse<T>
    {
        public T Data { get; private set; }

        [JsonIgnore]
        public int StatusCode { get; private set; }
        [JsonIgnore]
        public bool IsSuccessful { get; private set; }

        public List<string> Infos { get; set; }

        public static BaseResponse<T> Success(T data, int statusCode)
        {
            return new BaseResponse<T> {Data = data, StatusCode = statusCode, IsSuccessful = true};
        }

        public static BaseResponse<T> Success(int statusCode)
        {
            return new BaseResponse<T> {Data = default(T), StatusCode = statusCode, IsSuccessful = true};
        }

        public static BaseResponse<T> Error(List<string> errors, int statusCode)
        {
            return new BaseResponse<T>
            {
                Infos = errors,
                StatusCode = statusCode,
                IsSuccessful = false
            };
        }
    }
}