using UnityEngine;
using UnityEngine.Rendering;

namespace SRPStudy
{ 
    public class CameraRenderer
    {
        private ScriptableRenderContext _context;
        private Camera _camera;
        const string bufferName = "Render Camera";
        CommandBuffer buffer = new CommandBuffer
        {
            name = bufferName
        };
        private CullingResults _cullingResults;
            
            
        
        public void Render(ScriptableRenderContext context, Camera camera)
        {
            this._camera = camera;
            this._context = context;
            if (!Cull()) return;
            Setup();
            DrawVisibleGeomerty();
            Submit();
        }

        void DrawVisibleGeomerty()
        {
            _context.DrawSkybox(_camera);
        }
        void Setup()
        {            
            _context.SetupCameraProperties(_camera);
            buffer.ClearRenderTarget(true, true, Color.clear);
            buffer.BeginSample(bufferName);
            ExecuteBuffer();
        }
        void Submit()
        {
            buffer.EndSample(bufferName);
            ExecuteBuffer();
            _context.Submit();
        }
        //
        void ExecuteBuffer()
        {
            _context.ExecuteCommandBuffer(buffer);
            buffer.Clear();
        }

        bool Cull()
        {
            if(_camera.TryGetCullingParameters(out ScriptableCullingParameters p))
            {
                _cullingResults = _context.Cull(ref p);
                return true;
            }
            return false;
        }
    }
}