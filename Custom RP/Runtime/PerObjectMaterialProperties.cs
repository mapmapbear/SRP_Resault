using UnityEngine;
using UnityEngine.SocialPlatforms;

namespace SRPStudy
{
    [DisallowMultipleComponent]
    public class PerObjectMaterialProperties : MonoBehaviour
    {
        static MaterialPropertyBlock block;

        static int baseColorId = Shader.PropertyToID("_BaseColor");
        private static int metallicID = Shader.PropertyToID("_Metallic");
        private static int smoothnessID = Shader.PropertyToID("_Smoothness");

        [SerializeField, Range(0f, 1f)] private Color baseColor = Color.white;
        [SerializeField, Range(0f, 1f)] private float alphaCutoff = 0.5f, metallic = 0f, smoothness = 0.5f;
        
        private void OnValidate()
        {
            if(block == null)
            {
                block = new MaterialPropertyBlock();
            }
            block.SetColor(baseColorId, baseColor);
            block.SetFloat(metallicID, metallic);
            block.SetFloat(smoothnessID, smoothness);
            GetComponent<Renderer>().SetPropertyBlock(block);
        }
        
        private void Awake()
        {
            OnValidate();
        }
    }
}