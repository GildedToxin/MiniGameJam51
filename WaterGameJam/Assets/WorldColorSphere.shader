Shader "Custom/WorldColorSphere"
{
    Properties
    {
        _BaseColor ("Base Color", Color) = (1,1,1,1)
        _SphereCenter ("Sphere Center", Vector) = (0,0,0,0)
        _SphereRadius ("Sphere Radius", Float) = 5
        _Feather ("Edge Feather", Float) = 0.25
    }

    SubShader
    {
        Tags { "RenderPipeline"="UniversalPipeline" "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            Name "Unlit"
            Tags { "LightMode"="UniversalForward" }

            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            struct Attributes
            {
                float4 positionOS : POSITION;
            };

            struct Varyings
            {
                float4 positionHCS : SV_POSITION;
                float3 worldPos    : TEXCOORD0;
            };

            float4 _BaseColor;
            float3 _SphereCenter;
            float _SphereRadius;
            float _Feather;

            Varyings vert (Attributes v)
            {
                Varyings o;
                o.positionHCS = TransformObjectToHClip(v.positionOS.xyz);
                o.worldPos = TransformObjectToWorld(v.positionOS.xyz);
                return o;
            }

            half4 frag (Varyings i) : SV_Target
            {
                float dist = distance(i.worldPos, _SphereCenter);

                // Soft sphere mask
                float mask = smoothstep(
                    _SphereRadius,
                    _SphereRadius - _Feather,
                    dist
                );

                // World is black, color only exists inside the sphere
                float3 finalColor = _BaseColor.rgb * mask;

                return half4(finalColor, 1);
            }
            ENDHLSL
        }
    }
}
