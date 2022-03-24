Shader "Unlit/MoneyCounter"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _AnimationSheet ("Animation Sheet", 2D) = "white" {}
        [Toggle] _Animate ("Animate", int) = 0
        _AnimationSpeed ("Animation Speed", float) = 1
        _AnimationStartTime ("Animation Started At", float) = 0
        [HideInInspector]_CurrentTime ("CurrentTime", float) = 0
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
            float4 _MainTex_ST;
            sampler2D _AnimationSheet;
            float4 _AnimationSheet_ST;
            int _Animate;
            float _AnimationSpeed;
            float _AnimationStartTime;
            float _CurrentTime;

            v2f vert (appdata v)
            {
                v2f o;
                UNITY_SETUP_INSTANCE_ID(v); 
                UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            bool isWithinRange(float value, float min, float max)
            {
                if(value > min && value < max)
                    return true;
                return false;
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
                fixed4 col;
                if(_Animate == 1)
                {
                    // get time variable
                    float t = cos((_CurrentTime - _AnimationStartTime) * _AnimationSpeed);
                    t = Remap(t, -1, 1, 0, 12);

                    // set animation sheet
                    // frames based on t
                    if(isWithinRange(t, 0, 1) || isWithinRange(t, 11, 12))
                        col = tex2D(_MainTex, i.uv);
                    else
                    {
                        i.uv.x = i.uv.x * 0.1;
                        if(isWithinRange(t, 1, 2))
                            i.uv.x += 0;
                        if(isWithinRange(t, 2, 3))
                            i.uv.x += 0.1;
                        if(isWithinRange(t, 3, 4))
                            i.uv.x += 0.2;
                        if(isWithinRange(t, 4, 5))
                            i.uv.x += 0.3;
                        if(isWithinRange(t, 5, 6))
                            i.uv.x += 0.4;
                        if(isWithinRange(t, 6, 7))
                            i.uv.x += 0.5;
                        if(isWithinRange(t, 7, 8))
                            i.uv.x += 0.6;
                        if(isWithinRange(t, 8, 9))
                            i.uv.x += 0.7;
                        if(isWithinRange(t, 9, 10))
                            i.uv.x += 0.8;
                        if(isWithinRange(t, 10, 11))
                            i.uv.x += 0.9;
                        col = tex2D(_AnimationSheet, i.uv);
                    }
                }
                else
                {
                    col = tex2D(_MainTex, i.uv);
                }
                return col;
            }
            ENDCG
        }
    }
}
