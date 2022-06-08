Shader "LowPolyShaders/LowPolyVertexDisplacementPBR" {
	Properties {
		[HDR] _Color ("Tint", Color) = (1,1,1,1)
        _SpeedTimeChange ("Speed Change", float) = 1
        _NoiseTex ("Moving Noise Texture", 2D) = "white" {}
        _Amplitude ("Noise Amplitude", float) = 1
        _MovementDirection ("Movement Direction", vector) = (1,1,0,1)
	}
	SubShader {
		Tags {"RenderType"="Opaque" }
    
        Cull Off

		CGPROGRAM
		// Physically based StandardSpecular lighting model, and enable shadows on all light types
		#pragma surface surf StandardSpecular vertex:vert

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		struct Input {
			float4 color : COLOR;
		};		

		fixed4 _Color;

		sampler2D _NoiseTex;
		float _Amplitude;
		float4 _MovementDirection;
		float _Offset;
        float _SpeedTimeChange;

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

		void vert (inout appdata_full v) {
			// the color comes from a texture tinted by color
            float _Cosin = cos(_Time.y * _SpeedTimeChange);
            _Cosin = Remap(_Cosin, -1, 1, 0.5, 1);
			float4 movingNoiseMap = tex2Dlod(_NoiseTex, float4(v.vertex.xy + _MovementDirection.xy * _Time.y * 0.003 + _Cosin, v.vertex.yw)); // tex2D is now allowed in vertex shader. use tex2dlod instead
			float4 movingVertPos = float4(v.normal * _Amplitude, 0) * movingNoiseMap;
			v.vertex += float4(movingVertPos.x, movingVertPos.y, movingVertPos.z, movingVertPos.w);
			v.color = _Color;
        }

		void surf (Input IN, inout SurfaceOutputStandardSpecular o) {
			// Albedo comes from the vertex input
			o.Albedo = IN.color;
			// Metallic and smoothness come from slider variables
			o.Smoothness = 0;
			o.Specular = fixed3(0,0,0);
            o.Emission = _Color;
			o.Alpha = 1;
		}
		ENDCG
	} 
	FallBack "Diffuse"
}