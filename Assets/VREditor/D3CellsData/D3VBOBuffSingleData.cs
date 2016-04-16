using LibVRGeometry;
using System;
using UnityEngine;

namespace U3DSceneEditor
{
    [Serializable]
    public class D3VBOBuffSingleData : D3DataBase
    {
        /// <summary>
        /// 网络数据
        /// </summary>
        VBOBufferSingle mVbo;

        ObjModelRawAnly oAnly = null;
        ObjModelRawRender oRender = null;

        GameObject mGameObject = null;

        public D3VBOBuffSingleData(GameObject root)
        {
            oAnly = new ObjModelRawAnly();
            oRender = new ObjModelRawRender(root);
        }


        /// <summary>
        /// 创建对象
        /// </summary>
        /// <param name="omr"></param>
        public void CreateVBOBuffer(VBOBufferSingle vbo)
        {
            mVbo = vbo;

            float start = Time.realtimeSinceStartup;

            oAnly.AnlyVBOBufferSingle(vbo);

            oRender.BuildGameObject(ref oAnly);
            oRender.BuildMesh(ref oAnly);
            Smooth smooth = new Smooth(oAnly.buffer);
            oAnly.buffer = smooth.Exe_GeometryBuffer();
            oRender.Deformation(ref oAnly);

            Debuger.Log("Create " + (Time.realtimeSinceStartup - start) / 1000.0f / 1000.0f + " s");
        }

        public void ModifyVBOBuffer(VBOBufferSingle vbo)
        {
            mVbo = vbo;

            float start = Time.realtimeSinceStartup;

            oAnly.AnlyVBOBufferSingle(vbo);

            oRender.BuildMesh(ref oAnly);
            Smooth smooth = new Smooth(oAnly.buffer);
            oAnly.buffer = smooth.Exe_GeometryBuffer();
            oRender.Deformation(ref oAnly);

            Debuger.Log("Modify " + (Time.realtimeSinceStartup - start) / 1000.0f / 1000.0f + " s");
        }

        public void DestoryVBOBuffer(VBOBufferSingle vbo)
        {
            mVbo = null;

            oRender.Destory();
        }
    }
}
