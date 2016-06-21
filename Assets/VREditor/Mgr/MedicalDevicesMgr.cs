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
}
