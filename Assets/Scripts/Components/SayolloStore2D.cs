using Sayollo.Core;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Sayollo.Components
{
    [AddComponentMenu("Sayollo/Store/SayolloStore2D")]
    public class SayolloStore2D : BaseStoreComponent, IPointerClickHandler
    {
        public void OnPointerClick(PointerEventData eventData)
        {
            OpenStore();
        }
    }
}
