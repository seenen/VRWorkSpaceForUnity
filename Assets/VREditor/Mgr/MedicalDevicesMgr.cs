using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using LibVRGeometry.Message;
using LibVRGeometry;

public class MedicalDevicesMgr : MonoBehaviour
{
    static public MedicalDevicesMgr instance;

    void Awake()
    {
        instance = this;

//#if UNITY_EDITOR
//        SceneManager.LoadScene("GallBladder_Cutting", LoadSceneMode.Additive);
//#endif
    }

    void Start()
    {
        instance = this;
    }

    #region 初始化
    public MedicalDevices Left;
    public MedicalDevices Right;

    public FingerControl mFingerControl;

    public void SetLeft(GameObject obj)
    {
        Left = obj.GetComponent<MedicalDevices>();
        mFingerControl.handleLeftKeyBoard = Left;
    }

    public void SetRight(GameObject obj)
    {
        Right = obj.GetComponent<MedicalDevices>();
        mFingerControl.handleRightKeyBoard = Right;
    }
    #endregion 初始化

    #region 信息碰交互
    /// <summary>
    /// 刀刃和器官的交互
    /// </summary>
    /// <param name="cb"></param>
    /// <param name="ho"></param>
    public void Trigger(ComponentBlade cb, HOGallBladder ho, Vector3 pos)
    {
        IM_MD2HO im = new IM_MD2HO();
        im.MD_ID = cb.md.id;
        im.HO_ID = ho.id;

        if (cb.order == 0)
        {
            im.EndPointLeft = cb.order;
            im.EndPointLeftIsCollision = true;

        }
        if (cb.order == 1)
        {
            im.EndPointRight = cb.order;
            im.EndPointRightIsCollision = true;

        }
        im.time = System.DateTime.Now;

#if !UNITY_EDITOR
        string o = MessageDecoder.EncodeMessageByProtobuf<IM_MD2HO>(im);

        CallMessage.Instance.CallMsg(o);
#endif
    }
    #endregion
}
