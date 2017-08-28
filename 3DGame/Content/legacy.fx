Texture xRock;
Texture xWaterBumpMap;
Texture xGrass;
Texture xSand;
Texture xTexture;
Texture xRefractionMap;
Texture xReflectionMap;

//------- Constants --------
float4x4 World;
float4x4 View;
float4x4 Projection;
float4x4 xView;
float4x4 xProjection;
float4x4 xWorld;
float4x4 xWorld2;
float4x4 xReflectionView;
float4x4 xBone;
float4x4 xOrigin;

float4 ClipPlane0;

float3 xLightDirection;
float3 xMoonDirection;
float3 xWindDirection;
float3 xCamPos;
float3 xCamUp;

float xAmbient;
float xPointSpriteSize;

bool xEnableLighting;
bool xShowNormals;
bool xUnderWater;
bool xFog;
bool Clipping;

float xTime;
float xWindForce;
float xWaveLength;
float xWaveHeight;
float xZoomLevel;
//------- Texture Samplers --------


struct VertexToPixel
{
    float4 Position   	: POSITION;    
    float4 Color		: COLOR0;
    float LightingFactor: TEXCOORD4;
	float MoonLight: TEXCOORD0;
    float2 TextureCoords: TEXCOORD1;
	 float Fog            : TEXCOORD5;
	 float4 clipDistances     : TEXCOORD2;
	 float4 Normal : TEXCOORD3;
	 float4 TW : TEXCOORD6;
};


struct PixelToFrame
{
    float4 Color : COLOR0;
};



sampler WaterBumpMapSampler = sampler_state { texture = <xWaterBumpMap> ; magfilter = LINEAR; minfilter = LINEAR; mipfilter=LINEAR; AddressU = wrap; AddressV = wrap;};

sampler TextureSampler = sampler_state { texture = <xTexture>; magfilter = ANISOTROPIC; minfilter =ANISOTROPIC; mipfilter=POINT; AddressU = wrap; AddressV = wrap;};

sampler GrassSampler = sampler_state { texture = <xGrass>; magfilter = ANISOTROPIC; minfilter =ANISOTROPIC; mipfilter=POINT; AddressU = wrap; AddressV = wrap;};

sampler RockSampler = sampler_state { texture = <xRock>; magfilter = ANISOTROPIC; minfilter =ANISOTROPIC; mipfilter=POINT; AddressU = wrap; AddressV = wrap;};

sampler SandSampler = sampler_state { texture = <xSand>; magfilter = ANISOTROPIC; minfilter =ANISOTROPIC; mipfilter=POINT; AddressU = wrap; AddressV = wrap;};

sampler ReflectionSampler = sampler_state { texture = <xReflectionMap> ; magfilter = ANISOTROPIC; minfilter = ANISOTROPIC; mipfilter=LINEAR; AddressU = mirror; AddressV = mirror;};

sampler RefractionSampler = sampler_state { texture = <xRefractionMap> ; magfilter = ANISOTROPIC; minfilter = ANISOTROPIC; mipfilter=LINEAR; AddressU = mirror; AddressV = mirror;};








//------- Technique: TexturedTinted --------

VertexToPixel TexturedTintedVS( float4 inPos : POSITION,  float2 inTexCoords: TEXCOORD0, float4 inNormal : NORMAL,float4 inColor: COLOR, float4 inWeights: TEXCOORD1)
{	
	VertexToPixel Output = (VertexToPixel)0;
	float4x4 preViewProjection = mul (xView, xProjection);
	float4x4 preWorldViewProjection = mul (xWorld, preViewProjection);

	Output.Position = mul(inPos, preWorldViewProjection);	
	Output.TextureCoords = inTexCoords;
    Output.Color = inColor;
	Output.clipDistances = dot(inPos, ClipPlane0);
	Output.Normal=normalize(inNormal);
	float3 outNormal = normalize(mul(normalize(inNormal), xWorld));	
	outNormal=normalize(inNormal);
	//Output.LightingFactor = 0.5f;
	//if (xEnableLighting)
		Output.LightingFactor = dot(outNormal, -xLightDirection);
		Output.MoonLight = dot(outNormal, -xMoonDirection);
		float4 fogColor = float4(1.0f, 1.0f, 1.0f, 1.0f);
	
		//Output.Color=lerp(Output.Color,fogColor,((Output.Position.z/40)*(Output.Position.z/50)));	//Output.Fog=pow(Output.Position.z,1.0f)/100;//Output.Position.z*Output.Position.z/1000;
	Output.Fog=pow(Output.clipDistances/125.0f,1.0f);
	Output.TW=inWeights;
	return Output;    
}

