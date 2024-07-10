sampler2D input : register(s0);

float4 main(float2 uv : TEXCOORD) : COLOR
{
    float4 color = tex2D(input, uv);
    color.rgb = 1 - color.rgb; // Invert the color
    return color;
}
