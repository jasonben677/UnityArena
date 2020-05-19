Shader "Custom/Dissolve"
{
    Properties
    {
        _Tint ("Tint", Color) = (1,1,1,1)
        _Step ("Step", Range(0, 1)) = 0
        _value ("Value", Range(0 , 1)) = 0.1
        _Intensity ("Intensity", Range(0,1)) = 0.05
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _dissolveTex ("Dissolve (RGB)", 2D) = "white" {}
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard fullforwardshadows

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        sampler2D _MainTex;
        sampler2D _dissolveTex;

        float _Step;
        float _value;
        float _Intensity;

        struct Input
        {
            float2 uv_MainTex;
        };

        half _Glossiness;
        half _Metallic;
        fixed4 _Tint;

        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            fixed4 d = tex2D (_dissolveTex, IN.uv_MainTex);
            // Albedo comes from a texture tinted by color
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex);
            
            float mask = d.r - _Step;

            if (mask < _value)
            {
              c = c * _Tint * _Intensity / mask;
            }
            
            clip(mask);
                        
            o.Albedo = c.rgb;
            // Metallic and smoothness come from slider variables
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
            o.Alpha = c.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
