using LibVRGeometry;
using System;
using System.Threading;
using U3DSceneEditor;
using UnityEngine;

public class DataServer : MonoBehaviour
{
    VBOBufferSingle vbo = null;
    void Start()
    {

        //初始化计算器
        Server.StartServer("C:/DataServer/qingxing.dat");


        vbo = new VBOBufferSingle();
        vbo.id = 0;

        CreateModel();
    }

    public void Update()
    {
        UpdateModel();

        index++;

        if (index == MAX_COUNT)
            index = 0;
    }

    public void Destory()
    {
        Server.EndServer();
    }

    void CreateModel()
    {
        float start = System.DateTime.Now.Millisecond;

        Server.Pull(ref vbo);
        vbo.id = 0;
        vbo.name = "default";
        vbo.materialName = "default";
        vbo.state = VBOState.Create;
        vbo.vboType = VBOType.DOT_OBJ;

        VBOBufferSingleMgr.Instance.Update(vbo);

        //unity3dControl2.SendMessage<VBOBufferSingle>(vbo);

        VBOBufferSingleFile.Output(vbo, "G:/GitHub/VRWorkSpaceForUnity/Assets/DataServerFiles/DataServer_vboBufferSingle.obj");
        start = (System.DateTime.Now.Millisecond - start);

        Console.WriteLine("SendMessage<VBOBufferSingle>." + start / 1000.0f / 1000.0f);
    }

    void UpdateModel()
    {
        Server.Pull(ref vbo);
        vbo.id = 0;
        vbo.state = VBOState.Update;
        vbo.vboType = VBOType.DOT_OBJ;
        vbo.name = "default";
        vbo.materialName = "default";

        Console.WriteLine("DataServer.Beta is running in its own thread." + vbo.id);

        VBOBufferSingleMgr.Instance.Update(vbo);

    }

    int MAX_COUNT = 100;
    int index = 0;

    public void Beta()
    {

        Thread.Sleep(10);

        while (true)
        {
            UpdateModel();

            Thread.Sleep(10);

            index++;

            if (index == MAX_COUNT) index = 0;

            float start = System.DateTime.Now.Millisecond;

            //unity3dControl2.SendMessage<VBOBufferSingle>(vbo);

            start = (System.DateTime.Now.Millisecond - start);

            Console.WriteLine("SendMessage<ObjModelRaw>." + start / 1000.0f / 1000.0f);

            //VBOBufferSingleFile.Output(vbo, "G:/GitHub/VRWorkSpaceForUnity/Assets/DataServerFiles/DataServer_vboBufferSingle_" + index.ToString() + ".obj");

        }
    }
}
