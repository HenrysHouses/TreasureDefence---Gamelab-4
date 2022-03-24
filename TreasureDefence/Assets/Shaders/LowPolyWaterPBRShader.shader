Shader "LowPolyShaders/LowPolyVertexDisplacementPBR" {
	Properties {
		_MainTex ("Color Scheme", 2D) = "white" {}
		_Color ("Tint", Color) = (1,1,1,1)
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
		_Metallic ("Metallic", Range(0,1)) = 0.0
		_Alpha ("Alpha", Range(0,1)) = 1
        _NoiseTex ("Moving Noise Texture", 2D) = "white" {}
        _Amplitude ("Noise Amplitude", float) = 1
		_Offset ("Placement Offset", float) = 0
        _MovementDirection ("Movement Direction", vector) = (1,1,0,1)
	}
	SubShader {
		Tags {"Queue"="Transparent" "RenderType"="TransparentCutout" }
        ZTest LEqual // LEqual - Default (under), GEqual - only behind something, Always - Always above
        Blend SrcAlpha OneMinusSrcAlpha
		
		CGPROGRAM
		// Physically based StandardSpecular lighting model, and enable shadows on all light types
		#pragma surface surf StandardSpecular  fullforwardshadows vertex:vert alpha:blend

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		struct Input {
			float4 color : COLOR;
		};		

		sampler2D _MainTex;
		fixed4 _Color;

		sampler2D _NoiseTex;
		float _Amplitude;
		float4 _MovementDirection;
		float _Offset;


		void vert (inout appdata_full v) {
			// the color comes from a texture tinted by color
			float4 movingNoiseMap = tex2Dlod(_NoiseTex, float4(v.vertex.xy + _MovementDirection.xy * _Time.y * 0.003, v.vertex.yw)); // tex2D is now allowed in vertex shader. use tex2dlod instead
			float4 movingVertPos = float4(v.normal * _Amplitude, 0) * movingNoiseMap;
			v.vertex += float4(movingVertPos.x, movingVertPos.y -_Offset, movingVertPos.z, movingVertPos.w);
			v.color = tex2Dlod(_MainTex, v.texcoord) * _Color;
        }

		half _Glossiness;
		half _Metallic;
		half _Alpha;

		void surf (Input IN, inout SurfaceOutputStandardSpecular o) {
			// Albedo comes from the vertex input
			o.Albedo = IN.color;
			// Metallic and smoothness come from slider variables
			o.Smoothness = _Glossiness;
			o.Specular = fixed3(0,0,0);
			o.Alpha = _Alpha;
		}
		ENDCG
	} 
	FallBack "Diffuse"
}


// boat float attempt
// Shader "LowPolyShaders/LowPolyVertexDisplacementPBR" {
// 	Properties {
// 		_MainTex ("Color Scheme", 2D) = "white" {}
// 		_Color ("Tint", Color) = (1,1,1,1)
// 		_Glossiness ("Smoothness", Range(0,1)) = 0.5
// 		_Metallic ("Metallic", Range(0,1)) = 0.0
// 		_Alpha ("Alpha", Range(0,1)) = 1
//         _NoiseTex ("Moving Noise Texture", 2D) = "white" {}
//         _Amplitude ("Noise Amplitude", float) = 1
// 		_Offset ("Placement Offset", float) = 0
//         _MovementDirection ("Movement Direction", vector) = (1,1,0,1)
// 		_Speed ("Wave Speed", float) = 1
// 		_Count ("Wave Count", float) = 8
// 		_MaxWaveHeight ("Max Wave Height", float) = 1
// 		_MinWaveHeight ("Min Wave Height", float) = 1
// 		_MinY ("Min Wave y Offset", float) = 1
// 		_MinX ("Min Wave x Offset", float) = 1
// 		_Wiggle ("Wave Wiggle", float) = 1
// 	}
// 	SubShader {
// 		Tags {"Queue"="Transparent" "RenderType"="TransparentCutout" }
//         ZTest LEqual // LEqual - Default (under), GEqual - only behind something, Always - Always above
//         Blend SrcAlpha OneMinusSrcAlpha
		
