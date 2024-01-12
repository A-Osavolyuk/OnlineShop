using OnlineShop.Domain.Enuns;

namespace OnlineShop.Domain.DTOs
{
    public class RequestDto
    {
        public ApiMethods Method { get; set; } = ApiMethods.GET;
        public string Url { get; set; } = string.Empty;
        public object? Data { get; set; }
        public string AccessToken { get; set; } = string.Empty;
    }
}
