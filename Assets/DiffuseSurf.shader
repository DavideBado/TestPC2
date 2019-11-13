Shader "Custom/DiffuseSurf"
{
    Properties
    {
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
	    _Color ("Tint Color", Color) = (1, 1, 1, 1)
    }

    SubShader
    {
        Tags { "RenderType"="Opaque" }

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard

	    sampler2D _MainTex;
		fixed4 _Color;

        struct Input
        {
            float2 uv_MainTex;
        };

	    void surf (Input IN, inout SurfaceOutputStandard o)
	    {
			fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
		    o.Albedo = c.rgb;
		}

        ENDCG
    }

    FallBack "Diffuse"
}
