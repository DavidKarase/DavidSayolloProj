using Sayollo.Services;
using UnityEngine;

namespace Sayollo.Core
{
    public abstract class BaseAdComponent : MonoBehaviour, IBaseAdComponent
    {
        private IAdsManager adsManager;

        private void Start()
        {
            adsManager = SingleManager.Get<IAdsManager>();
            adsManager.Register(this);
        }

        private void OnDisable()
        {
            //adsManager.Unregister(this);//TODO
        }

        public abstract void SetTexture(RenderTexture texture);
    }
}
