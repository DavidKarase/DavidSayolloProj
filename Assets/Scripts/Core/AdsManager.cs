using Sayollo.Services;
using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using UnityEngine;

namespace Sayollo.Core
{
    public delegate void AdCallbeck(VastData data);

    public class AdsManager : IAdsManager
    {
        private const string ERROR_FORMAT = "Sayollo.AdsManager.{0}: {1}";
        private const string URL = "https://6u3td6zfza.execute-api.us-east-2.amazonaws.com/prod/ad/vast";

        private XmlService xmlService;

        [RuntimeInitializeOnLoadMethod]
        private static void OnRuntimeMethodLoad()
        {
            new AdsManager();
        }

        private AdsManager()
        {
            SingleManager.Register<IAdsManager>(this);
            xmlService = new XmlService();
            GetAd(OnAdArrive);//TODO remove
        }

        public async void GetAd(AdCallbeck adCallbeck)
        {
            VastData result = await Task.Run(AskServerForAd);
            adCallbeck?.Invoke(result);
        }

        private VastData AskServerForAd()
        {
            WebResponse response = null;
            try
            {
                WebRequest request = WebRequest.Create(URL);
                response = request.GetResponse();

                VastData vastData = null;
                using (Stream dataStream = response.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(dataStream);
                    string responseFromServer = reader.ReadToEnd();
                    vastData = xmlService.XmlToObject<VastData>(responseFromServer);
                }
                response.Close();
                return vastData;
            }
            catch (Exception e)
            {
                Debug.LogErrorFormat(ERROR_FORMAT, "AskServerForAd", e.Message);
                if (response != null)
                    response.Close();
                return null;
            }
        }

        //TODO remove
        private static void OnAdArrive(VastData vastData)
        {
            Debug.LogError(vastData.Ad.InLine.Creatives.Creative.Linear.MediaFiles.MediaFile);
        }
    }
}