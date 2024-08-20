using System.Text.Json.Serialization;

namespace NLayer.Data.Dto
{
    public class CustomResponseDto<T>
    {
        public T Data { get; set; }
        [JsonIgnore]
        public int StatusCode { get; set; }
        public List<string> ErrorList { get; set; }

        public static CustomResponseDto<T> Success(int statusCode, T Data)
        {
            return new CustomResponseDto<T>() { Data = Data, StatusCode = statusCode };
        }

        public static CustomResponseDto<T> Success(int statusCode)
        {
            return new CustomResponseDto<T>() { StatusCode = statusCode };
        }

        public static CustomResponseDto<T> Fail(int statusCode, List<string> errorList)
        {
            return new CustomResponseDto<T>() { StatusCode = statusCode, ErrorList = errorList };
        }

        public static CustomResponseDto<T> Fail(int statusCode, string error)
        {
            return new CustomResponseDto<T>() { StatusCode = statusCode, ErrorList = new List<string> { error } };
        }
    }
}
