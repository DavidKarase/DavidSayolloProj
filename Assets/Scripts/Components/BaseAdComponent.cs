using Sayollo.Core;
using Sayollo.Services;
using UnityEngine;
using UnityEngine.UI;

namespace Sayollo.Components
{
    public class BaseAdComponent : MonoBehaviour, IBaseAdComponent
    {
        private IAdsManager adsManager;

        [SerializeField]
        private RawImage rawImage;

        private void Start()
        {
            adsManager = SingleManager.Get<IAdsManager>();
            adsManager.Register(this);
        }

        public void SetTexture(RenderTexture texture)
        {
            rawImage.texture = texture;
        }

        private void OnDestroy()
        {
            //adsManager.Unregister(this);//TODO
        }
    }
}
