Shader "Custom/ParticleExplotion"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0
        _ColorFernel ("Fernel Color", Color) = (1,1,1,1)
        _FernelStrength ("Fernel Strength", Range(0, 2)) = 1

        _VectorDebug("debug vector", vector) = (-1,-1,-1)
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" }
        LOD 200

        ZTest Always
        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard fullforwardshadows vertex:vert

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        sampler2D _MainTex;

        struct Input
        {
            float2 uv_MainTex;
            float texcoord : TEXCOORD3;
        };

        void vert (inout appdata_full v) {
			// the color comes from a texture tinted by color
            v.texcoord = mul( unity_ObjectToWorld, v.vertex);
        }


        half _Glossiness;
        half _Metallic;
        fixed4 _Color;

        float4 _ColorFernel;
        float _FernelStrength;

        float3 _VectorDebug;

        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            // calculate fernel
            float3 N = normalize(o.Normal);
            float3 V = normalize(_WorldSpaceCameraPos - IN.texcoord);
            
            _VectorDebug = _WorldSpaceCameraPos;

            float fernel = 1-dot(V, N) * _FernelStrength;


            o.Emission = fernel * _ColorFernel;

            // Albedo comes from a texture tinted by color
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
            o.Albedo = c.rgb;
            // Metallic and smoothness come from slider variables
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
            o.Alpha = c.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