PixelToFrame TexturedTintedPS(VertexToPixel PSIn) 
{
	PixelToFrame Output = (PixelToFrame)0;		
	//this takes the texture color(assumed grayscale), and puts it on a scale from -1 to 1.
	//the value is multiplied by vertex colour to determine how much is added to the vertex colour.
	//middle gray gives 0, resulting in vertex colour, white doubles the colour (in most cases whiting out) and black results in black.
	if (Clipping)
    clip(PSIn.clipDistances);
	
	
	if(PSIn.Fog>1.0f)
	PSIn.Fog=1.0f;
	float dividefactor =1.0f/8.0f; //0.0078125f;
		float4 waterColor = float4(0.0f, 0.400f, 0.10, 1.0f);
		float4 GrassColor= (tex2D(GrassSampler, PSIn.TextureCoords)*1*PSIn.Color)+(tex2D(GrassSampler, PSIn.TextureCoords*dividefactor)*1*PSIn.Color);
		//GrassColor = (((tex2D(TextureSampler, PSIn.TextureCoords)*2)-1)*PSIn.Color+PSIn.Color/2)+(((tex2D(TextureSampler, PSIn.TextureCoords*4.0f)*2)-1)*PSIn.Color+PSIn.Color/2);
		float4 RockColor=tex2D(RockSampler,PSIn.TextureCoords/8.0f);
		float4 SandColor= (tex2D(SandSampler, PSIn.TextureCoords/16.0f));
	/*
	GrassColor =(tex2D(GrassSampler, PSIn.TextureCoords)*1*PSIn.Color);
GrassColor +=(tex2D(GrassSampler, PSIn.TextureCoords*0.75f)*1*PSIn.Color)/2.0f;
GrassColor +=(tex2D(GrassSampler, PSIn.TextureCoords*0.5f)*1*PSIn.Color)/2.0f;
//*/
//float4 SandColor= ((tex2D(SandSampler, PSIn.TextureCoords)*2)-1)*PSIn.Color+PSIn.Color;

		float Derp=PSIn.TW.z;

		//fog
		Output.Color=lerp(GrassColor,SandColor,Derp);
		Output.Color=GrassColor;
		
		
		float slope=abs(PSIn.Normal.x)+abs(PSIn.Normal.z);
		//slope=1.0f-PSIn.Normal.y;
		//slope=pow(slope, 0.5f);
		Output.Color=lerp(GrassColor,RockColor,slope);
		Output.Color=GrassColor;
		if(slope>0.999f)
		Output.Color=RockColor;
		if(slope<0.999f && slope > 0.9f)
		{
			float slerp=(slope-0.9f)/10.0f*100.0f;
			Output.Color=lerp(GrassColor,RockColor,slerp);
		}
	
		if(xFog)
		Output.Color= lerp(Output.Color,waterColor,(PSIn.Fog));
		
Output.Color.rgb*= saturate(PSIn.LightingFactor);

	return Output;
}

technique TexturedTinted
{
	pass Pass0
	{   
		VertexShader = compile vs_4_0_level_9_1 TexturedTintedVS();
		PixelShader  = compile ps_4_0_level_9_1 TexturedTintedPS();
	}
}









//------- Technique: Pretransformed --------

VertexToPixel PretransformedVS( float4 inPos : POSITION, float4 inColor: COLOR)
{	
	VertexToPixel Output = (VertexToPixel)0;
	
	Output.Position = inPos;
	Output.Color = inColor;
    
	return Output;    
}

PixelToFrame PretransformedPS(VertexToPixel PSIn) 
{
	PixelToFrame Output = (PixelToFrame)0;		
	
	Output.Color = PSIn.Color;

	return Output;
}

technique Pretransformed
{
	pass Pass0
	{   
		VertexShader = compile vs_4_0_level_9_1 PretransformedVS();
		PixelShader  = compile ps_4_0_level_9_1 PretransformedPS();
	}
}

//------- Technique: PretransformedTextured --------

