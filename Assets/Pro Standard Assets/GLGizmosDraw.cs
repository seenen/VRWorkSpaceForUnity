#if WIN32
#else
using UnityEngine;
#endif

using System.Collections;
using System.Collections.Generic;

namespace U3DSceneEditor
{  
    public class GLGizmosDraw  
    {  
#if WIN32

#else
        private static Material mMaterial = null;  
		private static void CreateMaterial()
		{
			if(mMaterial == null)
				mMaterial = new Material("Shader \"Lines/Colored Blended\" {" +
								            "SubShader { Pass { " +
								            "    Blend SrcAlpha OneMinusSrcAlpha " +
								            "    ZWrite Off Cull Off Fog { Mode Off } " +
								            "    BindChannels {" +
								            "      Bind \"vertex\", vertex Bind \"color\", color }" +
								            "} } }");
		}
        
		
		public static void DrawLine(Vector3 start, Vector3 end, Color color)  
        {  
			CreateMaterial();
            mMaterial.SetPass(0);  
            GL.Color(color);

            GL.Begin(GL.LINES);  
			
            GL.Vertex(start);  
            GL.Vertex(end);  
				
			GL.End();
        } 
		
		public static void DrawSphere(Vector3 pos, float radios, Color color)
		{
			CreateMaterial();
			mMaterial.SetPass(0);  
			GL.Color(color);
			
            GL.Begin(GL.LINES);
			
			Vector3 N = new Vector3(radios, 0, 0);
			
			int segments = 30;
			for(int i=0; i<segments; ++i)
			{
				Vector3 p1 = Quaternion.AngleAxis(360/segments*i, Vector3.up) * N;
				Vector3 p2 = Quaternion.AngleAxis(360/segments*(i+1),Vector3.up) * N;
				
				GL.Vertex(pos + p1);  
            	GL.Vertex(pos + p2);  
			}
			
			GL.End();
		}
		
		public static void DrawArc(Transform trans, float radios, float degree, Color color)
		{
			CreateMaterial();
			mMaterial.SetPass(0);  
			GL.Color(color);
			
            GL.Begin(GL.LINES);
			
			Vector3 N = trans.forward.normalized * radios;

			Vector3 pL = Quaternion.AngleAxis(-degree*0.5f, Vector3.up) * N;
			Vector3 pR = Quaternion.AngleAxis(degree*0.5f, Vector3.up) * N;
			
			GL.Vertex(trans.position);
			GL.Vertex(trans.position + pL);
			
            GL.Vertex(trans.position);
			GL.Vertex(trans.position + pR);
			
			for(float i=(-degree*0.5f); i<degree*0.5f -11; i += 12)
			{
				Vector3 p1 = Quaternion.AngleAxis(i, Vector3.up) * N;
				Vector3 p2 = Quaternion.AngleAxis(i+12, Vector3.up) * N;
				
				GL.Vertex(trans.position + p1);  
            	GL.Vertex(trans.position + p2);  
			}
			
			Vector3 pM = Quaternion.AngleAxis(degree*0.5f-12, Vector3.up) * N;
			GL.Vertex(trans.position + pM);
			GL.Vertex(trans.position + pR);
			
			GL.End();
		}
        
        public static void DrawCube(Transform trans, Vector3 size, Color color)  
        {  
            CreateMaterial();
			mMaterial.SetPass(0);  
			GL.Color(color);
			
			GL.PushMatrix();
			
			//Matrix4x4 m = new Matrix4x4();
			//{
			//	m = Matrix4x4.TRS(trans.position, trans.rotation, Vector3.one);
			//}
   //         GL.MultMatrix(m);
			
            GL.MultMatrix(trans.localToWorldMatrix);
            GL.Begin(GL.LINES);
			
			UnityEngine.Vector3 lt = new UnityEngine.Vector3(-size.x*0.5f, 0, size.y*0.5f);
			UnityEngine.Vector3 lb = new UnityEngine.Vector3(-size.x*0.5f, 0, -size.y*0.5f);
			UnityEngine.Vector3 rb = new UnityEngine.Vector3(size.x*0.5f, 0, -size.y*0.5f);
			UnityEngine.Vector3 rt = new UnityEngine.Vector3(size.x*0.5f, 0, size.y*0.5f);
						
			UnityEngine.Vector3 LT = new UnityEngine.Vector3(-size.x*0.5f, size.z, size.y*0.5f);
			UnityEngine.Vector3 LB = new UnityEngine.Vector3(-size.x*0.5f, size.z, -size.y*0.5f);
			UnityEngine.Vector3 RB = new UnityEngine.Vector3(size.x*0.5f, size.z, -size.y*0.5f);
			UnityEngine.Vector3 RT = new UnityEngine.Vector3(size.x*0.5f, size.z, size.y*0.5f);

            GL.Vertex(lt); GL.Vertex(lb);
            GL.Vertex(lb); GL.Vertex(rb);
            GL.Vertex(rb); GL.Vertex(rt);
            GL.Vertex(rt); GL.Vertex(lt);

            GL.Vertex(LT); GL.Vertex(LB);
			GL.Vertex(LB); GL.Vertex(RB);
			GL.Vertex(RB); GL.Vertex(RT);
			GL.Vertex(RT); GL.Vertex(LT);
			
			GL.Vertex(lt); GL.Vertex(LT);
			GL.Vertex(lb); GL.Vertex(LB);
			GL.Vertex(rb); GL.Vertex(RB);
			GL.Vertex(rt); GL.Vertex(RT);
			
			GL.End();
			
			GL.PopMatrix();
        }  
#endif
    }
}  

