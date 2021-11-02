using Sayollo.Core;
using Sayollo.Services;
using UnityEngine;

namespace Sayollo.Components
{
    public class BaseAdComponent : MonoBehaviour
    {
        private IAdsManager adsManager;

        private void Start()
        {
            adsManager = SingleManager.Get<IAdsManager>();
            adsManager.GetAd(OnAdArrive);
        }

        private void OnAdArrive(VastData vastData)
        {
            Debug.LogError(vastData.Ad.InLine.Creatives.Creative.Linear.MediaFiles.MediaFile);
        }
    }
}