VertexToPixel PretransformedTexturedVS( float4 inPos : POSITION,  float2 inTexCoords: TEXCOORD0)
{	
	VertexToPixel Output = (VertexToPixel)0;
	
	Output.Position = inPos;
	//Output.Color = inColor;
    
	return Output;    
}

PixelToFrame PretransformedTexturedPS(VertexToPixel PSIn) 
{
	PixelToFrame Output = (PixelToFrame)0;		
	
	Output.Color = tex2D(TextureSampler, PSIn.TextureCoords);

	return Output;
}

technique PretransformedTextured
{
	pass Pass0
	{   
		VertexShader = compile vs_4_0_level_9_1 PretransformedTexturedVS();
		PixelShader  = compile ps_4_0_level_9_1 PretransformedTexturedPS();
	}
}
//------- Technique: Colored --------

VertexToPixel ColoredVS( float4 inPos : POSITION, float3 inNormal: NORMAL, float4 inColor: COLOR)
{	
	VertexToPixel Output = (VertexToPixel)0;
	float4x4 preViewProjection = mul (xView, xProjection);
	float4x4 preWorldViewProjection = mul (xWorld, preViewProjection);
    
	Output.Position = mul(inPos, preWorldViewProjection);
	Output.Color = inColor;
	
	float3 Normal = normalize(mul(normalize(inNormal), xWorld));	
	Output.LightingFactor = 1;
	if (xEnableLighting)
		Output.LightingFactor = dot(Normal, -xLightDirection);
    
	return Output;    
}

PixelToFrame ColoredPS(VertexToPixel PSIn) 
{
	PixelToFrame Output = (PixelToFrame)0;		
    
	Output.Color = PSIn.Color;
	Output.Color.rgb *= saturate(PSIn.LightingFactor) + xAmbient;

	return Output;
}

technique Colored
{
	pass Pass0
	{   
		VertexShader = compile vs_4_0_level_9_1 ColoredVS();
		PixelShader  = compile ps_4_0_level_9_1 ColoredPS();
	}
}

//------- Technique: ColoredNoShading --------

VertexToPixel ColoredNoShadingVS( float4 inPos : POSITION, float4 inColor: COLOR)
{	
	VertexToPixel Output = (VertexToPixel)0;
	float4x4 preViewProjection = mul (xView, xProjection);
	float4x4 preWorldViewProjection = mul (xWorld, preViewProjection);
    
	Output.Position = mul(inPos, preWorldViewProjection);
	Output.Color = inColor;
    
	Output.clipDistances = dot(inPos, ClipPlane0);
	return Output;    
}

PixelToFrame ColoredNoShadingPS(VertexToPixel PSIn) 
{
	PixelToFrame Output = (PixelToFrame)0;		
    
	Output.Color = PSIn.Color;
	if (Clipping)
    clip(PSIn.clipDistances);
	return Output;
}

technique ColoredNoShading
{
	pass Pass0
	{   
		VertexShader = compile vs_4_0_level_9_1 ColoredNoShadingVS();
		PixelShader  = compile ps_4_0_level_9_1 ColoredNoShadingPS();
	}
}
//--instanced
VertexToPixel ColoredNoShadingInstancedVS( float4 inPos : POSITION, float4 inColor: COLOR)
{	
	VertexToPixel Output = (VertexToPixel)0;
	float4x4 preViewProjection = mul (xView, xProjection);
	float4x4 preWorldViewProjection = mul (xWorld, preViewProjection);
    
	Output.Position = mul(inPos, preWorldViewProjection);
	Output.Color = inColor;
    
	Output.clipDistances = dot(inPos, ClipPlane0);
	return Output;    
}

PixelToFrame ColoredNoShadingInstancedPS(VertexToPixel PSIn) 
{
	PixelToFrame Output = (PixelToFrame)0;		
    
	Output.Color = PSIn.Color;
	if (Clipping)
    clip(PSIn.clipDistances);
	return Output;
}

technique ColoredNoShadingInstanced
{
	pass Pass0
	{   
		VertexShader = compile vs_4_0_level_9_1 ColoredNoShadingInstancedVS();
		PixelShader  = compile ps_4_0_level_9_1 ColoredNoShadingInstancedPS();
	}
}


//------- Technique: Textured --------

