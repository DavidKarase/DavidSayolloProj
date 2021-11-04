using Sayollo.Services;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using UnityEngine;

namespace Sayollo.Core
{
    public class StoreManager : IStoreManager
    {
        #region --- Members ---
        private const string LOG_FORMAT = "Sayollo.AdsManager.{0}: {1}";
        private const string URL = "https://6u3td6zfza.execute-api.us-east-2.amazonaws.com/prod/v1/gcom/ad";
        private const string STORE_OBJECT = "Store";

        private readonly JsonService jsonService;

        private ProductData product;
        private StoreView storeView;
        private bool isStoreOpen = false;
        #endregion

        #region --- Init ---
        [RuntimeInitializeOnLoadMethod]
        private static void OnRuntimeMethodLoad()
        {
            new StoreManager();
        }

        private StoreManager()
        {
            SingleManager.Register<IStoreManager>(this);
            jsonService = new JsonService();
            GetProduct();
        }
        #endregion

        #region --- Get Product ---
        private async void GetProduct()
        {
            product = await Task.Run(AskServerForProduct);
            if (product != null)
                Debug.LogFormat(LOG_FORMAT, "GetProduct", "Recive new product: " + product);
        }

        private ProductData AskServerForProduct()
        {
            HttpClient httpClient = new HttpClient();
            try
            {
                StringContent content = new StringContent("{}");
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

        #region --- Store ---
        public void OpenStore()
        {
            if (isStoreOpen)
                return;
            if (!storeView)
            {
                GameObject store = Resources.Load(STORE_OBJECT) as GameObject;
                UnityEngine.Object.Instantiate(store);
                storeView = store.GetComponent<StoreView>();
            }
            storeView.SetData(product);
            storeView.gameObject.SetActive(true);
            isStoreOpen = true;
            Debug.LogFormat(LOG_FORMAT, "OpenStore", "Store is open");
        }

        public void CloseStore()
        {
            storeView.gameObject.SetActive(true);
            isStoreOpen = false;
            Debug.LogFormat(LOG_FORMAT, "CloseStore", "Store is close");
        }
        #endregion
    }
}
