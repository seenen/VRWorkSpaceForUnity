Shader "Custom/Phong tessellation&Displacement mapping" {
	Properties{
		_EdgeLength("Edge length", Range(2,50)) = 5
		_Phong("Phong Strengh", Range(0,1)) = 0.5
		_MainTex("Base (RGB)", 2D) = "white" {}
	_Color("Color", color) = (1,1,1,0)

		_DispTex("Disp Texture", 2D) = "gray" {}
	_NormalMap("Normalmap", 2D) = "bump" {}
	_Displacement("Displacement", Range(0, 1.0)) = 0.3
		_SpecColor("Spec color", color) = (0.5,0.5,0.5,0.5)
	}
		SubShader{
		Tags{ "RenderType" = "Opaque" }
		LOD 300

		CGPROGRAM
#pragma surface surf Lambert vertex:disp tessellate:tessEdge tessphong:_Phong nolightmap
#include "Tessellation.cginc"

		struct appdata {
		float4 vertex : POSITION;
		float4 tangent : TANGENT;
		float3 normal : NORMAL;
		float2 texcoord : TEXCOORD0;
	};


	sampler2D _DispTex;
	float _Displacement;

	void disp(inout appdata v)
	{
		float d = tex2Dlod(_DispTex, float4(v.texcoord.xy,0,0)).r * _Displacement;
		v.vertex.xyz += v.normal * d;
	}

	float _Phong;
	float _EdgeLength;

	float4 tessEdge(appdata v0, appdata v1, appdata v2)
	{
		return UnityEdgeLengthBasedTess(v0.vertex, v1.vertex, v2.vertex, _EdgeLength);
	}

	struct Input {
		float2 uv_MainTex;
	};

	fixed4 _Color;
	sampler2D _MainTex;
	sampler2D _NormalMap;

	void surf(Input IN, inout SurfaceOutput o) {
		half4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
		o.Albedo = c.rgb;
		o.Specular = 0.2;
		o.Gloss = 1.0;
		o.Normal = UnpackNormal(tex2D(_NormalMap, IN.uv_MainTex));
		o.Alpha = c.a;
	}

	ENDCG
	}
		FallBack "Diffuse"
}
