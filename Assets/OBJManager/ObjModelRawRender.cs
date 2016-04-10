using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjModelRawRender
{
    //  
    public GameObject gameObject = null;
    public MeshFilter mMeshFilter = null;
    public MeshRenderer mMeshRenderer = null;


    public Dictionary<string, Material> materials = new Dictionary<string, Material>();
    public GameObject[] ms = null;

    //  材质配置
    public Material mMaterial = null;

    public ObjModelRawRender()
    {
        gameObject = new GameObject();
    }

    public void Destory()
    {
        if (gameObject != null)
        {
            GameObject.Destroy(gameObject);
            mMeshFilter = null;
            mMeshRenderer = null;
            gameObject = null;
        }
    }

    public void BuildGameObject(ref ObjModelRawAnly omr)
    {
        if (omr.hasMaterials)
        {
            foreach (ObjModelRawAnly.MaterialData md in omr.materialData)
            {
                materials.Add(md.name, omr.GetMaterial(md));
            }
        }
        else
        {
            materials.Add("default", new Material(Shader.Find("VertexLit")));
        }

        ms = new GameObject[omr.buffer.numObjects];

        if (omr.buffer.numObjects == 1)
        {
            mMeshFilter = (MeshFilter)gameObject.AddComponent(typeof(MeshFilter));
            mMeshRenderer = (MeshRenderer)gameObject.AddComponent(typeof(MeshRenderer));
            ms[0] = gameObject;
        }
        else if (omr.buffer.numObjects > 1)
        {
            for (int i = 0; i < omr.buffer.numObjects; i++)
            {
                GameObject go = new GameObject();
                go.transform.parent = gameObject.transform;
                go.AddComponent(typeof(MeshFilter));
                go.AddComponent(typeof(MeshRenderer));
                ms[i] = go;
            }
        }

        mMeshRenderer.material = MaterialManager.GetBeginMat();

    }

    ////public void BuildMesh(ref ObjModelRawAnly omr)
    ////{

    ////    omr.buffer.PopulateMeshes(ms, materials);

    ////}

    public void BuildMesh(ref ObjModelRawAnly omr)
    {
        omr.buffer.PopulateMeshes();
    }

    /// <summary>
    /// 变形
    /// </summary>
    /// <param name="omr"></param>
    public void Deformation(ref ObjModelRawAnly omr)
    {
        mMeshFilter.mesh.vertices = omr.buffer.vertices.ToArray();
        mMeshFilter.mesh.triangles = omr.buffer.triangles;
        //mMeshFilter.mesh.uv = bufferCache.uvs.ToArray();
        //mMeshFilter.mesh.normals = bufferCache.normals.ToArray();
        mMeshFilter.mesh.RecalculateNormals();
        mMeshFilter.mesh.RecalculateBounds();
    }
}
