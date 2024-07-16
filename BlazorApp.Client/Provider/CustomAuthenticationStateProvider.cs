using Blazor.SubtleCrypto;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using SharedLibrary.Models;
using System.Security.Claims;
using System.Text.Json;

namespace BlazorApp.Client.Provider
{
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly ILocalStorageService localStorageService;
        private readonly ICryptoService cryptoService;
        private ClaimsPrincipal _anonymous = new ClaimsPrincipal(new ClaimsIdentity());

        public CustomAuthenticationStateProvider(ILocalStorageService localStorageService , ICryptoService cryptoService)
        {
            this.localStorageService = localStorageService;
            this.cryptoService = cryptoService;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            //code without Encryption Library Crypto
            //var jwtTokenFromLocalStorage = await localStorageService.GetItemAsync<string>("JwtToken");
            //if (string.IsNullOrEmpty(jwtTokenFromLocalStorage))
            //    return new AuthenticationState(
            //        new ClaimsPrincipal(new ClaimsIdentity()));

            //return new AuthenticationState(
            //        new ClaimsPrincipal(new ClaimsIdentity(ParseClaimsFromJwt(jwtTokenFromLocalStorage), "JwtAuth")));


            //code with Encryption Library Crypto
            var encryptedUserSession = await localStorageService.GetItemAsync<Response>("UserData");
            if (encryptedUserSession is null)
                return await Task.FromResult(new AuthenticationState(_anonymous));


            var claimPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
            {
            new Claim(ClaimTypes.Name,  await cryptoService.DecryptAsync(encryptedUserSession.Username!.ToString())),
            new Claim(ClaimTypes.Role, await cryptoService.DecryptAsync(encryptedUserSession.Role!.ToString()))
            }, "JwtAuth"));

            //Call the Utility class and pass in the token to decrypt
            return await Task.FromResult(new AuthenticationState(claimPrincipal));
        }
        public void NotifyAuthenticationState()
        {
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }


        //code without Encryption Library Crypto
        //private IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
        //{
        //    var payload = jwt.Split(".")[1];
        //    var jsonBytes = ParseBase64WithoutPadding(payload);
        //    var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);
        //    return keyValuePairs!.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString()!));
        //}
        //private byte[] ParseBase64WithoutPadding(string base64)
        //{
        //    switch (base64.Length % 4)
        //    {
        //        case 2: base64 += "=="; break;
        //        case 3: base64 += "="; break;
        //    }
        //    return Convert.FromBase64String(base64);
        //}


    }
}
