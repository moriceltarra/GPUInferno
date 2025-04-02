Shader "Unlit/HeavyURP"
{
    Properties
    {
        _BaseColor ("Base Color", Color) = (1,1,1,1)
        _Distortion ("Distortion Intensity", Range(0,10)) = 5
        _EmissionColor ("Emission Color", Color) = (1,1,1,1)
        _Metallic ("Metallic", Range(0,1)) = 1
        _Smoothness ("Smoothness", Range(0,1)) = 1
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" "RenderPipeline"="UniversalPipeline" }
        Pass
        {
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            struct Attributes
            {
                float4 positionOS : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct Varyings
            {
                float4 positionCS : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            float _Distortion;
            half4 _BaseColor;
            half4 _EmissionColor;
            half _Metallic;
            half _Smoothness;

            Varyings vert(Attributes IN)
            {
                Varyings OUT;
                OUT.positionCS = TransformObjectToHClip(IN.positionOS);
                OUT.uv = IN.uv;
                return OUT;
            }

            half4 frag(Varyings IN) : SV_Target
            {
                // 🔥 Ruido dinámico basado en tiempo
                float noise = sin(_Time.y * 50) * cos(IN.uv.x * IN.uv.y * _Distortion);
                
                // 🔥 Distorsión en pantalla
                float2 distortedUV = IN.uv + noise * 0.05;
                
                // 🔥 Color base dinámico con ruido
                float3 color = lerp(float3(0,0,1), float3(1,0,0), noise);
                
                // 🔥 Cálculo de emisión HDR
                float3 emission = _EmissionColor.rgb * 2.0 * noise;

                // 🔥 PBR: Metallic y Smoothness
                float3 finalColor = color * _BaseColor.rgb * _Metallic;
                float3 finalSmooth = lerp(finalColor, emission, _Smoothness);
                
                return half4(finalSmooth, 1);
            }
            ENDHLSL
        }
    }
}