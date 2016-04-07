using UnityEngine;
using System.Collections;
using VRClient;
using LibraryGeometryFormat;
using System.Collections.Generic;
using System.IO;
using System;

public class _Message_Json_ : MonoBehaviour
{
    void Start()
    {
#if UNITY_EDITOR

        StartCoroutine(Load());
#endif

    }

    public IEnumerator Load()
    {
        {
            string path = "file:///G:/GitHub/VR/Tools/stl2obj/Resources/DataFileObj/1.obj";

            WWW loader = new WWW(path);

            yield return loader;

            ObjModelRaw omr = new ObjModelRaw();
            omr.id = 0;
            omr.content = loader.text;
            omr.state = ObjModelRawState.Create;

            string output = EditorMessageDecoder.EncodeMessageByProtobuf<ObjModelRaw>(omr);

            ObjModelRaw obj = EditorMessageDecoder.DecodeMessageByProtobuf<ObjModelRaw>(output);

            RenderObjRaw((ObjModelRaw)obj);

            loader.Dispose();
            loader = null;

        }

        yield return 1000;

        {
            string path = "file:///G:/GitHub/VR/Tools/stl2obj/Resources/DataFileObj/5.obj";

            WWW loader = new WWW(path);

            yield return loader;

            ObjModelRaw omr = new ObjModelRaw();
            omr.id = 0;
            omr.content = loader.text;
            omr.state = ObjModelRawState.Update;

            string output = EditorMessageDecoder.EncodeMessageByProtobuf<ObjModelRaw>(omr);

            ObjModelRaw obj = EditorMessageDecoder.DecodeMessageByProtobuf<ObjModelRaw>(output);

            RenderObjRaw((ObjModelRaw)obj);

            loader.Dispose();
            loader = null;

        }
    }

    ObjModelRawMgr mObjModelRawMgr = new ObjModelRawMgr();

    string buff = string.Empty;

    int bufflen = 0;

    void _Message_Json_Recv(string content)
    {
        Debug.Log("_Message_Json_Recv " + content.Length);

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

                Debug.Log("[bufflen:] " + bufflen);

                //object obj = EditorMessageDecoder.DecodeMessage(buff);
                //ObjModelRaw obj = EditorMessageDecoder.DecodeMessage<ObjModelRaw>(buff);
                ObjModelRaw obj = EditorMessageDecoder.DecodeMessageByProtobuf<ObjModelRaw>(buff);

                RenderObjRaw(obj);
                //Debug.Log(" [obj:] " + obj);

                //if (obj is ObjModel) RenderObj((ObjModel)obj);

                //if (obj is ObjModelRaw) RenderObjRaw((ObjModelRaw)obj);

                buff = string.Empty;

                Debug.Log(" Done ");
            }
        }
        catch (Exception e)
        {
            Debug.LogWarning(e);
        }

    }

    void RenderObjRaw(ObjModelRaw om)
    {
        //Debug.Log("RenderObjRaw" + om.state);

        mObjModelRawMgr.Update(om);
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
        Debug.Log("顶点：" + newVerts.Length);

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

        Debug.Log("三角形：" + om.faceList.Count);

        List<int> triangles = new List<int>();
        for (int i = 0; i < om.faceList.Count; ++i)
        {
            triangles.Add(om.faceList[i].x);
            triangles.Add(om.faceList[i].y);
            triangles.Add(om.faceList[i].z);
        }
        mesh.triangles = triangles.ToArray();

        Debug.Log("法向量：" + om.normalList.Count);
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
