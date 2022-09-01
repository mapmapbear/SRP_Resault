using UnityEngine;
using UnityEngine.Rendering;

namespace SRPStudy
{
    public class MyPipeline : RenderPipeline
    {
        private CameraRenderer renderer = new CameraRenderer();
        private bool useDynamicBatching, useGPUInstancing;
        protected override void Render(ScriptableRenderContext context, Camera[] cameras)
        {
            foreach (Camera camera in cameras)
            {
                renderer.Render(context, camera, useDynamicBatching, useDynamicBatching);
            }
        }

        public MyPipeline(bool useDynamicBatching, bool useGPUInstancing, bool useSRPBatcher)
        {
            this.useDynamicBatching = useDynamicBatching;
            this.useGPUInstancing = useDynamicBatching;
            GraphicsSettings.useScriptableRenderPipelineBatching = useSRPBatcher;
        }
    }
}