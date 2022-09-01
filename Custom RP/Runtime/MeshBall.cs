using System;
using UnityEngine;
using UnityEngine.Rendering;
using Random = UnityEngine.Random;

namespace SRPStudy
{
    public class MeshBall : MonoBehaviour
    {
        private static int baseColorID = Shader.PropertyToID("_BaseColor");
        [SerializeField] private Mesh mesh = default;
        [SerializeField] private Material material = default;
        // 随机的 转换矩阵数组和颜色数组
        private Matrix4x4[] matrices = new Matrix4x4[1023];
        private Vector4[] baseColors = new Vector4[1023];

        private MaterialPropertyBlock block;
        void Awake () {
            for (int i = 0; i < matrices.Length; i++) {
                matrices[i] = Matrix4x4.TRS(
                    Random.insideUnitSphere * 10f, 
                    Quaternion.Euler(Random.value * 360f, Random.value * 360f, Random.value * 360f),
                    Vector3.one
                );
                baseColors[i] =
                    new Vector4(Random.value, Random.value, Random.value, Random.Range(0.0f, 1.0f));
            }
        }

        private void Update()
        {
            if (block == null)
            {
                block = new MaterialPropertyBlock();
                block.SetVectorArray(baseColorID, baseColors);
            }
            Graphics.DrawMeshInstanced(mesh, 0, material, matrices, 1023, block);
        }
    }
    
    
}