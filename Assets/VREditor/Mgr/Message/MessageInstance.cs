using LibVRGeometry;
using LibVRGeometry.Message;
using System;
using U3DSceneEditor;
using UnityEngine.SceneManagement;

public class MessageInstance : IMessage
{
    ObjModelRawMgr mObjModelRawMgr;

    public MessageInstance()
    {
        mObjModelRawMgr = new ObjModelRawMgr();
    }

    /// <summary>
    /// 编辑器消息
    /// </summary>
    /// <param name="em"></param>
    public void OnEditorMessage(EditorMessage em)
    {
        Debuger.Log("MessageInstance.EditorMessage" + em.name);
    }

    public void OnMDScissors(HDScissorsMessage o)
    {
        Debuger.Log("MessageInstance.OnMDScissors" + MessageDecoder.EncodeMessage(o));

        VBOBufferSingleMgr.Instance.Update(o);
    }

    public void OnMDTitaniumClamp(HDTitaniumClampMessage o)
    {
        Debuger.Log("MessageInstance.OnMDTitaniumClamp" + MessageDecoder.EncodeMessage(o));

        VBOBufferSingleMgr.Instance.Update(o);
    }

    public void OnMDRobotArm(HDRobotArmMessage o)
    {
        Debuger.Log("MessageInstance.OnMDRobotArm" + MessageDecoder.EncodeMessage(o));

        VBOBufferSingleMgr.Instance.Update(o);
    }

    public void OnSceneMessage(SceneMessage o)
    {
        Debuger.Log("MessageInstance.OnSceneMessage" + o.scene_name);

        //SceneManager.LoadScene("GallBladder_Cutting", LoadSceneMode.Additive);

        //MedicalDevicesMgr.instance.SetLeft(GameObject.Find("MDTitaniumClamp"));

        //MedicalDevicesMgr.instance.SetRight(GameObject.Find("MDScissors"));
    }

    /// <summary>
    /// 2组以上对象数据格式
    /// </summary>
    /// <param name="buffer"></param>
    public void OnVBOBuffer(VBOBuffer buffer)
    {
        //Debuger.Log("MessageInstance.OnVBOBuffer" + buffer.id);

        mObjModelRawMgr.Update(buffer);
    }

    /// <summary>
    /// 单对象数据格式
    /// </summary>
    /// <param name="buffer"></param>
    public void OnVBOBufferSingle(VBOBufferSingle buffer)
    {
        Debuger.Log("MessageInstance.OnVBOBufferSingle" + buffer.id);

        switch(buffer.vboType)
        {
            case VBOType.Null:
                break;
            case VBOType.DOT_OBJ:
                VBOBufferSingleMgr.Instance.Update(buffer);
                break;
            case VBOType.U3D_MESH:
                break;
        }
    }

    public void OnEnvCfg(EnvCfg o)
    {
        Debuger.Log("MessageInstance.OnEnvCfg " + o.Log);

        Debuger.EnableLog = bool.Parse(o.Log);
    }

    public void OnMD2HO(IM_MD2HO o)
    {
        throw new NotImplementedException();
    }

    public void OnAddMessage(HDScissorsMessage sm, HDTitaniumClampMessage tc)
    {
        throw new NotImplementedException();
    }
}
