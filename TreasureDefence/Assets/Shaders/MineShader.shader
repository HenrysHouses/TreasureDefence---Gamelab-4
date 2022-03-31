Shader "Custom/MineShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Color ("Color", Color) = (1,1,1,1)
        _ColorFernel ("Fernel Color", Color) = (1,1,1,1)
        [Toggle] _FernelToggle ("Fernel Toggle", float) = 1
        _FernelStrength ("Fernel Strength", Range(0, 2)) = 1
        _Gloss ("Gloss", Range(0, 1)) = 1
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass // always for directional light // cant have point light
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"
            #include "Lighting.cginc" // needs this for lighting
            // #include "AutoLight.cginc" // needs this for lighting

            struct MeshData
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                float3 normal : NORMAL;

                UNITY_VERTEX_INPUT_INSTANCE_ID

            };

            struct Interpolators
            {
                float4 vertex : SV_POSITION;
                float2 uv : TEXCOORD0;
                float3 normal : TEXCOORD1;
                float3 wPos : TEXCOORD2;

                UNITY_VERTEX_OUTPUT_STEREO

            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float _Gloss;
            float3 _Color;
            float4 _ColorFernel;
            float _FernelStrength;
            float _FernelToggle;

            Interpolators vert (MeshData v)
            {
                Interpolators o;

                UNITY_SETUP_INSTANCE_ID(v); 
                UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.normal = UnityObjectToWorldNormal( v.normal );
                o.wPos = mul( unity_ObjectToWorld, v.vertex);
                return o;
            }


            /* Phong

                // specular lighting
                float3 V = normalize(_WorldSpaceCameraPos - i.wPos );
                float3 R = reflect(-L, N);
                float3 specularLight = saturate(dot(V, R));
                
                specularLight = pow(specularLight, _Gloss);
                
                return float4(specularLight.xxx, 1);
            
            */
            /* Blinn - Phong

                // specular lighting
                float3 V = normalize(_WorldSpaceCameraPos - i.wPos );
                float3 H = normalize(L + V);
                float3 specularLight = saturate(dot(H, N)) * (lambert > 0); // using lambert to remove specular in situations when its on the back of the model
                
                specularLight = pow(specularLight, _Gloss);
                
                return float4(specularLight.xxx, 1);          
            */


            fixed4 frag (Interpolators i) : SV_Target
            {
                // diffuse lighting
                float3 N = normalize(i.normal);
                float3 L = _WorldSpaceLightPos0.xyz; // actually a direction
                float3 lambert = saturate( dot( N, L ) );
                float3 diffuseLight = lambert * _LightColor0.xyz; // saturate is the same as max(0, (dot))

                // specular lighting
                float3 V = normalize(_WorldSpaceCameraPos - i.wPos );
                float3 H = normalize(L + V);
                float3 specularLight = saturate(dot(H, N)) * (lambert > 0); // using lambert to remove specular in situations when its on the back of the model
                
                float specularExponent = exp2(_Gloss * 6 + 2 ); // might not be best to do this math in the shader
                specularLight = pow( specularLight, specularExponent ) * _Gloss; // can time with _Gloss for energy conservation aproximation // look into BRDF and inisopotic lense flaire
                specularLight *= _LightColor0.xyz;
                
                // float fernel = step(0.6,1-dot(V, N)); // creates some kind of outline but it doesnt look very good on non smooth objects
                float fernel;
                if(_FernelToggle)
                    fernel = 1-dot(V, N) * _FernelStrength;// *(cos(_Time.y * 4)*0.5+1); // flashing effect // -dot(V, N) creates the fernel glow effect
                else
                    fernel = 0;
                // return float4(specularLight, 1);
                fixed4 col = tex2D(_MainTex, i.uv);
                return float4(col.xyz *(diffuseLight * _Color + specularLight + fernel * _ColorFernel), 1);
            }
            ENDCG
        }
    }
}
