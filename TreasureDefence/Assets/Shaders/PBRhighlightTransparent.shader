Shader "Custom/PBRhighlightTransparent"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        [Toggle] _FernelToggle ("Fresnel Toggle", float) = 0
        _FernelStrength ("Fresnel Strength", Range(0, 2)) = 1
        _ColorFresnel ("Fresnel Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent"}

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard fullforwardshadows alpha:blend

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        sampler2D _MainTex;

        struct Input
        {
            float2 uv_MainTex;
            float3 viewDir;
            float texcoord : TEXCOORD3;
            float3 worldRefl; INTERNAL_DATA
        };

        half _Glossiness;
        half _Metallic;
        fixed4 _Color;
        float4 _ColorFresnel;
        float _FernelToggle;
        float _FernelStrength;

        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            // Albedo comes from a texture tinted by color
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
            
            // calculate fernel
            float3 N = normalize(o.Normal);
            float3 V = IN.viewDir;
            float fernel = 1-dot(V, N) * _FernelStrength;

            o.Albedo = fernel;
            o.Emission = saturate(fernel * _ColorFresnel * _FernelToggle);
            // Metallic and smoothness come from slider variables
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
            o.Alpha = fernel * _FernelToggle;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
