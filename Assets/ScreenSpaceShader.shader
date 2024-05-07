Shader "Custom/ObjectSpaceTextureShader" {
    Properties{
        _MainTex("Texture", 2D) = "white" {}
    }
        SubShader{
            Tags { "RenderType" = "Opaque" }
            LOD 100

            Pass {
                CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag
                #include "UnityCG.cginc"

                struct appdata {
                    float4 vertex : POSITION;
                    float2 uv : TEXCOORD0;
                };

                struct v2f {
                    float2 uv : TEXCOORD0;
                    float4 pos : SV_POSITION;
                };

                sampler2D _MainTex;

                v2f vert(appdata v) {
                    v2f o;
                    o.pos = float4(v.vertex.xyz * 2, 1);
                    o.uv = v.uv;
                    return o;
                }

                half4 frag(v2f i) : SV_Target{
                    return tex2D(_MainTex, i.uv);
                }
                ENDCG
            }
    }
}
