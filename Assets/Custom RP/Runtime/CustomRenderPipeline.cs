using UnityEngine;
using UnityEngine.Rendering;

public partial class CustomRenderPipeline : RenderPipeline {

	CameraRenderer renderer = new CameraRenderer();

	bool useDynamicBatching, useGPUInstancing, useLightsPerObject;
	private bool allowHDR;
	ShadowSettings shadowSettings;
	PostFXSettings postFXSettings;
	int colorLUTResolution;
	
	public CustomRenderPipeline (
		bool allowHDR, bool useDynamicBatching, bool useGPUInstancing, bool useSRPBatcher,
		bool useLightsPerObject, ShadowSettings shadowSettings, PostFXSettings postFXSettings, int colorLUTResolution
	) {
		this.colorLUTResolution = colorLUTResolution;
		this.shadowSettings = shadowSettings;
		this.useDynamicBatching = useDynamicBatching;
		this.useGPUInstancing = useGPUInstancing;
		this.allowHDR = allowHDR;
		this.useLightsPerObject = useLightsPerObject;
		this.postFXSettings = postFXSettings;
		GraphicsSettings.useScriptableRenderPipelineBatching = useSRPBatcher;
		GraphicsSettings.lightsUseLinearIntensity = true;
		InitializeForEditor();
	}

	protected override void Render (
		ScriptableRenderContext context, Camera[] cameras
	) {
		foreach (Camera camera in cameras) {
			renderer.Render(
				context, camera, allowHDR,
				useDynamicBatching, useGPUInstancing, useLightsPerObject,
				shadowSettings, postFXSettings, colorLUTResolution
			);
		}
	}
}