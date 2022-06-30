Shader "Masked/Mask" 
{
 
	SubShader 
	{
		
		Tags {"Queue" = "Transparent+1" }
 
		
		// Do nothing specific in the pass:
 
		Pass 
		{
			Blend Zero One			
		}
	}
}