using UnityEngine;
using System.Collections;
using LibVRGeometry;

namespace U3DSceneEditor
{
    public class D3VBOBuffSingle : D3Object, IFingerControl
    {
        [SerializeField]
        public D3VBOBuffSingleData mD3VBOBuffSingleData;

        override public void InitData(D3DataBase data)
        {
            base.InitData(data);

            bDrag = true;
            bSelection = true;
            bRotation = true;

            //
            mD3VBOBuffSingleData = (D3VBOBuffSingleData)mData;
        }

        override public void UpdateData()
        {
            base.UpdateData();

            if (m_sel)
            {
            }
        }

        #region Render
        ObjModelRawAnly oAnly = new ObjModelRawAnly();

        public void Update(VBOBufferSingle omr)
        {
            switch (omr.state)
            {
                case MessageState.Null:
                    break;
                case MessageState.Create:
                    CreateVBOBuffer(omr);
                    break;
                case MessageState.Update:
                    ModifyVBOBuffer(omr);
                    break;
                case MessageState.Destory:
                    DestoryVBOBuffer(omr);
                    break;
            }

        }

        /// <summary>
        /// 创建对象
        /// </summary>
        /// <param name="omr"></param>
        ObjModelRawRender CreateVBOBuffer(VBOBufferSingle vbo)
        {
            float start = Time.realtimeSinceStartup;

            oAnly.AnlyVBOBufferSingle(vbo);

            ObjModelRawRender r = new ObjModelRawRender();
            r.BuildGameObject(ref oAnly);
            r.BuildMesh(ref oAnly);
            Smooth smooth = new Smooth(oAnly.buffer);
            oAnly.buffer = smooth.Exe_GeometryBuffer();
            r.Deformation(ref oAnly);

            //mObjModelRawAnly.Add(vbo.id, r);

            Debuger.Log("Create " + (Time.realtimeSinceStartup - start) / 1000.0f / 1000.0f + " s");

            return r;
        }

        void ModifyVBOBuffer(VBOBufferSingle vbo)
        {
            float start = Time.realtimeSinceStartup;

            oAnly.AnlyVBOBufferSingle(vbo);

            ObjModelRawRender r = (ObjModelRawRender)mObjModelRawAnly[vbo.id];

            r.BuildMesh(ref oAnly);
            Smooth smooth = new Smooth(oAnly.buffer);
            oAnly.buffer = smooth.Exe_GeometryBuffer();
            r.Deformation(ref oAnly);

            Debuger.Log("Modify " + (Time.realtimeSinceStartup - start) / 1000.0f / 1000.0f + " s");
        }

        void DestoryVBOBuffer(VBOBufferSingle vbo)
        {
            ObjModelRawRender r = mObjModelRawAnly[vbo.id];

            if (r != null)
            {
                r.Destory();

                mObjModelRawAnly.Remove(vbo.id);
            }
        }
        #endregion
    }
}