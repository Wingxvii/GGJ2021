Shader "Invert"{
    //show values to edit in inspector
    Properties{
        [HideInInspector] _MainTex("Texture", 2D) = "white" {}
        _BlurSize("Blur Size", Range(0,0.1)) = 0
    }

    SubShader{
        // grabbed from https://www.ronja-tutorials.com/post/016-postprocessing-basics/
        // markers that specify that we don't need culling
        // or reading/writing to the depth buffer
        Cull Off
        ZWrite Off
        ZTest Always

        Pass{
            CGPROGRAM
            //include useful shader functions
            #include "UnityCG.cginc"

            #pragma multi_compile _SAMPLES_LOW _SAMPLES_MEDIUM _SAMPLES_HIGH
            #pragma shader_feature GAUSS

            //define vertex and fragment shader
            #pragma vertex vert
            #pragma fragment frag

            //texture and transforms of the texture
            sampler2D _MainTex;
            float _BlurSize;
            float _StandardDeviation;

            #define PI 3.14159265359
            #define E 2.71828182846

            #if _SAMPLES_LOW
                #define SAMPLES 10
            #elif _SAMPLES_MEDIUM
                #define SAMPLES 30
            #else
                #define SAMPLES 100
            #endif

            //the object data that's put into the vertex shader
            struct appdata {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            //the data that's used to generate fragments and can be read by the fragment shader
            struct v2f {
                float4 position : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            //the vertex shader
            v2f vert(appdata v) {
                v2f o;
                //convert the vertex positions from object space to clip space so they can be rendered
                o.position = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            //the fragment shader
            fixed4 frag(v2f i) : SV_TARGET{
                //get source color from texture
                fixed4 col = tex2D(_MainTex, i.uv);

                //invert the color
                col = 1 - col;
                return col;
            }

        ENDCG
        }
    }
}