VertexToPixel TexturedVS( float4 inPos : POSITION, float3 inNormal: NORMAL, float2 inTexCoords: TEXCOORD0)
{	
	VertexToPixel Output = (VertexToPixel)0;
	float4x4 preViewProjection = mul (xView, xProjection);
	float4x4 preWorldViewProjection = mul (xWorld, preViewProjection);
    
	Output.Position = mul(inPos, preWorldViewProjection);	
	Output.TextureCoords = inTexCoords;
	
	float3 Normal = normalize(mul(normalize(inNormal), xWorld));	
	Output.LightingFactor = 1;
	if (xEnableLighting)
		Output.LightingFactor = dot(Normal, -xLightDirection);
    
	return Output;    
}

PixelToFrame TexturedPS(VertexToPixel PSIn) 
{
	PixelToFrame Output = (PixelToFrame)0;		
	
	Output.Color = tex2D(TextureSampler, PSIn.TextureCoords);
	Output.Color.rgb *= saturate(PSIn.LightingFactor) + xAmbient;

	return Output;
}

technique Textured
{
	pass Pass0
	{   
		VertexShader = compile vs_4_0_level_9_1 TexturedVS();
		PixelShader  = compile ps_4_0_level_9_1 TexturedPS();
	}
}

//------- Technique: TexturedNoShading --------

VertexToPixel TexturedNoShadingVS( float4 inPos : POSITION, float3 inNormal: NORMAL, float2 inTexCoords: TEXCOORD0)
{	
	VertexToPixel Output = (VertexToPixel)0;
	float4x4 preViewProjection = mul (xView, xProjection);
	float4x4 preWorldViewProjection = mul (xWorld, preViewProjection);
    
	Output.Position = mul(inPos, preWorldViewProjection);	
	Output.TextureCoords = inTexCoords;
    
	return Output;    
}

PixelToFrame TexturedNoShadingPS(VertexToPixel PSIn) 
{
	PixelToFrame Output = (PixelToFrame)0;		
	
	Output.Color = tex2D(TextureSampler, PSIn.TextureCoords);

	return Output;
}

technique TexturedNoShading
{
	pass Pass0
	{   
		VertexShader = compile vs_4_0_level_9_1 TexturedNoShadingVS();
		PixelShader  = compile ps_4_0_level_9_1 TexturedNoShadingPS();
	}
}

//------- Technique: PointSprites --------

VertexToPixel PointSpriteVS( float4 inPos : POSITION,  float2 inTexCoord: TEXCOORD0,float4 inColor: COLOR, float2 inWeights: TEXCOORD1)
{
    VertexToPixel Output = (VertexToPixel)0;

    float3 center = mul(inPos, xWorld);
    float3 eyeVector = center - xCamPos;

    float3 sideVector = cross(eyeVector,xCamUp);
    sideVector = normalize(sideVector);
    float3 upVector = cross(sideVector,eyeVector);
    upVector = normalize(upVector);

    float3 finalPosition = center;
    finalPosition += (inTexCoord.x-0.5f)*sideVector*0.5f*xPointSpriteSize;
    finalPosition += (0.5f-inTexCoord.y)*upVector*0.5f*xPointSpriteSize;

    float4 finalPosition4 = float4(finalPosition, 1);

    float4x4 preViewProjection = mul (xView, xProjection);
    Output.Position = mul(finalPosition4, preViewProjection);

    Output.TextureCoords = inTexCoord;

	Output.Color=inColor;
	
    return Output;
}

PixelToFrame PointSpritePS(VertexToPixel PSIn) : COLOR0
{
    PixelToFrame Output = (PixelToFrame)0;
    float4 texcolor = tex2D(TextureSampler, PSIn.TextureCoords);
	float4 shade=float4(0.5f,0.5f,0.5f,0.5f);
	
	Output.Color=texcolor;
	if(texcolor.r==texcolor.g && texcolor.g==texcolor.b)
	{
	Output.Color=PSIn.Color+(texcolor-shade)*2;
	if(texcolor.r<0.5f)
	 Output.Color=texcolor*PSIn.Color*2;
	}
	
    return Output;
}

