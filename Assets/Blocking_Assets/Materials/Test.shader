// Shader created with Shader Forge v1.04 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.04;sub:START;pass:START;ps:flbk:,lico:1,lgpr:1,nrmq:1,limd:1,uamb:True,mssp:True,lmpd:False,lprd:False,rprd:False,enco:False,frtr:True,vitr:True,dbil:False,rmgx:True,rpth:0,hqsc:True,hqlp:False,tesm:0,blpr:0,bsrc:0,bdst:1,culm:0,dpts:2,wrdp:True,dith:2,ufog:True,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,ofsf:0,ofsu:0,f2p0:False;n:type:ShaderForge.SFN_Final,id:6972,x:32719,y:32712,varname:node_6972,prsc:2|diff-9144-RGB,diffpow-1987-OUT,spec-2321-RGB,gloss-5959-G,normal-2614-RGB;n:type:ShaderForge.SFN_Tex2d,id:9144,x:32344,y:32708,ptovrint:False,ptlb:Diff,ptin:_Diff,varname:node_9144,prsc:2,tex:870cb6a3913a73949833fce3baa3553a,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Tex2d,id:2614,x:32326,y:32920,ptovrint:False,ptlb:node_2614,ptin:_node_2614,varname:node_2614,prsc:2,tex:c96d09cfa2cc58f4cb0c3bfefb0df09a,ntxv:3,isnm:True;n:type:ShaderForge.SFN_Vector1,id:1987,x:32517,y:32640,varname:node_1987,prsc:2,v1:1;n:type:ShaderForge.SFN_Tex2d,id:5959,x:32477,y:33045,ptovrint:False,ptlb:node_5959,ptin:_node_5959,varname:node_5959,prsc:2,tex:e4c11273f178b6c488edb53ad5d26bd2,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Tex2d,id:2321,x:32498,y:32494,ptovrint:False,ptlb:node_2321,ptin:_node_2321,varname:node_2321,prsc:2,tex:77e56217b64ea1c4985e70d4affb62dd,ntxv:0,isnm:False;proporder:9144-2614-2321-5959;pass:END;sub:END;*/

