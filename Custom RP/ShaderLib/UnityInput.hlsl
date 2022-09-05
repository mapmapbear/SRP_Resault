#ifndef CUSTOM_UNITY_INPUT_INCLUDE
#define CUSTOM_UNITY_INPUT_INCLUDE

// cbuffer UintyPerDraw
// {
//     float4x4 unity_ObjectToWorld;
//     float4x4 unity_WorldToObject;
//     float4 unity_LODFade;
//     half4 unity_WorldTransformParams;
// }
CBUFFER_START(UnityPerDraw)
    float4x4 unity_ObjectToWorld;
    float4x4 unity_WorldToObject;
    float4 unity_LODFade;
    float4 unity_WorldTransformParams;
CBUFFER_END

float4x4 unity_MatrixVP;
float4x4 unity_MatrixV;
float4x4 glstate_matrix_projection;
float3 _WorldSpaceCameraPos;
#endif

