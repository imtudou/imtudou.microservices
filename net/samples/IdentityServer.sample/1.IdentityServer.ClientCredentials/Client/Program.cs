using IdentityModel.Client;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            // 从元数据中发现端点

            var client = new HttpClient();
            var disco = client.GetAsync("http://localhost:5001/identity").Result;
            if (disco.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {

                // 1. 获取 accesstoken
                var tokenResponse = client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
                {
                    Address = "http://localhost:5000/connect/token",
                    ClientId = "ClientCredentials.Client",
                    ClientSecret = "secret"
                }).Result;

                if (!tokenResponse.IsError && !string.IsNullOrEmpty(tokenResponse.AccessToken))
                {
                    // 2. 获取token 调用api
                    var apiClient = new HttpClient();
                    apiClient.SetBearerToken(tokenResponse.AccessToken);
                    var response = apiClient.GetAsync("http://localhost:5001/identity").Result;
                    if(response.IsSuccessStatusCode)
                    {
                        var content = response.Content.ReadAsStringAsync().Result;
                        Console.WriteLine(content);
                    }

                }


            }



            Console.ReadKey();
        }
    }
}
