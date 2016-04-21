using LibVRGeometry;
using LibVRGeometry.Message;
using System.Collections.Generic;
using UnityEngine;

public class ObjModelRawMgr
{
    public Dictionary<int, ObjModelRawRender> mObjModelRawAnly = new Dictionary<int, ObjModelRawRender>();

    ObjModelRawAnly o = new ObjModelRawAnly();

    #region VBOBuffer
    public void Update(VBOBuffer omr)
    {
        switch (omr.state)
        {
            case VBOState.Null:
                break;
            case VBOState.Create:
                CreateVBOBuffer(omr);
                break;
            case VBOState.Update:
                ModifyVBOBuffer(omr);
                break;
            case VBOState.Destory:
                DestoryVBOBuffer(omr);
                break;
        }

    }

    /// <summary>
    /// 创建对象
    /// </summary>
    /// <param name="omr"></param>
    void CreateVBOBuffer(VBOBuffer omr)
    {
        float start = Time.realtimeSinceStartup;

        o.AnlyVBOBuffer(omr);

        ObjModelRawRender r = new ObjModelRawRender(new GameObject(omr.id.ToString()));
        r.BuildGameObject(ref o);
        r.BuildMesh(ref o);
        Smooth smooth = new Smooth(o.buffer);
        o.buffer = smooth.Exe_GeometryBuffer();
        r.Deformation(ref o);

        mObjModelRawAnly.Add(omr.id, r);

        Debuger.Log("Create " + (Time.realtimeSinceStartup - start) / 1000.0f / 1000.0f + " s");
    }

    void ModifyVBOBuffer(VBOBuffer omr)
    {
        float start = Time.realtimeSinceStartup;

        o.AnlyVBOBuffer(omr);

        ObjModelRawRender r = (ObjModelRawRender)mObjModelRawAnly[omr.id];

        r.BuildMesh(ref o);
        Smooth smooth = new Smooth(o.buffer);
        o.buffer = smooth.Exe_GeometryBuffer();
        r.Deformation(ref o);

        Debuger.Log("Modify " + (Time.realtimeSinceStartup - start) / 1000.0f / 1000.0f + " s");
    }

    void DestoryVBOBuffer(VBOBuffer vbo)
    {
        ObjModelRawRender r = mObjModelRawAnly[vbo.id];

        if ( r != null )
        {
            r.Destory();

            mObjModelRawAnly.Remove(vbo.id);
        }
    }
    #endregion

    //#region ObjModelRaw
    //public void Update(ObjModelRaw omr)
    //{
    //    switch (omr.state)
    //    {
    //        case MessageState.Null:
    //            break;
    //        case MessageState.Create:
    //            Create(omr);
    //            break;
    //        case MessageState.Update:
    //            Modify(omr);
    //            break;
    //        case MessageState.Destory:
    //            Destory(omr);
    //            break;
    //    }
    //}

    ///// <summary>
    ///// 创建对象
    ///// </summary>
    ///// <param name="omr"></param>
    //void Create(ObjModelRaw omr)
    //{
    //    float start = Time.realtimeSinceStartup;

    //    ObjModelRawAnly o = new ObjModelRawAnly(omr);
    //    o.SetGeometryData(omr.content);

    //    ObjModelRawRender r = new ObjModelRawRender();
    //    r.BuildGameObject(ref o);
    //    r.BuildMesh(ref o);
    //    Smooth smooth = new Smooth(o.buffer);
    //    o.buffer = smooth.Exe_GeometryBuffer();
    //    r.Deformation(ref o);

    //    mObjModelRawAnly.Add(omr.id, r);

    //    Debuger.Log("Create " + (Time.realtimeSinceStartup - start) / 1000.0f / 1000.0f + " s");
    //}

    //void Modify(ObjModelRaw omr)
    //{
    //    float start = Time.realtimeSinceStartup;

    //    ObjModelRawAnly o = new ObjModelRawAnly(omr);
    //    o.SetGeometryData(omr.content);


    //    ObjModelRawRender r = (ObjModelRawRender)mObjModelRawAnly[omr.id];

    //    r.BuildMesh(ref o);
    //    Smooth smooth = new Smooth(o.buffer);
    //    o.buffer = smooth.Exe_GeometryBuffer();
    //    r.Deformation(ref o);

    //    Debuger.Log("Modify " + (Time.realtimeSinceStartup - start) / 1000.0f / 1000.0f + " s");

    //}

    //void Destory(ObjModelRaw omr)
    //{

    //}
    //#endregion
}
