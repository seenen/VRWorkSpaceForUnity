using UnityEngine;
using System.Collections;
using LibraryGeometryFormat;

public class ObjFileSample : MonoBehaviour {

	// Use this for initialization
	void Start () {
        string path = "G:/GitHub/VR/Tools/stl2obj/Resources/DataFileObj/1.obj";

        ObjFile of = ObjFile.Load(path);

        GameObject.Find("_Message_").SendMessage("RenderObj", of.Models[0]);

        Debug.Log("本地 顶点：" + modelMesh.vertexCount);
        Debug.Log("本地 三角形：" + modelMesh.triangles.Length);
        Debug.Log("本地 法线：" + modelMesh.normals.Length);

    }

    // Update is called once per frame
    void Update () {
	
	}

    public Mesh modelMesh;


}
