Shader "lit/lit"
{
    
    Properties
    {
        _BaseMap("Texture", 2D) = "white" {}
        _BaseColor("Color", Color) = (0.5, 0.5, 0.5, 1.0)
        _CutOff("Alpha Cutoff", Range(0.0, 1.0)) = 0.5
        _Metallic("Metallic", Range(0, 1)) = 0
        _Smoothness("Smoothness", Range(0, 1))= 0.5
        [Toggle(_CLIPPING)] _Clipping ("Alpha Clipping", Float) = 0
        [Toggle(_PREMULTIPLY_ALPHA)] _PremulAlpha ("Premultiply Alpha", Float) = 0
        [Enum(UnityEngine.Rendering.BlendMode)]_SrcBlend("Src Blend", Float) = 1.0
        [Enum(UnityEngine.Rendering.BlendMode)]_DstBlend("Dst Blend", Float) = 0.0
        [Enum(off, 0, On, 1)] _ZWrite ("ZWrite", Float) = 1.0
        
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        Tags { "LightMode"="CustomLit"}
        LOD 100

        Pass
        {
            Blend[_SrcBlend] [_DstBlend]
            HLSLPROGRAM
#pragma target 3.5
#pragma shader_feature _CLIPPING
#pragma shader_feature _PREMULTIPLY_ALPHA
#pragma multi_compile_instancing
#pragma vertex vert
#pragma fragment frag
#include "litPass.hlsl" 

            ENDHLSL
        }
    }
    CustomEditor  "CustomShaderGUI"
}
