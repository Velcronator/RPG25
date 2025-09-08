Shader "Custom/URP/FogCurtain"
{
    Properties
    {
        _FogColor ("Fog Color", Color) = (0.8,0.8,0.8,1)
        _Opacity ("Opacity", Range(0,1)) = 1
        _FadeHeight ("Fade Height", Float) = 5
        _NoiseTex ("Noise Texture", 2D) = "white" {}
        _NoiseTiling ("Noise Tiling", Float) = 1
        _ScrollSpeed ("Scroll Speed", Vector) = (0.05,0.02,0,0)
    }

    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent" "IgnoreProjector"="True" }
        LOD 100

        Cull Off
        ZWrite Off
        Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            sampler2D _NoiseTex;
            float4 _NoiseTex_ST;
            float4 _FogColor;
            float _Opacity;
            float _FadeHeight;
            float _NoiseTiling;
            float4 _ScrollSpeed;

            struct Attributes
            {
                float3 positionOS : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct Varyings
            {
                float4 positionHCS : SV_POSITION;
                float2 uv : TEXCOORD0;
                float3 worldPos : TEXCOORD1;
            };

            Varyings vert(Attributes IN)
            {
                Varyings OUT;
                float4 posOS = float4(IN.positionOS, 1.0);       // Ensure proper float4
                OUT.positionHCS = TransformObjectToHClip(posOS);
                OUT.uv = TRANSFORM_TEX(IN.uv, _NoiseTex);
                OUT.worldPos = TransformObjectToWorld(posOS).xyz;
                return OUT;
            }

            half4 frag(Varyings IN) : SV_Target
            {
                // Height-based fade
                half alpha = saturate(IN.worldPos.y / _FadeHeight) * _Opacity;
                
                // Scrolling noise
                float2 noiseUV = IN.uv * _NoiseTiling + _ScrollSpeed.xy * _Time.y;
                half noise = tex2D(_NoiseTex, noiseUV).r;
                alpha *= lerp(0.5h, 1.0h, noise); // Soften noise effect

                half4 col;
                col.rgb = _FogColor.rgb;
                col.a = alpha;

                return col;
            }

            ENDHLSL
        }
    }
}
