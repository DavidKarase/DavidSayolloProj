using Sayollo.Core;
using UnityEngine;

namespace Sayollo.Components
{
    [AddComponentMenu("Sayollo/Ad/Ad3D")]
    [RequireComponent(typeof(MeshRenderer))]
    public class Ad3D : BaseAdComponent
    {
        private const string AdShaderName = "Unlit/Texture";

        private static Material adMaterial;
        private static Material AdMaterial
        {
            get
            {
                if (!adMaterial)
                    adMaterial = new Material(Shader.Find(AdShaderName));
                return adMaterial;
            }
        }

        private MeshRenderer meshRenderer;
        private MeshRenderer MeshRenderer
        {
            get
            {
                if (!meshRenderer)
                {
                    meshRenderer = GetComponent<MeshRenderer>();
                    meshRenderer.material = AdMaterial;
                }
                return meshRenderer;
            }
        }

        public override void SetTexture(RenderTexture texture)
        {
            MeshRenderer.material.SetTexture("_MainTex", texture);
        }
    }
}
