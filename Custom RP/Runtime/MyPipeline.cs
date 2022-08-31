using UnityEngine;
using UnityEngine.Rendering;

namespace SRPStudy
{
    public class MyPipeline : RenderPipeline
    {
        private CameraRenderer renderer = new CameraRenderer();
        protected override void Render(ScriptableRenderContext context, Camera[] cameras)
        {
            foreach (Camera camera in cameras)
            {
                renderer.Render(context, camera);
            }
        }

        public MyPipeline()
        {
            GraphicsSettings.useScriptableRenderPipelineBatching = true;
        }
    }
}