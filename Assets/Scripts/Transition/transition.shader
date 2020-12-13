//https://blog.cfm-art.net/archives/963
//マテリアルに指定された色と、テクスチャのαで描画するだけのものです。9割9分が決まり文句です。
//色を決める部分はfrag関数です。
//tex2Dでテクスチャの色を取得し、返り値(実際に描画される色)には_Colorのrgbとテクスチャのαを指定しています。
//_Colorはマテリアルに設定されているシェーダー定数です。(インスペクターでいじれるやつ)
//この画像のα値を時間によって増減させます。
//増減させる箇所はシェーダーのfrag関数のalphaの部分です。
//時間は0~1が渡されるとして、時間が0の時は - 1、1の時は + 1するように変更します。
//時間変数_Alphaを用意してfragでalphaに足してやります。

Shader "Unlit/transition"
{
	Properties
	{
		[PerRendererData] _MainTex("Sprite Texture", 2D) = "white" {}
		_Color("Tint", Color) = (1,1,1,1)
		_Alpha("Time", Range(0, 1)) = 0
	}

		SubShader
		{
			Tags
			{
				"Queue" = "Transparent"
				"IgnoreProjector" = "True"
				"RenderType" = "Transparent"
				"PreviewType" = "Plane"
				"CanUseSpriteAtlas" = "True"
			}

			Cull Off
			Lighting Off
			ZWrite Off
			ZTest[unity_GUIZTestMode]
			Fog{ Mode Off }
			Blend SrcAlpha OneMinusSrcAlpha

			Pass
			{
				CGPROGRAM
	#pragma vertex vert
	#pragma fragment frag
	#include "UnityCG.cginc"

				struct appdata_t
				{
					float4 vertex   : POSITION;
					float2 texcoord : TEXCOORD0;
				};

				struct v2f
				{
					float4 vertex   : SV_POSITION;
					half2 texcoord  : TEXCOORD0;
				};

				fixed4 _Color;
				fixed _Alpha;
				sampler2D _MainTex;

				// 頂点シェーダーの基本
				v2f vert(appdata_t IN)
				{
					v2f OUT;
					OUT.vertex = UnityObjectToClipPos(IN.vertex);
					OUT.texcoord = IN.texcoord;
	#ifdef UNITY_HALF_TEXEL_OFFSET
					OUT.vertex.xy += (_ScreenParams.zw - 1.0) * float2(-1,1);
	#endif
					return OUT;
				}

				// 通常のフラグメントシェーダー
				fixed4 frag(v2f IN) : SV_Target
				{
					half alpha = tex2D(_MainTex, IN.texcoord).a;
					alpha = saturate(alpha + (_Alpha * 2 - 1));
					return fixed4(_Color.r, _Color.g, _Color.b, alpha);
				}
				ENDCG
			}
		}

			FallBack "UI/Default"
}