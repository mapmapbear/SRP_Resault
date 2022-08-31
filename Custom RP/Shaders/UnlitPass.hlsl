#ifndef CUSTOM_UNLIT_PASS_INCLUDE
#define CUSTOM_UNLIT_PASS_INCLUDE

#include "../ShaderLib/Commom.hlsl"
// CBUFFER_START(UnityPerMaterial)
// 	float4 _BaseColor;
// CBUFFER_END

CBUFFER_START(UnityPerMaterial)
	float4 _BaseColor;
CBUFFER_END

float4 vert(float3 positionOS : POSITION) : SV_POSITION
{
	float3 positionWS = TransformObjectToWorld(positionOS.xyz);
	return TransformWorldToHClip(positionWS);
}

float4 frag() : SV_TARGET
{
	return _BaseColor;
}

#endif

