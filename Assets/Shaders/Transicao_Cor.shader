Shader "Custom/Transicao_Cor"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
		_TransitionColor("Transition Color", Color) = (1,1,1,1)
		_Threshold("Threshold", Range(0,1)) = 0
    }
    SubShader
    {
        Tags { "Queue"="Overlay+1" }
		Cull Off ZWrite Off  ZTest Always

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
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
			float4 _MainTex_ST;
			float _Threshold;
			float4 _TransitionColor;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);

                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                fixed4 col = tex2D(_MainTex, i.uv);
				col = lerp(col, _TransitionColor, _Threshold);
                return col;
            }
            ENDCG
        }
    }
}
