using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using LibVRGeometry;

public class VBOBufferSingleMgr
{
    public Dictionary<int, ObjModelRawRender> mObjModelRawAnly = new Dictionary<int, ObjModelRawRender>();

    #region VBOBuffer
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
    void CreateVBOBuffer(VBOBufferSingle vbo)
    {
        float start = Time.realtimeSinceStartup;

        ObjModelRawAnly o = new ObjModelRawAnly(vbo);

        ObjModelRawRender r = new ObjModelRawRender();
        r.BuildGameObject(ref o);
        r.BuildMesh(ref o);
        Smooth smooth = new Smooth(o.buffer);
        o.buffer = smooth.Exe_GeometryBuffer();
        r.Deformation(ref o);

        mObjModelRawAnly.Add(vbo.id, r);

        Debuger.Log("Create " + (Time. realtimeSinceStartup - start) / 1000.0f / 1000.0f + " s");
    }

    void ModifyVBOBuffer(VBOBufferSingle vbo)
    {
        float start = Time.realtimeSinceStartup;

        ObjModelRawAnly o = new ObjModelRawAnly(vbo);


        ObjModelRawRender r = (ObjModelRawRender)mObjModelRawAnly[vbo.id];

        r.BuildMesh(ref o);
        Smooth smooth = new Smooth(o.buffer);
        o.buffer = smooth.Exe_GeometryBuffer();
        r.Deformation(ref o);

        Debuger.Log("Modify " + (Time.realtimeSinceStartup - start) / 1000.0f / 1000.0f + " s");
    }

    void DestoryVBOBuffer(VBOBufferSingle vbo)
    {
        ObjModelRawRender r = mObjModelRawAnly[vbo.id];

        if ( r != null )
        {
            r.Destory();

            mObjModelRawAnly.Remove(vbo.id);
        }
    }
    #endregion
}
