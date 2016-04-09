using UnityEngine;
using System.Collections;
using LibVRGeometry;

public class ObjFileSample : MonoBehaviour {

	// Use this for initialization
	void Start () {
        string path = "G:/GitHub/VR/Tools/stl2obj/Resources/DataFileObj/1.obj";

        ObjFile of = ObjFile.Load(path);

        GameObject.Find("_Message_").SendMessage("RenderObj", of.Models[0]);

        Debuger.Log("本地 顶点：" + modelMesh.vertexCount);
        Debuger.Log("本地 三角形：" + modelMesh.triangles.Length);
        Debuger.Log("本地 法线：" + modelMesh.normals.Length);

    }

    // Update is called once per frame
    void Update () {
	
	}

    public Mesh modelMesh;


}
