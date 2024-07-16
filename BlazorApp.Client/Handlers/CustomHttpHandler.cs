using Blazored.LocalStorage;
using System.Linq;

namespace BlazorApp.Client.Handlers
{
    public class CustomHttpHandler : DelegatingHandler
    {
        private readonly ILocalStorageService localStorageService;

        public CustomHttpHandler(ILocalStorageService localStorageService)
        {
            this.localStorageService = localStorageService;
        }

        protected async override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (request.RequestUri!.AbsolutePath.ToLower().Contains("user/Login") || request.RequestUri!.AbsolutePath.ToLower().Contains("register"))
                return await base.SendAsync(request, cancellationToken);

            var token = await localStorageService.GetItemAsync<string>("JwtToken", cancellationToken = default);

            //Console.WriteLine($"Retrieved Token: {token}");

            if (token is not null && !request.Headers.Contains("Authorization"))
                //request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            request.Headers.Add("Authorization", $"Bearer {token}");

            return await base.SendAsync(request, cancellationToken);
        }
    }
}
