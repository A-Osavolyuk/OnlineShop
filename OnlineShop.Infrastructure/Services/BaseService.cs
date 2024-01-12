using Newtonsoft.Json;
using OnlineShop.Application.Interfaces;
using OnlineShop.Domain.DTOs;
using OnlineShop.Domain.Enuns;
using System.Net;
using System.Text;

namespace OnlineShop.Infrastructure.Services
{
    public class BaseService : IBaseService
    {
        private readonly HttpClient httpClient;

        public BaseService(IHttpClientFactory clientFactory)
        {
            httpClient = clientFactory.CreateClient("OnlineShop.WebUI");
        }

        public async Task<ResponseDto?> SendAsync(RequestDto request, bool withBearer = true)
        {
            try
            {
                HttpRequestMessage message = new();

                message.Headers.Add("Accept", "application/json");

                message.RequestUri = new Uri(request.Url);

                if (withBearer)
                {
                    //var token = tokenProvider.GetToken();
                    //message.Headers.Add("Authorization", $"Bearer {token}");
                }

                if (request.Data is not null)
                {
                    message.Content = new StringContent(JsonConvert.SerializeObject(request.Data),
                        Encoding.UTF8, "application/json");
                }

                HttpResponseMessage httpResponse = default!;

                switch (request.Method)
                {
                    case ApiMethods.POST:
                        message.Method = HttpMethod.Post;
                        break;
                    case ApiMethods.DELETE:
                        message.Method = HttpMethod.Delete;
                        break;
                    case ApiMethods.PUT:
                        message.Method = HttpMethod.Put;
                        break;
                    default:
                        message.Method = HttpMethod.Get;
                        break;
                }

                httpResponse = await httpClient.SendAsync(message);

                switch (httpResponse.StatusCode)
                {
                    case HttpStatusCode.NotFound:
                        return new() { IsSucceeded = false, Message = "Not Found" };
                    case HttpStatusCode.Forbidden:
                        return new() { IsSucceeded = false, Message = "Forbidden" };
                    case HttpStatusCode.Unauthorized:
                        return new() { IsSucceeded = false, Message = "Unauthorized" };
                    case HttpStatusCode.InternalServerError:
                        return new() { IsSucceeded = false, Message = "Internal Server Error" };
                    default:
                        var apiContent = await httpResponse.Content.ReadAsStringAsync();
                        return JsonConvert.DeserializeObject<ResponseDto>(apiContent);
                }
            }
            catch (Exception ex)
            {
                return new ResponseDto()
                {
                    IsSucceeded = false,
                    Message = ex.Message.ToString(),
                };
            }
        }
    }
}
