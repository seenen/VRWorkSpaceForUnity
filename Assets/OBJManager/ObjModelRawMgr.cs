using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using LibraryGeometryFormat;
using VRClient;

public class ObjModelRawMgr
{
    public Dictionary<int, ObjModelRawRender> mObjModelRawAnly = new Dictionary<int, ObjModelRawRender>();

    #region VBOBuffer
    public void Update(VBOBuffer omr)
    {
        switch (omr.state)
        {
            case ObjModelRawState.Null:
                break;
            case ObjModelRawState.Create:
                CreateVBOBuffer(omr);
                break;
            case ObjModelRawState.Update:
                ModifyVBOBuffer(omr);
                break;
            case ObjModelRawState.Destory:
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

        ObjModelRawAnly o = new ObjModelRawAnly(omr);

        ObjModelRawRender r = new ObjModelRawRender();
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

        ObjModelRawAnly o = new ObjModelRawAnly(omr);


        ObjModelRawRender r = (ObjModelRawRender)mObjModelRawAnly[omr.id];

        r.BuildMesh(ref o);
        Smooth smooth = new Smooth(o.buffer);
        o.buffer = smooth.Exe_GeometryBuffer();
        r.Deformation(ref o);

        Debuger.Log("Modify " + (Time.realtimeSinceStartup - start) / 1000.0f / 1000.0f + " s");
    }
    #endregion

    #region ObjModelRaw
    public void Update(ObjModelRaw omr)
    {
        switch (omr.state)
        {
            case ObjModelRawState.Null:
                break;
            case ObjModelRawState.Create:
                Create(omr);
                break;
            case ObjModelRawState.Update:
                Modify(omr);
                break;
            case ObjModelRawState.Destory:
                Destory(omr);
                break;
        }
    }

    /// <summary>
    /// 创建对象
    /// </summary>
    /// <param name="omr"></param>
    void Create(ObjModelRaw omr)
    {
        float start = Time.realtimeSinceStartup;

        ObjModelRawAnly o = new ObjModelRawAnly(omr);
        o.SetGeometryData(omr.content);

        ObjModelRawRender r = new ObjModelRawRender();
        r.BuildGameObject(ref o);
        r.BuildMesh(ref o);
        Smooth smooth = new Smooth(o.buffer);
        o.buffer = smooth.Exe_GeometryBuffer();
        r.Deformation(ref o);

        mObjModelRawAnly.Add(omr.id, r);

        Debuger.Log("Create " + (Time.realtimeSinceStartup - start) / 1000.0f / 1000.0f + " s");
    }

    void Modify(ObjModelRaw omr)
    {
        float start = Time.realtimeSinceStartup;

        ObjModelRawAnly o = new ObjModelRawAnly(omr);
        o.SetGeometryData(omr.content);


        ObjModelRawRender r = (ObjModelRawRender)mObjModelRawAnly[omr.id];

        r.BuildMesh(ref o);
        Smooth smooth = new Smooth(o.buffer);
        o.buffer = smooth.Exe_GeometryBuffer();
        r.Deformation(ref o);

        Debuger.Log("Modify " + (Time.realtimeSinceStartup - start) / 1000.0f / 1000.0f + " s");

    }

    void Destory(ObjModelRaw omr)
    {

    }
    #endregion
}
