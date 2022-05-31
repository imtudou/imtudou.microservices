using IdentityModel.Client;

using System;
using System.Net.Http;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var client = new HttpClient();
            // 1. 获取 accesstoken
            var tokenResponse = client.RequestPasswordTokenAsync(new PasswordTokenRequest
            {
                Address = "http://localhost:5000/connect/token",
                ClientId = "ResourceOwnerPassword.Client",
                ClientSecret = "secret",
                UserName = "zhangsan",
                Password = "password",
                Scope = "api"
            }).Result;

            if (!tokenResponse.IsError)
            {
                Console.WriteLine(tokenResponse.AccessToken);
            }

            // 2. 获取token 调用api
            var apiClient = new HttpClient();
            apiClient.SetBearerToken(tokenResponse.AccessToken);
            var response = apiClient.GetAsync("http://localhost:5001/identity").Result;
            if (response.IsSuccessStatusCode)
            {
                var content = response.Content.ReadAsStringAsync().Result;
                Console.WriteLine(content);
            }

            Console.ReadKey();
        }
    }
}
