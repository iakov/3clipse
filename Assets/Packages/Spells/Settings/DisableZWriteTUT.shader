Shader "Custom/DisabledZWriteTUT"
{
    SubShader{
        Tags{
            "RenderType" = "Opaque"
        }

        Pass{
            ZWrite off 
        }
    }
}