// 		CGPROGRAM
// 		// Physically based StandardSpecular lighting model, and enable shadows on all light types
// 		#pragma surface surf StandardSpecular  fullforwardshadows vertex:vert alpha:blend

// 		// Use shader model 3.0 target, to get nicer looking lighting
// 		#pragma target 3.0
// 		#define TAU 6.28318530718

// 		struct Input {
// 			float4 color : COLOR;
// 		};		

// 		sampler2D _MainTex;
// 		fixed4 _Color;

// 		sampler2D _NoiseTex;
// 		float _Amplitude;
// 		float4 _MovementDirection;
// 		float _Offset;
// 		float _Speed;
// 		float _Count;
// 		float _Wiggle;
// 		float _MaxWaveHeight;
// 		float _MinWaveHeight;
// 		float _MinX;
// 		float _MinY;

// 		float GetWave(float2 uv){
// 			float2 uvsCentered = uv * 2 - 1;
// 			float radialDistance = length(uvsCentered);

// 			float wave = cos((radialDistance - _Time.y * _Speed) * TAU * _Count) * 0.5 + 0.5;
// 			wave *= 1-radialDistance;
// 			return wave;
// 		}

// 		float GetStraightWave(float uv)
// 		{
// 			float xOffset = cos(uv.x * TAU * _Wiggle) * 0.01;
// 			float t = cos((uv.x + xOffset - _Time.y * _Speed) * TAU * _Count) * 0.5 + 0.5;
// 			float waves = t;

// 			return waves;
// 		}

// 		float DrawLine(float2 start, float2 end, float2 uv)
// 		{
// 			float2 p1 =  start;
// 			float2 p2 =  end;
// 			float2 p3 = uv;
// 			float2 p12 = p2-p1;
// 			float2 p13 = p3-p1;

// 			float d = dot(p12, p13) / length(p12);
// 			float p4 = p1 + normalize(p12) * d;
// 			float col = 0;
// 			if(length(p4-p3) < 0.07)
// 			{
// 				col ++;
// 			}
// 			return col;
// 		}

// 		void vert (inout appdata_full v) {
// 			// the color comes from a texture tinted by color
// 			// float4 movingNoiseMap = tex2Dlod(_NoiseTex, float4(v.vertex.xy + _MovementDirection.xy * _Time.y * 0.003, v.vertex.yw)); // tex2D is now allowed in vertex shader. use tex2dlod instead
// 			// float4 movingVertPos = float4(v.normal * _Amplitude, 0) * movingNoiseMap;

// 			float wave = GetStraightWave(v.texcoord);
// 			// float wave = cos( _Time.y);

// 			// float _temp = frac(lerp(0, 1, v.texcoord.xy + _Time.y));
// 			// float time = (_Time.y%2)*0.3;
// 			// float _line = DrawLine(float2(1,0), float2(1,1), v.texcoord);
// 			float4 movingVertPos = float4(v.normal * _Amplitude, 0) * wave;
// 			v.vertex += float4(movingVertPos.x, movingVertPos.y -_Offset, movingVertPos.z, movingVertPos.w);
// 			// v.vertex.y += _line;
// 			v.vertex.y = clamp(v.vertex.y, _MinWaveHeight, _MaxWaveHeight);

			
// 			v.color = tex2Dlod(_MainTex, v.texcoord) * _Color;
// 			// v.color = _line;
// 		}

// 		half _Glossiness;
// 		half _Metallic;
// 		half _Alpha;

// 		void surf (Input IN, inout SurfaceOutputStandardSpecular o) {
// 			// Albedo comes from the vertex input
// 			o.Albedo = IN.color;
// 			// Metallic and smoothness come from slider variables
// 			o.Smoothness = _Glossiness;
// 			o.Specular = fixed3(0,0,0);
// 			o.Alpha = _Alpha;
// 		}
// 		ENDCG
// 	} 
// 	FallBack "Diffuse"
// }

