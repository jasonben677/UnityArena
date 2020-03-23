﻿// Upgrade NOTE: replaced 'SeperateSpecular' with 'SeparateSpecular'

Shader "Vertex Simple"
{
	Properties{
		_MainTex("Main Texture", 2D) = "white"{}
	_MainTex2("Main Texture2", 2D) = "white"{}
	//_MainTex("Main Texture", 2D) = "white"{}
		_Color("Main Color", COLOR) = (1,1,1,1)
		_Spec("Spec Color", COLOR) = (1,0,0,1)
		_Shi("Shininess", Float) = 1.0
			_Cutoff("Cut off", Float) = 0.0
	}
		SubShader
		{
	 Tags { "Queue" = "Transparent" }
			Pass {
			   Cull off
			  //Blend Off
		       AlphaTest Greater [_Cutoff]
			   Material {
				 Diffuse[_Color]
				 Ambient[_Color]
				 Specular[_Spec]
				 Shininess[_Shi]
			   }
			   Lighting On
			   SeparateSpecular On

			   SetTexture[_MainTex]
			   {
				  combine  texture
				}
	
           }
			Pass {
			   Cull off
			   ZTest Less
			   ZWrite Off
			    Blend SrcAlpha OneMinusSrcAlpha
				AlphaTest LEqual [_Cutoff]
				Material {
				  Diffuse[_Color]
				  Ambient[_Color]
				  Specular[_Spec]
				  Shininess[_Shi]
				}
				Lighting On
				SeparateSpecular On

				SetTexture[_MainTex]
				{
				   combine  texture
				 }

			}
	    }
}
