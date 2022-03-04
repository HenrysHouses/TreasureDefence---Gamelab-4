Shader "Unlit/TowerCylinderHighlight"
{
    Properties
    {
        // _MainTex ("Texture", 2D) = "white" {}
        _Color ("Color", Color) = (1,1,1,1)
        _Color2 ("Color2", Color) = (1,1,1,1)
        _Alpha ("Transparency", Range(0,1)) = 1
        _PulseSpeed ("Speed", Range(0,10)) = 1
    }
    SubShader
    {
        Tags { "RenderType"="TransparentCutout" "Queue" = "Transparent"}
		Cull Off
		ZWrite Off
        ZTest LEqual
        Blend SrcAlpha OneMinusSrcAlpha
        
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                float3 normals : NORMAL; // local space normal position

                UNITY_VERTEX_INPUT_INSTANCE_ID
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                float3 normal : TEXCORD0; // data, can be anything and not limited by only positive numbers. different from meshdata texcords

                UNITY_VERTEX_OUTPUT_STEREO
            };

            // sampler2D _MainTex;
            // float4 _MainTex_ST;
            float4 _Color;
            float4 _Color2;
            float _Alpha;
            float _PulseSpeed;

            v2f vert (appdata v)
            {
                v2f o;
                UNITY_SETUP_INSTANCE_ID(v); 
                UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
                o.normal = UnityObjectToWorldNormal(v.normals) ; // just pass data //o.normal = mul((float3x3)unity_ObjectToWorld, v.normals); //global normals
                o.vertex = UnityObjectToClipPos(v.vertex);
                // o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.uv = v.uv;
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
                float topBottomRemover = (abs(i.normal.y) < 0.999);
                // sample the texture
                // fixed4 col = _Color;
                // fixed4 col = tex2D(_MainTex, i.uv) * _Color;
                float4 mask = lerp(float4(1,1,1,1), float4(0,0,0,0), i.uv.y);
                

                float t = cos(_Time.y * _PulseSpeed);
                t = Remap(t, -1, 1, 0,1);
                mask.w *= topBottomRemover * _Alpha * (t+0.1);

                float4 col = lerp(_Color, _Color2, t);
                mask.xyz = mask.xyz * col.xyz; 
                return mask;
            }
            ENDCG
        }
    }
}
