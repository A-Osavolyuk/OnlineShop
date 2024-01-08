namespace OnlineShop.ProductApi.Models.DTOs
{
    public class ResponseDto
    {
        public string Message { get; set; } = "";
        public bool IsSucceeded { get; set; } = false;
        public object? Result { get; set; }
    }
}
