Shader "Custom/FragmentShader"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
	    _Color ("Albedo Color", Color) = (1, 1, 1, 1)
	}

	SubShader
	{
		Tags 
		{
			"RenderType" = "Opaque"
			"LightMode" = "ForwardBase"
			"PassFlags" = "OnlyDirectional"
		}

		CGPROGRAM
			// Physically based Standard lighting model, and enable shadows on all light types
			#pragma surface surf Standard

		sampler2D _MainTex;
		float4 _Color;



		struct appdata
		{
			float4 vertex : POSITION;
			float2 uv : TEXCOORD0;
			float3 normal : NORMAL;
		};

		struct v2f
		{
			float2 uv : TEXCOORD0;
			float4 vertex : SV_POSITION;
			float3 worldNormal : NORMAL;
		};



		v2f vert (appdata v)
		{
			v2f o;
			o.vertex = UnityObjectToClipPos(v.vertex);
			o.uv = tex2D(_MainTex, v.uv);
			o.worldNormal = UnityObjectToWorldNormal(v.normal);
			return o;
		}

		fixed4 frag (v2f IN) : SV_Target
		{
			fixed4 albedo = tex2D(_MainTex, IN.uv) * _Color;
		    float3 normal = normalize(IN.worldNormal);
		    float diffuse = dot(_WorldSpaceLightPos0, normal);
			
		    fixed4 col = albedo * diffuse;
		    return col;
		}

		
		
		
		struct Input
		{
			float2 uv_MainTex;
		};


		void surf(Input IN, inout SurfaceOutputStandard o)
		{
			fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
			o.Albedo = c.rgb;
		}

	    ENDCG
	}
			FallBack "Diffuse"
}