using LibVRGeometry;
using LibVRGeometry.Message;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _Message_Json_ : MonoBehaviour
{
    MessageInstance mMessageInstance = new MessageInstance();

    void Start()
    {
        Debuger.EnableLog = true;

#if UNITY_EDITOR

        //StartCoroutine(Load());
        //StartCoroutine(LoadVBO());
        StartCoroutine(LoadVBOBufferSingle());
#endif

    }
    public IEnumerator LoadVBOBufferSingle()
    {
        {
            string path = "file:///G:/GitHub/VR/Tools/stl2obj/Resources/DataFileObj/1.obj";

            WWW loader = new WWW(path);

            yield return loader;

            ///拿到一个GB对象
            ObjModelRaw omr = new ObjModelRaw();
            omr.id = 0;
            omr.content = loader.text;
            omr.state = MessageState.Create;

            ObjModelRawAnly o = new ObjModelRawAnly(omr);
            o.SetGeometryData(omr.content);
            o.buffer.PopulateMeshes();

            //  构造一个VBO对象

            VBOBufferSingle vbo = new VBOBufferSingle();
            foreach(Vector3 v3 in o.buffer.vertices)
                vbo.vertices.Add(new _Vector3(v3.x, v3.y, v3.z));
            foreach(Vector2 v2 in o.buffer.uvs)
                vbo.uvs.Add(new _Vector2(v2.x, v2.y));
            foreach(Vector3 v3 in o.buffer.normals)
                vbo.normals.Add(new _Vector3(v3.x, v3.y, v3.z));
            vbo.state = MessageState.Create;

            vbo.name = o.buffer.objects[0].name;
            vbo.faces = o.buffer.objects[0].allFaces;
            vbo.materialName = "default";

            //  序列化
            string output = MessageDecoder.EncodeMessageByProtobuf<VBOBufferSingle>(vbo);
            RenderVBOBufferSingle(vbo);

            loader.Dispose();
            loader = null;

        }
    }

    //public IEnumerator LoadVBO()
    //{
    //    {
    //        string path = "file:///G:/GitHub/VR/Tools/stl2obj/Resources/DataFileObj/1.obj";

    //        WWW loader = new WWW(path);

    //        yield return loader;

    //        ///拿到一个GB对象
    //        ObjModelRaw omr = new ObjModelRaw();
    //        omr.id = 0;
    //        omr.content = loader.text;
    //        omr.state = MessageState.Create;

    //        ObjModelRawAnly o = new ObjModelRawAnly(omr);
    //        o.SetGeometryData(omr.content);
    //        o.buffer.PopulateMeshes();

    //        //  构造一个VBO对象

    //        VBOBuffer vbo = new VBOBuffer();
    //        vbo.objects.AddRange(o.buffer.objects);
    //        foreach(Vector3 v3 in o.buffer.vertices)
    //            vbo.vertices.Add(new _Vector3(v3.x, v3.y, v3.z));
    //        foreach(Vector2 v2 in o.buffer.uvs)
    //            vbo.uvs.Add(new _Vector2(v2.x, v2.y));
    //        foreach(Vector3 v3 in o.buffer.normals)
    //            vbo.normals.Add(new _Vector3(v3.x, v3.y, v3.z));
    //        foreach(int i in o.buffer.triangles)
    //            vbo.triangles.Add(i);
    //        vbo.state = MessageState.Create;

    //        //  序列化
    //        string output = MessageDecoder.EncodeMessageByProtobuf<VBOBuffer>(vbo);
    //        RenderVBOBuffer(vbo);

    //        loader.Dispose();
    //        loader = null;

    //    }
    //}

    //public IEnumerator Load()
    //{
    //    {
    //        string path = "file:///G:/GitHub/VR/Tools/stl2obj/Resources/DataFileObj/1.obj";

    //        WWW loader = new WWW(path);

    //        yield return loader;

    //        ObjModelRaw omr = new ObjModelRaw();
    //        omr.id = 0;
    //        omr.content = loader.text;
    //        omr.state = MessageState.Create;

    //        string output = MessageDecoder.EncodeMessageByProtobuf<ObjModelRaw>(omr);

    //        ObjModelRaw obj = MessageDecoder.DecodeMessageByProtobuf<ObjModelRaw>(output);

    //        RenderObjRaw((ObjModelRaw)obj);

    //        loader.Dispose();
    //        loader = null;

    //    }

    //    yield return 1000;

    //    {
    //        string path = "file:///G:/GitHub/VR/Tools/stl2obj/Resources/DataFileObj/5.obj";

    //        WWW loader = new WWW(path);

    //        yield return loader;

    //        ObjModelRaw omr = new ObjModelRaw();
    //        omr.id = 0;
    //        omr.content = loader.text;
    //        omr.state = MessageState.Update;

    //        string output = MessageDecoder.EncodeMessageByProtobuf<ObjModelRaw>(omr);

    //        ObjModelRaw obj = MessageDecoder.DecodeMessageByProtobuf<ObjModelRaw>(output);

    //        RenderObjRaw((ObjModelRaw)obj);

    //        loader.Dispose();
    //        loader = null;

    //    }
    //}

    string buff = string.Empty;

    int bufflen = 0;

    int start = 0;

    void _Message_Json_Recv(string content)
    {
        //Debuger.Log("_Message_Json_Recv " + content.Length);

        try
        {
            if (content.Length == 9999)
            {
                buff += content;

                bufflen += 9999;
            }
            else
            {
                buff += content;

                bufflen += content.Length;

                //float start = Time.realtimeSinceStartup;

                //Debuger.Log("[bufflen:] " + bufflen);

                //object obj = EditorMessageDecoder.DecodeMessage(buff);
                //ObjModelRaw obj = EditorMessageDecoder.DecodeMessage<ObjModelRaw>(buff);
                //ObjModelRaw obj = EditorMessageDecoder.DecodeMessageByProtobuf<ObjModelRaw>(buff);
                //VBOBuffer obj = MessageDecoder.DecodeMessageByProtobuf<VBOBuffer>(buff);
                MessageDecoder.DecodeMessageWithHeader(buff, mMessageInstance);

                //Debuger.Log("DecodeMessageByProtobuf " + (System.DateTime.Now.Millisecond - obj.t) / 1000.0f / 1000.0f + " s");
                Debuger.Log("DecodeMessageByProtobuf " + (Time.realtimeSinceStartup - start) / 1000.0f / 1000.0f + " s");

                ////RenderObjRaw(obj);
                //RenderVBOBuffer(obj);
                //Debuger.Log(" [obj:] " + obj);

                //if (obj is ObjModel) RenderObj((ObjModel)obj);

                //if (obj is ObjModelRaw) RenderObjRaw((ObjModelRaw)obj);

                buff = string.Empty;

                start = System.DateTime.Now.Millisecond;

                Debuger.Log(" Done ");
            }
        }
        catch (Exception e)
        {
            Debuger.LogWarning(e);
        }

    }

    //ObjModelRawMgr mObjModelRawMgr = new ObjModelRawMgr();

    //void RenderObjRaw(ObjModelRaw om)
    //{
    //    //Debuger.Log("RenderObjRaw" + om.state);

    //    mObjModelRawMgr.Update(om);
    //}

    VBOBufferSingleMgr mVBOBufferSingleMgr = new VBOBufferSingleMgr();

    //void RenderVBOBuffer(VBOBuffer vbo)
    //{
    //    Debuger.Log("RenderVBOBuffer" + vbo.objects.Count);

    //    mObjModelRawMgr.Update(vbo);
    //}

    void RenderVBOBufferSingle(VBOBufferSingle vbo)
    {
        Debuger.Log("RenderVBOBufferSingle" + vbo.id);

        mVBOBufferSingleMgr.Update(vbo);
    }

    void RenderObj(ObjModel om)
    {
        ///
        _Vector3[] newVerts = new _Vector3[om.faceList.Count];
        _Vector2[] newUVs = new _Vector2[om.faceList.Count];
        _Vector3[] newNormals = new _Vector3[om.faceList.Count];
        int INDEX = 0;
        /* The following foreach loops through the facedata and assigns the appropriate vertex, uv, or normal
         * for the appropriate Unity mesh array.
         */
        foreach (Tuple v in om.faceList)
        {
            newVerts[INDEX] = om.positionList[(int)v.x - 1];
            //if (v.y >= 1)
            //    newUVs[INDEX] = om.uvList[(int)v.y - 1];

            if (v.z >= 1)
                newNormals[INDEX] = om.normalList[(int)v.z - 1];

            INDEX++;
        }
        ///

        GameObject go = new GameObject();

        MeshFilter mf = (MeshFilter)go.AddComponent(typeof(MeshFilter));
        MeshRenderer mr = (MeshRenderer)go.AddComponent(typeof(MeshRenderer));

        //  顶点
        Debuger.Log("顶点：" + newVerts.Length);

        Mesh mesh = new Mesh();
        mesh.vertices = new Vector3[newVerts.Length];
        for (int i = 0; i < newVerts.Length; ++i)
        {
            _Vector3 _v = newVerts[i];

            Vector3 v3;
            v3.x = _v.X;
            v3.y = _v.Y;
            v3.z = _v.Z;

            mesh.vertices[i] = v3;
        }

        Debuger.Log("三角形：" + om.faceList.Count);

        List<int> triangles = new List<int>();
        for (int i = 0; i < om.faceList.Count; ++i)
        {
            triangles.Add(om.faceList[i].x);
            triangles.Add(om.faceList[i].y);
            triangles.Add(om.faceList[i].z);
        }
        mesh.triangles = triangles.ToArray();

        Debuger.Log("法向量：" + om.normalList.Count);
        mesh.normals = new Vector3[om.normalList.Count];
        for (int i = 0; i < om.normalList.Count; ++i)
        {
            _Vector3 _v = om.normalList[i];

            Vector3 v3;
            v3.x = _v.X;
            v3.y = _v.Y;
            v3.z = _v.Z;

            mesh.normals[i] = v3;
        }

        mr.material = new Material(Shader.Find("Diffuse"));
        mf.mesh = mesh;
        mf.mesh.RecalculateNormals();
        mf.mesh.RecalculateBounds();
        mf.mesh.Optimize();

    }
}
