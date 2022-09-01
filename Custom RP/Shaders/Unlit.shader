Shader "Unlit/Ulit"
{
    Properties
    {
        _BaseMap("Texture", 2D) = "white" {}
        _BaseColor("Color", Color) = (1.0, 1.0, 1.0, 1.0)
        _CutOff("Alpha Cutoff", Range(0.0, 1.0)) = 0.5
        [Toggle(_CLIPPING)] _Clipping ("Alpha Clipping", Float) = 0
        [Enum(UnityEngine.Rendering.BlendMode)]_SrcBlend("Src Blend", Float) = 1.0
        [Enum(UnityEngine.Rendering.BlendMode)]_DstBlend("Dst Blend", Float) = 0.0
        [Enum(off, 0, On, 1)] _ZWrite ("ZWrite", Float) = 1.0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            Blend[_SrcBlend] [_DstBlend]
            HLSLPROGRAM
#pragma shader_feature _CLIPPING
#pragma multi_compile_instancing
#pragma vertex vert
#pragma fragment frag
#include "UnlitPass.hlsl" 

            ENDHLSL
        }
    }
}
