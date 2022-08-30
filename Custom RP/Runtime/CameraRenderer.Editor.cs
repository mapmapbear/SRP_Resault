using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Profiling;

namespace SRPStudy
{ 
    partial class CameraRenderer
    {
        partial void DrawUnsupportedShaders();
        partial void DrawGizmos();
        partial void PrepareForSceneWindow();
        partial void PrepareBuffer();
#if UNITY_EDITOR
        private static Material errorMaterial;
        private static ShaderTagId[] legacyShaderTagIds =
        {
            new ShaderTagId("Always"),
            new ShaderTagId("ForwardBase"),
            new ShaderTagId("PrepassBase"),
            new ShaderTagId("Vertex"),
            new ShaderTagId("VertexLMRGBM"),
            new ShaderTagId("VertexLM")
        };
#endif
        
#if UNITY_EDITOR
        partial void DrawUnsupportedShaders()
        {
            if (errorMaterial == null)
            {
                errorMaterial = new Material(Shader.Find("Hidden/InternalErrorShader"));
            }
            // 把支持的shader类型数组插入到过滤内容中
            var drawingSettings = new DrawingSettings(legacyShaderTagIds[0], new SortingSettings(_camera)
            ){
                overrideMaterial = errorMaterial
            };
            for (int i = 1; i < legacyShaderTagIds.Length; ++i) {
                drawingSettings.SetShaderPassName(i, legacyShaderTagIds[i]);
            }
            
            var filteringSettings = FilteringSettings.defaultValue;
            _context.DrawRenderers(_cullingResults, ref drawingSettings, ref filteringSettings);
        }

        partial void DrawGizmos()
        {
            if (Handles.ShouldRenderGizmos())
            {
                _context.DrawGizmos(_camera,GizmoSubset.PreImageEffects);
                _context.DrawGizmos(_camera, GizmoSubset.PostImageEffects);
            }
        }

        partial void PrepareForSceneWindow()
        {
            if (_camera.cameraType == CameraType.SceneView)
            {
                ScriptableRenderContext.EmitGeometryForCamera(_camera);
            }
        }

        private string SampleName { get; set; }
        partial void PrepareBuffer()
        {
            Profiler.BeginSample("Editor Only");
            buffer.name = SampleName = _camera.name;
            Profiler.EndSample();
        }
#else
        const string SampleName = bufferName;
        
#endif
    }
}