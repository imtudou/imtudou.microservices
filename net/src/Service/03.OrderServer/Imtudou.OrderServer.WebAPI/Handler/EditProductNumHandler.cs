using Imtudou.OrderServer.WebAPI.Handler.Command;

using MediatR;

using Newtonsoft.Json;

using NPOI.SS.Formula.Functions;

using System.Net;
using System.Net.Http.Headers;
using System.Text;

namespace Imtudou.OrderServer.WebAPI.Handler
{
    public class EditProductNumHandler : INotificationHandler<EditProductNumCommand>
    {

        private readonly IHttpClientFactory httpClientFactory;

        public EditProductNumHandler(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }

        Task INotificationHandler<EditProductNumCommand>.Handle(EditProductNumCommand notification, CancellationToken cancellationToken)
        {
            try
            {
                string url = "http://localhost:2277/api/Product/EditProductNum";
                var input = new { ProductID = notification.ProductID };
                var result = this.PostAsync<bool>(url, input);
            }
            catch (Exception ex)
            {


            }
            finally
            { 
            
            }
           
            return Task.CompletedTask;
        }

        public async Task<T> PostAsync<T>(string url, object request)
        {
            var client = httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            StringContent requestContent = null;
            if (request != null)
            {
                var cc = JsonConvert.SerializeObject(request, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore, DateFormatString = default });
                requestContent = new StringContent(cc, Encoding.UTF8, "application/json");
            }

            var httpResult =  await client.PostAsync(url, requestContent);                    
            if (httpResult.StatusCode == HttpStatusCode.OK)
            {
                string resultStr = httpResult.Content.ReadAsStringAsync().Result;
                var result = JsonConvert.DeserializeObject<T>(resultStr);
                return result;
            }
            return default(T);
        }

        public async Task<bool> GetCC()
        {

            //string url = "http://localhost:2277/api/Product/EditProductNum";
            //var input = new { ProductID = "" };
            //var result = this.PostAsync<bool>(url, input);
           return cc();
        }

        public bool cc()
        {
            return true;
        }






    }
}
