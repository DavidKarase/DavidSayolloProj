using Sayollo.Core;
using UnityEngine.EventSystems;

namespace Sayollo.Components
{
    public class SayolloStore2D : BaseStoreComponent, IPointerClickHandler
    {
        public void OnPointerClick(PointerEventData eventData)
        {
            OpenStore();
        }
    }
}
