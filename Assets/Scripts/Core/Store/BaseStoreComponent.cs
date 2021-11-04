using Sayollo.Services;
using UnityEngine;

namespace Sayollo.Core
{
    public class BaseStoreComponent : MonoBehaviour
    {
        private IStoreManager storeManager;
        private IStoreManager StoreManager
        {
            get
            {
                if (storeManager == null)
                    storeManager = SingleManager.Get<IStoreManager>();
                return storeManager;
            }
        }

        public void OpenStore() => StoreManager.OpenStore();
    }
}
