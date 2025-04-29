Shader "Unlit/PixelShader"
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

            sampler2D _OriginalScene;

            int AmountOfBlocks;
            int TextureWidth;
            int TextureHeight;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv; // We're using screen-space UVs, no need for TRANSFORM_TEX
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                // Calculate block size in UV space
                float blockSizeX = 1.0 / AmountOfBlocks;
                float aspect = (float)TextureWidth / TextureHeight;
                float blockSizeY = blockSizeX * aspect;

                // Find which block the current pixel belongs to
                float2 blockCoord = floor(i.uv / float2(blockSizeX, blockSizeY));

                // Sample from the center of the block
                float2 centerOfBlock = (blockCoord + 0.5) * float2(blockSizeX, blockSizeY);

                // Clamp to avoid out-of-bounds issues
                centerOfBlock = clamp(centerOfBlock, 0.0, 1.0);

                // Sample color from center of block
                fixed4 col = tex2D(_OriginalScene, centerOfBlock);
                return col;
            }

            ENDCG
        }
    }
}

