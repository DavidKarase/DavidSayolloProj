using Sayollo.Services;
using UnityEngine;
using TMPro;

namespace Sayollo.Core
{
    public class StoreView : MonoBehaviour
    {
        private IStoreManager storeManager;

        [SerializeField]
        private TextMeshProUGUI title;

        [SerializeField]
        private TextMeshProUGUI itemName;

        [SerializeField]
        private TextMeshProUGUI price;

        private void Awake()
        {
            storeManager = SingleManager.Get<IStoreManager>();
        }

        public void SetData(ProductData product)
        {
            title.text = product.Title;
            itemName.text = product.ItemName;
            price.text = product.Price + product.CurrencySign;
        }
    }
}
