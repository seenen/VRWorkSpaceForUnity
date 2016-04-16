using System.Collections.Generic;
using UnityEngine;

public class MeshWireShowMono : MonoBehaviour
{
    Vector3 P0;
    Vector3 P1;
    Vector3 P2;

    List<Vector3> wires = new List<Vector3>();
    Color lineColor = Color.blue;

    static Material lineMaterial;

    public GameObject myMesh;

    // Use this for initialization
    void Awake()
    {
        //myMesh = gameObject;
    }

    void Start()
    {
        CreateLineMaterial();

        UpdateMesh(myMesh);
    }

    public void UpdateMesh(GameObject myMesh)
    {

        if (myMesh == null)
            return;

        MeshFilter filter = myMesh.GetComponent<MeshFilter>();
        Mesh mesh = filter.mesh;
        Vector3[] vertices = mesh.vertices;
        int[] triangles = mesh.triangles;

        for (int k = 0; k < triangles.Length / 3; k++)
        {
            wires.Add(vertices[triangles[k * 3]]);
            wires.Add(vertices[triangles[k * 3 + 1]]);
            wires.Add(vertices[triangles[k * 3 + 2]]);
        }
        wires.Add(vertices[triangles[triangles.Length - 2]]);
        wires.Add(vertices[triangles[triangles.Length - 1]]);

        Debuger.Log(wires.Count);

    }

    void OnPostRender()
    {
        if (myMesh == null)
            return;


        lineMaterial.SetPass(0);

        GL.Begin(GL.LINES);
        GL.Color(lineColor);
        for (int i = 0; i < wires.Count / 3; i++)
        {
            P0 = myMesh.transform.TransformPoint(wires[i * 3]);
            P1 = myMesh.transform.TransformPoint(wires[i * 3 + 1]);
            P2 = myMesh.transform.TransformPoint(wires[i * 3 + 2]);

            GL.Vertex3(P0.x, P0.y, P0.z);
            GL.Vertex3(P1.x, P1.y, P1.z);
            GL.Vertex3(P2.x, P2.y, P2.z);
            GL.Vertex3(P0.x, P0.y, P0.z);
        }

        GL.End();
    }

    void CreateLineMaterial()
    {
        if (!lineMaterial)
        {
            lineMaterial = new Material("Shader \"Lines/Colored Blended\" {" +
                "SubShader { Pass { " +
                "    Blend SrcAlpha OneMinusSrcAlpha " +
                "    ZWrite Off Cull Front Fog { Mode Off } " +
                "} } }");
            lineMaterial.hideFlags = HideFlags.HideAndDontSave;
            lineMaterial.shader.hideFlags = HideFlags.HideAndDontSave;
        }

    }
}
