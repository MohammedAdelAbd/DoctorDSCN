using Blazor.SubtleCrypto;
using BlazorApp.Client;
using BlazorApp.Client.Handlers;
using BlazorApp.Client.Provider;
using BlazorApp.Client.Services;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace BlazorApp.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");


            builder.Services.AddHttpClient<IUserService, UserService>(
                client => client.BaseAddress = new Uri("https://localhost:7180/")
                );
            builder.Services.AddHttpClient<IUserInfoService, UserInfoService>(
                client => client.BaseAddress = new Uri("https://localhost:7180/")
                );
            builder.Services.AddHttpClient<ICustomerService, CustomerService>(
                client => client.BaseAddress = new Uri("https://localhost:7180/")
                );
            builder.Services.AddHttpClient<IUserProductService, ProductService>(
                client => client.BaseAddress = new Uri("https://localhost:7180/")
                );
            builder.Services.AddHttpClient<ICustomerProductService, CustomerProductService>(
                client => client.BaseAddress = new Uri("https://localhost:7180/")
                );
                

            //builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7180/") });

            //Addedd Microsoft.Extention.Http
            builder.Services.AddHttpClient("MyApi", options =>
            {
                options.BaseAddress = new Uri("https://localhost:7180/");
            }).AddHttpMessageHandler<CustomHttpHandler>();


            builder.Services.AddSubtleCrypto(opt =>
                                opt.Key = "ELE9xOyAyJHCsIPLMbbZHQ7pVy7WUlvZ60y5WkKDGMSw5xh5IM54kUPlycKmHF9VGtYUilglL8iePLwr");

            builder.Services.AddBlazorBootstrap();
            builder.Services.AddBlazoredLocalStorage();

            builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
             
            builder.Services.AddAuthorizationCore();
            builder.Services.AddScoped<CustomHttpHandler>();


            await builder.Build().RunAsync();
        }
    }
}