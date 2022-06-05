Shader "Unlit/Skybox"
{
    Properties
    {
        _DayColorMain ("Day Color Main", Color) = (1,1,1,1)
        _DayColorSecondary ("Day Color Secondary", Color) = (0,0,0,1)
        _NightColorMain ("Night Color Main", Color) = (1,1,1,1)
        _NightColorSecondary ("Night Color Secondary", Color) = (0,0,0,1)
        _TimeOfDay ("Time Of Day", Range(0, 1)) = 1
        _ColorOffset ("Offset", float) = 1
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }

        Cull Front

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

            float4 _DayColorMain;
            float4 _DayColorSecondary;
            float4 _NightColorMain;
            float4 _NightColorSecondary;
            float _ColorOffset;
            float _TimeOfDay;

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
                // sample the texture
                float4 day = lerp(_DayColorMain, _DayColorSecondary, i.uv.y + _ColorOffset);
                float4 Night = lerp(_NightColorMain, _NightColorSecondary, i.uv.y + _ColorOffset);
                return lerp(day, Night, _TimeOfDay);
            }
            ENDCG
        }
    }
}