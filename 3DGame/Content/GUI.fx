float4x4 World;
float4x4 View;
float4x4 Projection;
float4 Colour;
Texture IconMap;
sampler IconMapSampler = sampler_state { texture = <IconMap> ; magfilter = POINT; minfilter = LINEAR; mipfilter=LINEAR; AddressU = wrap; AddressV = wrap;};

struct VertexShaderInput
{
    float4 Position : POSITION0;
	float2 TextureCoords: TEXCOORD0;
    // TODO: add input channels such as texture
    // coordinates and vertex colors here.
};

struct VertexShaderOutput
{
    float4 Position : POSITION0;
	float2 TextureCoords: TEXCOORD0;
    // TODO: add vertex shader outputs such as colors and texture
    // coordinates here. These values will automatically be interpolated
    // over the triangle, and provided as input to your pixel shader.
};
struct PixelShaderOutput
{
    float4 Color : COLOR0;
};
VertexShaderOutput VertexShaderFunction(VertexShaderInput input)
{
    VertexShaderOutput Output = (VertexShaderOutput)0;
	Output.Position = input.Position;
	Output.TextureCoords= input.TextureCoords;
    // TODO: add your vertex shader code here.

    return Output;
}

PixelShaderOutput PixelShaderFunction(VertexShaderOutput input)
{
    // TODO: add your pixel shader code here.
	PixelShaderOutput Output = (PixelShaderOutput)0;
	float4 shade=float4(0.5f,0.5f,0.5f,0.5f);
	float4 texcolor=tex2D(IconMapSampler, input.TextureCoords);
	Output.Color=texcolor;
	if(texcolor.r==texcolor.g && texcolor.g==texcolor.b)
	{
	Output.Color=Colour+(texcolor-shade)*2;
	if(texcolor.r<0.5f)
	 Output.Color=texcolor*Colour*2;
	}

    return Output;
}

technique Technique1
{
    pass Pass1
    {
        // TODO: set renderstates here.

        VertexShader = compile vs_4_0_level_9_1 VertexShaderFunction();
        PixelShader = compile ps_4_0_level_9_1 PixelShaderFunction();
    }
}
