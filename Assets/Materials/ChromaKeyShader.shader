Shader "Custom/ChromaKeyShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _ChromaKeyColor ("Chroma Key Color", Color) = (0,1,0,1)
    }
    SubShader
    {
        Tags { "Queue" = "Overlay" }
        LOD 100

        Pass
        {
            ZWrite Off
            Blend SrcAlpha OneMinusSrcAlpha
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata_t
            {
                float4 vertex : POSITION;
                float2 texcoord : TEXCOORD0;
            };

            struct v2f
            {
                float2 texcoord : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _ChromaKeyColor;

            v2f vert (appdata_t v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.texcoord = v.texcoord;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 tex = tex2D(_MainTex, i.texcoord);
                // Chroma key logic with proper alpha handling
                if (all(abs(tex.rgb - _ChromaKeyColor.rgb) < 0.1))
                {
                    discard;
                }
                return tex;
            }
            ENDCG
        }
    }
}
