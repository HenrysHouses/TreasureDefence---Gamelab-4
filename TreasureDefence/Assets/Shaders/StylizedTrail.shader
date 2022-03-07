Shader "Unlit/StylizedTrail"
{
    Properties
    {
        _MainTex ("Trail Texture", 2D) = "white" {}
        _NoiseTex ("Noise Texture", 2D) = "white" {}
        _Alpha ("Alpha", Range(0, 1)) = 1
        _ScrollSpeedTexture ("Texture Speed", Range(0,5)) = 1
        _ScrollSpeedNoise ("Noise Speed", Range(0,5)) = 1
        _ColorMain ("Main Color", Color) = (1,1,1,1)
        _ColorSecond ("Secondary Color", Color) = (1,1,1,1)
        _FadeStrength ("Fade Strength", float) = 0.2
    }
    SubShader
    {
        Tags { "Queue"="Transparent" "RenderType"="TransparentCutout" }

        ZWrite Off
        ZTest Always
        Blend SrcAlpha OneMinusSrcAlpha
        
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;

                UNITY_VERTEX_INPUT_INSTANCE_ID
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;

                UNITY_VERTEX_OUTPUT_STEREO
            };

            sampler2D _MainTex;
            sampler2D _NoiseTex;
            float4 _MainTex_ST;
            float _ScrollSpeedTexture;
            float _ScrollSpeedNoise;
            fixed4 _ColorMain;
            fixed4 _ColorSecond;
            float _FadeStrength;
            float _Alpha;

            v2f vert (appdata v)
            {
                v2f o;
                UNITY_SETUP_INSTANCE_ID(v); 
                UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col;
                col = lerp(_ColorSecond, _ColorMain, 1- i.uv.x);
                col = col * tex2D(_MainTex, fixed2(i.uv.x - _Time.y * _ScrollSpeedTexture, i.uv.y));
                fixed4 noise = tex2D(_NoiseTex, fixed2(i.uv.x - _Time.y * _ScrollSpeedNoise, i.uv.y));
                fixed mask =  1-saturate( i.uv.x + noise - (1-noise - i.uv.x) + _FadeStrength);
                col *= mask;
                return fixed4(col.xyz, col.w * _Alpha);
            }
            ENDCG
        }
    }
}
