using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using LibraryGeometryFormat;

public class ObjModelRawMgr
{
    public Dictionary<int, ObjModelRawRender> mObjModelRawAnly = new Dictionary<int, ObjModelRawRender>();

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
        ObjModelRawAnly o = new ObjModelRawAnly(omr);
        o.SetGeometryData(omr.content);

        ObjModelRawRender r = new ObjModelRawRender();
        ////r.BuildGameObject(ref o);
        ////r.BuildMesh(ref o);
        ////Smooth smooth = new Smooth(o.buffer);
        ////o.buffer = smooth.Exe_GeometryBuffer();

        r.BuildGameObject(ref o);
        r.BuildMesh(ref o);
        Smooth smooth = new Smooth(o.buffer);
        o.buffer = smooth.Exe_GeometryBuffer();
        r.Deformation(ref o);

        mObjModelRawAnly.Add(omr.id, r);
    }

    void Modify(ObjModelRaw omr)
    {
        ObjModelRawAnly o = new ObjModelRawAnly(omr);
        o.SetGeometryData(omr.content);


        ObjModelRawRender r = (ObjModelRawRender)mObjModelRawAnly[omr.id];

        r.BuildMesh(ref o);
        Smooth smooth = new Smooth(o.buffer);
        o.buffer = smooth.Exe_GeometryBuffer();
        r.Deformation(ref o);
    }

    void Destory(ObjModelRaw omr)
    {

    }
}
