using Sayollo.Services;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Sayollo.Core
{
    public class StoreManager
    {
        #region --- Members ---
        private const string LOG_FORMAT = "Sayollo.AdsManager.{0}: {1}";
        private const string URL = "https://6u3td6zfza.execute-api.us-east-2.amazonaws.com/prod/v1/gcom/ad";

        private readonly JsonService jsonService;

        private ProductData product;
        #endregion

        #region --- Init ---
        [RuntimeInitializeOnLoadMethod]
        private static void OnRuntimeMethodLoad()
        {
            new StoreManager();
        }

        private StoreManager()
        {
            SingleManager.Register(this);
            jsonService = new JsonService();
            GetProduct();
        }
        #endregion

        #region --- Get Product ---
        private async void GetProduct()
        {
            product = await Task.Run(AskServerForProduct);
        }

        private ProductData AskServerForProduct()
        {
            HttpClient httpClient = new HttpClient();
            try
            {
                StringContent content = new StringContent("{}", Encoding.UTF8, "application/json");
                HttpResponseMessage result = httpClient.PostAsync(URL, content).Result;

                string responseFromServer = result.Content.ReadAsStringAsync().Result;

                httpClient.Dispose();
                httpClient = null;

                ProductData vastData = jsonService.JsonToObject<ProductData>(responseFromServer);
                return vastData;
            }
            catch (Exception e)
            {
                Debug.LogErrorFormat(LOG_FORMAT, "AskServerForProduct", e.Message);
                if (httpClient != null)
                    httpClient.Dispose();
                return null;
            }
        }
        #endregion
    }
}
