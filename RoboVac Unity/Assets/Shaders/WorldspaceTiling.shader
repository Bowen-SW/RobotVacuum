// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'
// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Unlit/WorldspaceTiling"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100
        
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
            
            // Structure for input coming from the game to the vertex shader.
            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };
            
            // Structure to pass data from vertex to fragment shader.
            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };
            
            // Variables to refer to the texture & transform parameters assigned in the Inspector.
            sampler2D _MainTex;
            float4 _MainTex_ST;
            
            // Vertex shader transforms each vertex & preps data for interpolation.
            v2f vert (appdata v)
            {
                // Prepare a structure to hold the output.
                v2f o;

                // Transform the vertex into screen space.
                o.vertex = UnityObjectToClipPos(v.vertex);

                // Get the xy position of the vertex in worldspace.
                // We'll use these instead of the object's own texture coordinates.
                float2 worldXY = mul(unity_ObjectToWorld, v.vertex).xy;

                // Apply the texture offset & scale parameters set in the material Inspector.
                o.uv = TRANSFORM_TEX(worldXY, _MainTex);

                return o;
            }

            // Fragment shader renders each pixel.
            fixed4 frag (v2f i) : SV_Target
            {
                // Look up the right colour from the texture and output it.                   
                fixed4 col = tex2D(_MainTex, i.uv);
                return col;
            }
            ENDCG
        }
    }
}