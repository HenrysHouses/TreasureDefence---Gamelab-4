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
		LOD 200
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
