using Sayollo.Core;
using UnityEngine;
using UnityEngine.UI;

namespace Sayollo.Components
{
    [AddComponentMenu("Sayollo/Ad/Ad2D")]
    [RequireComponent(typeof(RawImage))]
    public class Ad2D : BaseAdComponent
    {
        private RawImage rawImage;
        private RawImage RawImage
        {
            get
            {
                if (!rawImage)
                    rawImage = GetComponent<RawImage>();
                return rawImage;
            }
        }

        public override void SetTexture(RenderTexture texture)
        {
            RawImage.texture = texture;
        }
    }
}