Shader "Custom/Test" {
    Properties {
        _Diff ("Diff", 2D) = "white" {}
        _node_2614 ("node_2614", 2D) = "bump" {}
        _node_2321 ("node_2321", 2D) = "white" {}
        _node_5959 ("node_5959", 2D) = "white" {}
    }
    SubShader {
        Tags {
            "RenderType"="Opaque"
        }
        LOD 200
        Pass {
            Name "ForwardBase"
            Tags {
                "LightMode"="ForwardBase"
            }
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma exclude_renderers xbox360 ps3 flash d3d11_9x 
            #pragma target 3.0
            uniform float4 _LightColor0;
            uniform sampler2D _Diff; uniform float4 _Diff_ST;
            uniform sampler2D _node_2614; uniform float4 _node_2614_ST;
            uniform sampler2D _node_5959; uniform float4 _node_5959_ST;
            uniform sampler2D _node_2321; uniform float4 _node_2321_ST;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                float3 tangentDir : TEXCOORD3;
                float3 binormalDir : TEXCOORD4;
                LIGHTING_COORDS(5,6)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = mul(_Object2World, float4(v.normal,0)).xyz;
                o.tangentDir = normalize( mul( _Object2World, float4( v.tangent.xyz, 0.0 ) ).xyz );
                o.binormalDir = normalize(cross(o.normalDir, o.tangentDir) * v.tangent.w);
                o.posWorld = mul(_Object2World, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            fixed4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3x3 tangentTransform = float3x3( i.tangentDir, i.binormalDir, i.normalDir);
/////// Vectors:
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 _node_2614_var = UnpackNormal(tex2D(_node_2614,TRANSFORM_TEX(i.uv0, _node_2614)));
                float3 normalLocal = _node_2614_var.rgb;
                float3 normalDirection = normalize(mul( normalLocal, tangentTransform )); // Perturbed normals
                float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
                float3 lightColor = _LightColor0.rgb;
                float3 halfDirection = normalize(viewDirection+lightDirection);
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 attenColor = attenuation * _LightColor0.xyz;
///////// Gloss:
                float4 _node_5959_var = tex2D(_node_5959,TRANSFORM_TEX(i.uv0, _node_5959));
                float gloss = _node_5959_var.g;
                float specPow = exp2( gloss * 10.0+1.0);
////// Specular:
                float NdotL = max(0, dot( normalDirection, lightDirection ));
                float4 _node_2321_var = tex2D(_node_2321,TRANSFORM_TEX(i.uv0, _node_2321));
                float3 specularColor = _node_2321_var.rgb;
                float3 directSpecular = (floor(attenuation) * _LightColor0.xyz) * pow(max(0,dot(halfDirection,normalDirection)),specPow);
                float3 specular = directSpecular * specularColor;
/////// Diffuse:
                NdotL = max(0.0,dot( normalDirection, lightDirection ));
                float3 indirectDiffuse = float3(0,0,0);
                float3 directDiffuse = pow(max( 0.0, NdotL), 1.0) * attenColor;
                indirectDiffuse += UNITY_LIGHTMODEL_AMBIENT.rgb; // Ambient Light
                float4 _Diff_var = tex2D(_Diff,TRANSFORM_TEX(i.uv0, _Diff));
                float3 diffuse = (directDiffuse + indirectDiffuse) * _Diff_var.rgb;
/// Final Color:
                float3 finalColor = diffuse + specular;
                return fixed4(finalColor,1);
            }
            ENDCG
        }
        Pass {
            Name "ForwardAdd"
            Tags {
                "LightMode"="ForwardAdd"
            }
            Blend One One
            
            
            Fog { Color (0,0,0,0) }
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDADD
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #pragma multi_compile_fwdadd_fullshadows
            #pragma exclude_renderers xbox360 ps3 flash d3d11_9x 
            #pragma target 3.0
            uniform float4 _LightColor0;
            uniform sampler2D _Diff; uniform float4 _Diff_ST;
            uniform sampler2D _node_2614; uniform float4 _node_2614_ST;
            uniform sampler2D _node_5959; uniform float4 _node_5959_ST;
            uniform sampler2D _node_2321; uniform float4 _node_2321_ST;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                float3 tangentDir : TEXCOORD3;
                float3 binormalDir : TEXCOORD4;
                LIGHTING_COORDS(5,6)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = mul(_Object2World, float4(v.normal,0)).xyz;
                o.tangentDir = normalize( mul( _Object2World, float4( v.tangent.xyz, 0.0 ) ).xyz );
                o.binormalDir = normalize(cross(o.normalDir, o.tangentDir) * v.tangent.w);
                o.posWorld = mul(_Object2World, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            fixed4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3x3 tangentTransform = float3x3( i.tangentDir, i.binormalDir, i.normalDir);
/////// Vectors:
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 _node_2614_var = UnpackNormal(tex2D(_node_2614,TRANSFORM_TEX(i.uv0, _node_2614)));
                float3 normalLocal = _node_2614_var.rgb;
                float3 normalDirection = normalize(mul( normalLocal, tangentTransform )); // Perturbed normals
                float3 lightDirection = normalize(lerp(_WorldSpaceLightPos0.xyz, _WorldSpaceLightPos0.xyz - i.posWorld.xyz,_WorldSpaceLightPos0.w));
                float3 lightColor = _LightColor0.rgb;
                float3 halfDirection = normalize(viewDirection+lightDirection);
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 attenColor = attenuation * _LightColor0.xyz;
///////// Gloss:
                float4 _node_5959_var = tex2D(_node_5959,TRANSFORM_TEX(i.uv0, _node_5959));
                float gloss = _node_5959_var.g;
                float specPow = exp2( gloss * 10.0+1.0);
////// Specular:
                float NdotL = max(0, dot( normalDirection, lightDirection ));
                float4 _node_2321_var = tex2D(_node_2321,TRANSFORM_TEX(i.uv0, _node_2321));
                float3 specularColor = _node_2321_var.rgb;
                float3 directSpecular = attenColor * pow(max(0,dot(halfDirection,normalDirection)),specPow);
                float3 specular = directSpecular * specularColor;
/////// Diffuse:
                NdotL = max(0.0,dot( normalDirection, lightDirection ));
                float3 directDiffuse = pow(max( 0.0, NdotL), 1.0) * attenColor;
                float4 _Diff_var = tex2D(_Diff,TRANSFORM_TEX(i.uv0, _Diff));
                float3 diffuse = directDiffuse * _Diff_var.rgb;
/// Final Color:
                float3 finalColor = diffuse + specular;
                return fixed4(finalColor * 1,0);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
