using Sayollo.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Video;

namespace Sayollo.Core
{
    internal class AdsManager : IAdsManager
    {
        #region --- Members ---
        private const string LOG_FORMAT = "Sayollo.AdsManager.{0}: {1}";
        private const string URL = "https://6u3td6zfza.execute-api.us-east-2.amazonaws.com/prod/ad/vast";

        private readonly XmlService xmlService;

        private readonly RenderTexture adTexture;
        private VideoPlayer videoPlayer;
        private readonly List<IBaseAdComponent> ads = new List<IBaseAdComponent>();
        #endregion

        #region --- Init ---
        [RuntimeInitializeOnLoadMethod]
        private static void OnRuntimeMethodLoad()
        {
            new AdsManager();
        }

        private AdsManager()
        {
            SingleManager.Register<IAdsManager>(this);
            xmlService = new XmlService();
            adTexture = new RenderTexture(512, 512, 0);
            CreateVideoPlayer();
            GetAd();
        }

        private void CreateVideoPlayer()
        {
            if (videoPlayer)
                return;
            GameObject videoPlayerObj = new GameObject("SayolloAdPlayer");
            videoPlayer = videoPlayerObj.AddComponent<VideoPlayer>();
            videoPlayer.renderMode = VideoRenderMode.RenderTexture;
            videoPlayer.targetTexture = adTexture;
            videoPlayer.source = VideoSource.Url;
            videoPlayer.isLooping = true;
        }
        #endregion

        public void Register(IBaseAdComponent adComponent)
        {
            ads.Add(adComponent);
            adComponent.SetTexture(adTexture);
            PlayAd();
        }

        #region --- Get Ad ---
        private async void GetAd()
        {
            VastData result = await Task.Run(AskServerForAd);
            Debug.LogFormat(LOG_FORMAT, "GetAd", "new ad arrived: " + result.Ad.InLine.Creatives.Creative.Linear.MediaFiles.MediaFile);
            videoPlayer.url = result.Ad.InLine.Creatives.Creative.Linear.MediaFiles.MediaFile;
            PlayAd();
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
                Debug.LogErrorFormat(LOG_FORMAT, "AskServerForAd", e.Message);
                if (response != null)
                    response.Close();
                return null;
            }
        }
        #endregion

        private void PlayAd()
        {
            if (videoPlayer.isPlaying || ads.Count == 0 || string.IsNullOrEmpty(videoPlayer.url))
                return;
            videoPlayer.Play();
        }
    }
}