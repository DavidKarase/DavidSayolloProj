using Sayollo.Core;
using UnityEngine;

namespace Sayollo.Components
{
    [AddComponentMenu("Sayollo/Store/SayolloStore3D")]
    public class SayolloStore3D : BaseStoreComponent
    {
        private void OnMouseUpAsButton()
        {
            OpenStore();
        }
    }
}
