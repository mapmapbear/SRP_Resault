using Unity.Collections;
using UnityEngine.Rendering;
using UnityEngine;

namespace SRPStudy
{
    public class Lighting
    {
        private const string bufferName = "Lighting";
        private const int maxDirLightCount = 4;
        static int
            dirLightColorId = Shader.PropertyToID("_DirectionalLightColor"),
            dirLightDirectionId = Shader.PropertyToID("_DirectionalLightDirection");

        private static int dirLightCountID = Shader.PropertyToID("_DirectionalLightCount"),
            dirLightColorsID = Shader.PropertyToID("_DirectionalLightColors"),
            dirLightDirectionsID = Shader.PropertyToID("_DirectionalLightDirections");

        private static Vector4[] dirLightColors = new Vector4[maxDirLightCount];
        private static Vector4[] dirLightDirections = new Vector4[maxDirLightCount];
        
        private CommandBuffer buffer = new CommandBuffer()
        {
            name = bufferName
        };

        private CullingResults _cullingResults;
        public void Setup(ScriptableRenderContext contex,
            CullingResults cullingResults)
        {
            this._cullingResults = cullingResults;
            buffer.BeginSample(bufferName);
            // SetupDirectionalLight();
            SetupLight();
            buffer.EndSample(bufferName);
            contex.ExecuteCommandBuffer(buffer);
            buffer.Clear();
        }

        void SetupLight()
        {
            NativeArray<VisibleLight> visibleLights = _cullingResults.visibleLights;
            int dirLightCount = 0;
            for (int i = 0; i < visibleLights.Length; i++) {
                VisibleLight visibleLight = visibleLights[i];
                if (visibleLight.lightType == LightType.Directional) {
                    SetupDirectionalLight(dirLightCount++, ref visibleLight);
                    if (dirLightCount >= maxDirLightCount) {
                        break;
                    }
                }
            }

            buffer.SetGlobalInt(dirLightCountID, visibleLights.Length);
            buffer.SetGlobalVectorArray(dirLightColorsID, dirLightColors);
            buffer.SetGlobalVectorArray(dirLightDirectionsID, dirLightDirections);
        }
        void SetupDirectionalLight (int index, ref VisibleLight visibleLight) {
            // Light light = RenderSettings.sun;
            // buffer.SetGlobalVector(dirLightColorId, light.color.linear * light.intensity);
            // buffer.SetGlobalVector(dirLightDirectionId, -light.transform.forward);
            dirLightColors[index] = visibleLight.finalColor;
            dirLightDirections[index] = -visibleLight.localToWorldMatrix.GetColumn(2);
        }
    }
}