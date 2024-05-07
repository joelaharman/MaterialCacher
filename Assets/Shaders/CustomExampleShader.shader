Shader "Custom/CustomExampleShader" {
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

                float _Mode = 0;

                struct appdata {
                    float4 vertex : POSITION;
                    float3 normal : NORMAL;
                    float2 uv : TEXCOORD0;
                };

                struct v2f {
                    float2 uv : TEXCOORD0;
                    float3 normal : NORMAL;
                    float4 pos : SV_POSITION;
                };

                sampler2D _MainTex;
                float4 _MainTex_ST;

                v2f vert(appdata v) {
                    v2f o;
                    if (_Mode > 0.5) {
                        o.pos = float4(v.vertex.xyz * 2, 1);
                    }
                    else {
                        o.pos = UnityObjectToClipPos(v.vertex);
                    }
                    o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                    return o;
                }

                half4 frag(v2f i) : SV_Target{

                    // Busy work to lower FPS.
                    float layerValue = 0;
                    for (int j = 0; j < 400000; j++)
                    {
                        layerValue += sqrt(j);
                    }
                    layerValue = min(1.0f, layerValue);
                
                    // Normal
                    return tex2D(_MainTex, i.uv) * layerValue;
                }

                ENDCG
            }
    }
}
