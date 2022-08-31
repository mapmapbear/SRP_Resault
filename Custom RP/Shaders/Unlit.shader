Shader "Unlit/Ulit"
{
    Properties
    {
        _BaseColor("Color", Color) = (1.0, 1.0, 1.0, 1.0)
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            HLSLPROGRAM
#pragma vertex vert
#pragma fragment frag
#include "UnlitPass.hlsl" 

            ENDHLSL
        }
    }
}
