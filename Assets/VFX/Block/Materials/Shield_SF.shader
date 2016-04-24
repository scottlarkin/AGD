// Shader created with Shader Forge v1.04 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.04;sub:START;pass:START;ps:flbk:,lico:1,lgpr:1,nrmq:1,limd:1,uamb:True,mssp:True,lmpd:False,lprd:False,rprd:False,enco:False,frtr:True,vitr:True,dbil:False,rmgx:True,rpth:0,hqsc:True,hqlp:False,tesm:0,blpr:1,bsrc:3,bdst:7,culm:0,dpts:2,wrdp:False,dith:2,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,ofsf:0,ofsu:0,f2p0:False;n:type:ShaderForge.SFN_Final,id:9153,x:33001,y:32849,varname:node_9153,prsc:2|emission-2613-OUT,alpha-7843-A;n:type:ShaderForge.SFN_Tex2d,id:7843,x:32519,y:33022,ptovrint:False,ptlb:node_7843,ptin:_node_7843,varname:node_7843,prsc:2,tex:d1dad8334b1e14640af734f6bb48cb05,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Multiply,id:2613,x:32737,y:32889,varname:node_2613,prsc:2|A-4991-OUT,B-7843-RGB;n:type:ShaderForge.SFN_Vector3,id:4991,x:32519,y:32813,varname:node_4991,prsc:2,v1:0.3676471,v2:1,v3:1;proporder:7843;pass:END;sub:END;*/

Shader "Shader Forge/Shield_SF" {
    Properties {
        _node_7843 ("node_7843", 2D) = "white" {}
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent"
            "RenderType"="Transparent"
        }
        Pass {
            Name "ForwardBase"
            Tags {
                "LightMode"="ForwardBase"
            }
            Blend SrcAlpha OneMinusSrcAlpha
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase
            #pragma exclude_renderers xbox360 ps3 flash d3d11_9x 
            #pragma target 3.0
            uniform sampler2D _node_7843; uniform float4 _node_7843_ST;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
                return o;
            }
            fixed4 frag(VertexOutput i) : COLOR {
/////// Vectors:
////// Lighting:
////// Emissive:
                float4 _node_7843_var = tex2D(_node_7843,TRANSFORM_TEX(i.uv0, _node_7843));
                float3 emissive = (float3(0.3676471,1,1)*_node_7843_var.rgb);
                float3 finalColor = emissive;
                return fixed4(finalColor,_node_7843_var.a);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
