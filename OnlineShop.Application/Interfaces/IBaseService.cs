using OnlineShop.Domain.DTOs;

namespace OnlineShop.Application.Interfaces
{
    public interface IBaseService
    {
        Task<ResponseDto?> SendAsync(RequestDto request, bool withBearer = true);
    }
}
