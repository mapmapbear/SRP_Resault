using UnityEngine;
using UnityEngine.Rendering;


namespace SRPStudy
{ 
    partial class CameraRenderer
    {
        private ScriptableRenderContext _context;
        private Camera _camera;
        const string bufferName = "Render Camera";
        CommandBuffer buffer = new CommandBuffer
        {
            name = bufferName
        };
        private CullingResults _cullingResults;
        private static ShaderTagId unlitShaderTagId = new ShaderTagId("SRPDefaultUnlit");

        public void Render(ScriptableRenderContext context, Camera camera)
        {
            this._camera = camera;
            this._context = context;
            PrepareBuffer();
            PrepareForSceneWindow();
            if (!Cull()) return;
            Setup();
            DrawVisibleGeomerty();
            DrawUnsupportedShaders();
            DrawGizmos(); //绘制相机视椎
            Submit();
        }

        void DrawVisibleGeomerty()
        {
            //通过排序后的相机的正交排序或者距离的排序来平判断是否渲染
            var sortingSettings = new SortingSettings(_camera) {
                criteria = SortingCriteria.CommonOpaque
            };
            var drawingSettings = new DrawingSettings(unlitShaderTagId, sortingSettings);
            //将渲染队列中的所有对象设置为过滤的对象
            var filteringSettings = new FilteringSettings(RenderQueueRange.opaque);
            _context.DrawRenderers(_cullingResults, ref drawingSettings, ref filteringSettings);
            _context.DrawSkybox(_camera);
            sortingSettings.criteria = SortingCriteria.CommonTransparent;
            drawingSettings.sortingSettings = sortingSettings;
            filteringSettings.renderQueueRange = RenderQueueRange.transparent;
            _context.DrawRenderers(_cullingResults, ref drawingSettings, ref filteringSettings);
        }
        void Setup()
        {            
            _context.SetupCameraProperties(_camera);
            CameraClearFlags flags = _camera.clearFlags;
            buffer.ClearRenderTarget(
                flags <= CameraClearFlags.Depth, 
                flags == CameraClearFlags.Color,
                flags == CameraClearFlags.Color ? _camera.backgroundColor.linear : Color.clear
                );
            buffer.BeginSample(SampleName);
            ExecuteBuffer();
        }
        void Submit()
        {
            buffer.EndSample(SampleName);
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