technique PointSprites
{
	pass Pass0
	{   
		VertexShader = compile vs_4_0_level_9_1 PointSpriteVS();
		PixelShader  = compile ps_4_0_level_9_1 PointSpritePS();
	}
}
//tech: overviewmap
VertexToPixel OverHeadMapVS( float4 inPos : POSITION,  float2 inTexCoords: TEXCOORD0, float4 inNormal : NORMAL,float4 inColor: COLOR)
{
  	VertexToPixel Output = (VertexToPixel)0;
	Output.Position = inPos;
	Output.Position =mul(inPos, xWorld);
		//mul(inPos, preWorldViewProjection);	
		float4 dullColor = float4(0.3f, 0.6f, 0.5f, 1.0f);
	Output.TextureCoords = inTexCoords;
    Output.Color = inColor;

	if(Output.Position.z<(2.9f/(64.0f*xZoomLevel)))
	  Output.Color = lerp(inColor,dullColor,0.8f);
	Output.Position.z=0;
	return Output;
}
PixelToFrame OverHeadMapPS(VertexToPixel PSIn) 
{
	PixelToFrame Output = (PixelToFrame)0;	

	Output.Color=PSIn.Color;	
	//Output.Color = ((tex2D(TextureSampler, PSIn.TextureCoords)*2)-1)*PSIn.Color+PSIn.Color;
	return Output;
	}

	Technique OverHeadMap
	{
	pass Pass0
	{   
		VertexShader = compile vs_4_0_level_9_1 OverHeadMapVS();
		PixelShader  = compile ps_4_0_level_9_1 OverHeadMapPS();
	}
}
//------- Technique: Water --------
struct WVertexToPixel
{
    float4 Position                 : POSITION;
    float4 ReflectionMapSamplingPos    : TEXCOORD1;
    float2 BumpMapSamplingPos        : TEXCOORD2;

     float4 RefractionMapSamplingPos : TEXCOORD3;
     float4 Position3D                : TEXCOORD4;

};

struct WPixelToFrame
{
     float4 Color : COLOR0;
};

WVertexToPixel WaterVS(float4 inPos : POSITION, float2 inTex: TEXCOORD)
{    
     WVertexToPixel Output = (WVertexToPixel)0;

     float4x4 preViewProjection = mul (xView, xProjection);
     float4x4 preWorldViewProjection = mul (xWorld, preViewProjection);
     float4x4 preReflectionViewProjection = mul (xReflectionView, xProjection);
     float4x4 preWorldReflectionViewProjection = mul (xWorld, preReflectionViewProjection);

     Output.Position = mul(inPos, preWorldViewProjection);
     Output.ReflectionMapSamplingPos = mul(inPos, preWorldReflectionViewProjection);
     Output.RefractionMapSamplingPos = mul(inPos, preWorldViewProjection);
	 Output.Position3D = mul(inPos, xWorld);
	float2 moveVector = float2(0, xTime*xWindForce);
Output.BumpMapSamplingPos = (inTex + moveVector)/xWaveLength;
// Output.Fog=Output.Position.z/100;
	
     return Output;
}

WPixelToFrame WaterPS(WVertexToPixel PSIn)
{
     WPixelToFrame Output = (WPixelToFrame)0;   
	// xWaveHeight=1;     
	float4 bumpColor = tex2D(WaterBumpMapSampler, PSIn.BumpMapSamplingPos);
     float2 perturbation =xWaveHeight*(bumpColor.rg - 0.5f)*2.0f;
	//perturbation = float2(0, 0);
     float2 ProjectedTexCoords;
	 //PSIn.ReflectionMapSamplingPos.w = 1.0f;
     ProjectedTexCoords.x = PSIn.ReflectionMapSamplingPos.x/PSIn.ReflectionMapSamplingPos.w/2.0f + 0.5f;
     ProjectedTexCoords.y = -PSIn.ReflectionMapSamplingPos.y/PSIn.ReflectionMapSamplingPos.w/2.0f + 0.5f;    
float2 ProjectedTexCoords2;
     ProjectedTexCoords2.x = PSIn.RefractionMapSamplingPos.x/PSIn.RefractionMapSamplingPos.w/2.0f + 0.5f;
     ProjectedTexCoords2.y = -PSIn.RefractionMapSamplingPos.y/PSIn.RefractionMapSamplingPos.w/2.0f + 0.5f;    

	 float3 eyeVector = normalize(xCamPos - PSIn.Position3D);
	 //eyeVector = normalize( PSIn.Position3D- xCamPos);
	 float3 normalVector = float3(0,1,0);
	 float fresnelTerm = dot(eyeVector, normalVector);
	 Output.Color = lerp(tex2D(RefractionSampler, ProjectedTexCoords2 + perturbation), tex2D(ReflectionSampler, ProjectedTexCoords+perturbation), clamp(0.7f-fresnelTerm,0,1));
    // Output.Color =/2 + /2;    
    //Output.Color.Alpha=0.5f;
	//float4 dullColor = float4(0.3f, 0.3f, 0.5f, 1.0f);
	float4 dullColor = float4(0.3f, 0.6f, 0.5f, 1.0f);
			float4 fogColor = float4(1.0f, 1.0f, 1.0f, 1.0f);
	//Output.Color=lerp(Output.Color,fogColor,PSIn.Fog);

	//Output.Color = lerp(Output.Color, dullColor, 0.8f);
//Output.Color= tex2D(ReflectionSampler, ProjectedTexCoords);
     return Output;
}

