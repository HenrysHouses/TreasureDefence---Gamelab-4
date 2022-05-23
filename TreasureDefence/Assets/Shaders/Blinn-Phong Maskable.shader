Shader "HenryCustom/StencilMasking/Blinn-Phong Maskable"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Color ("Color", Color) = (1,1,1,1)
        _ColorFernel ("Fernel Color", Color) = (1,1,1,1)
        [Toggle] _FernelToggle ("Fernel Toggle", float) = 1
        _FernelStrength ("Fernel Strength", Range(0, 2)) = 1
        _Gloss ("Gloss", Range(0, 1)) = 1

        _MaskLayer ("Mask Layer", int) = 1 
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }

         Stencil {
            Ref [_MaskLayer]
            Comp NotEqual
            Pass Keep
        }   

        Pass // always for directional light // cant have point light
        {
            Tags{"Lightmode" = "ForwardBase"}
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_fwdbase

            #include "UnityCG.cginc"
            #include "Lighting.cginc" // needs this for lighting
            #include "AutoLight.cginc"


            struct MeshData
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                float3 normal : NORMAL;
            };

            struct Interpolators
            {
                float2 uv : TEXCOORD0;
                SHADOW_COORDS(1) 
                float3 wPos : TEXCOORD2;
                float4 pos : SV_POSITION;
                float3 worldNormal : NORMAL;
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
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.worldNormal = UnityObjectToWorldNormal( v.normal );
                o.wPos = mul( unity_ObjectToWorld, v.vertex);
                TRANSFER_SHADOW(o)
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
                float3 N = normalize(i.worldNormal);
                float3 L = _WorldSpaceLightPos0.xyz; // actually a direction
                float shadow = SHADOW_ATTENUATION(i); // reading shadow data from texcoord
                float3 lambert = saturate( dot( N, L ) ) * shadow;
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
                return float4(col.xyz *(diffuseLight * _Color + specularLight + saturate(fernel) * _ColorFernel), 1);
            }
            ENDCG
        }

        // shadow caster rendering pass, implemented manually
        // using macros from UnityCG.cginc
        Pass
        {
            Tags {"LightMode"="ShadowCaster"}

            Name "ShadowCast"
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_shadowcaster
            #include "UnityCG.cginc"

            struct v2f { 
                V2F_SHADOW_CASTER;
            };

            v2f vert(appdata_base v)
            {
                v2f o;
                TRANSFER_SHADOW_CASTER_NORMALOFFSET(o)
                return o;
            }

            float4 frag(v2f i) : SV_Target
            {
                SHADOW_CASTER_FRAGMENT(i)
            }
            ENDCG
        }
    }
}
