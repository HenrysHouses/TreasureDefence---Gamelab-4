Shader "Unlit/GhostHighLighter"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _FernelStrength ("Fernel Strength", Range(0, 2)) = 1
        _BlinkSpeed ("Blink Speed", float) = 1
        _MinAlpha ("Minimum Alpha", float) = 0
        _MaxAlpha ("Maximum Alpha", float) = 1
    }
    SubShader
    {
        Tags { "Queue"="Transparent" "RenderType"="TransparentCutout" }
        LOD 100

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
                float3 normal : NORMAL;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float2 uv : TEXCOORD0;
                float3 normal : TEXCOORD1;
                float3 wPos : TEXCOORD2;
            };

            float4 _Color;
            float _FernelStrength;
            float _BlinkSpeed;
            float _MaxAlpha;
            float _MinAlpha;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.normal = UnityObjectToWorldNormal( v.normal );
                o.wPos = mul( unity_ObjectToWorld, v.vertex);
                return o;
            }

            float Remap (float from, float fromMin, float fromMax, float toMin,  float toMax)
            {
                float fromAbs  =  from - fromMin;
                float fromMaxAbs = fromMax - fromMin;      
            
                float normal = fromAbs / fromMaxAbs;
        
                float toMaxAbs = toMax - toMin;
                float toAbs = toMaxAbs * normal;
        
                float to = toAbs + toMin;
            
                return to;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // create fernel
                float3 N = normalize(i.normal);
                float3 V = normalize(_WorldSpaceCameraPos - i.wPos );
                float fernel = 1-dot(V, N) * _FernelStrength;
                float wave = cos(_Time.y * _BlinkSpeed) + 1;
                wave = Remap(wave, 0, 2, _MinAlpha, _MaxAlpha);
                // appply color
                fixed4 col = _Color * fernel * wave;
                return col;
            }
            ENDCG
        }
    }
}