technique Water
{
     pass Pass0
     {
         VertexShader = compile vs_4_0_level_9_3 WaterVS();
         PixelShader = compile ps_4_0_level_9_3 WaterPS();
     }
}



VertexToPixel GameModelVS( float4 inPos : POSITION,  float2 inTexCoords: TEXCOORD0,float4 inColor: COLOR, float2 inWeights: TEXCOORD1)
{	
	VertexToPixel Output = (VertexToPixel)0;
	float4x4 preViewProjection = mul (xView, xProjection);
	float4x4 preWorldViewProjection = mul (xWorld, preViewProjection); 
	Output.Position=mul(inPos,lerp(xOrigin,xBone,inWeights.x));
	Output.TextureCoords = inTexCoords;
    Output.Color = inColor;
	Output.clipDistances = dot(mul(inPos,xWorld), ClipPlane0);
	Output.Position = mul(Output.Position, preWorldViewProjection);	
	
	
	
	



	float4 fogColor = float4(1.0f, 1.0f, 1.0f, 1.0f);
	Output.Fog=pow(Output.Position.z,1.0f)/100;
	//Output.Position.z*Output.Position.z/1000;
	Output.Fog=pow(Output.clipDistances/125.0f,1.0f);
	//Output.TW=inWeights;
	return Output;    
}

PixelToFrame GameModelPS(VertexToPixel PSIn) 
{
	PixelToFrame Output = (PixelToFrame)0;		
	//this takes the texture color(assumed grayscale), and puts it on a scale from -1 to 1.
	//the value is multiplied by vertex colour to determine how much is added to the vertex colour.
	//middle gray gives 0, resulting in vertex colour, white doubles the colour (in most cases whiting out) and black results in black.
	if (Clipping)
    clip(PSIn.clipDistances);
	
	
	if(PSIn.Fog>1.0f)
	PSIn.Fog=1.0f;
	float4 watercolor=float4(0.0f,0.4f,0.1f,1.0f);
	Output.Color =PSIn.Color;
	
	
	
	
	
	
	
	
	
	
	
	
	if(xFog)
		Output.Color=lerp(Output.Color,watercolor,(PSIn.Fog/1.0f));
		
		

	return Output;
}

technique GameModel
{
	pass Pass0
	{   
		VertexShader = compile vs_4_0_level_9_1 GameModelVS();
		PixelShader  = compile ps_4_0_level_9_1 GameModelPS();
	}
}





// TODO: add effect parameters here.

struct VertexShaderInput
{
    float4 Position : POSITION0;

    // TODO: add input channels such as texture
    // coordinates and vertex colors here.
};

struct VertexShaderOutput
{
    float4 Position : POSITION0;

    // TODO: add vertex shader outputs such as colors and texture
    // coordinates here. These values will automatically be interpolated
    // over the triangle, and provided as input to your pixel shader.
};

VertexShaderOutput VertexShaderFunction(VertexShaderInput input)
{
    VertexShaderOutput output;

    float4 worldPosition = mul(input.Position, World);
    float4 viewPosition = mul(worldPosition, View);
    output.Position = mul(viewPosition, Projection);

    // TODO: add your vertex shader code here.

    return output;
}

float4 PixelShaderFunction(VertexShaderOutput input) : COLOR0
{
    // TODO: add your pixel shader code here.

    return float4(1, 0, 0, 1);
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
