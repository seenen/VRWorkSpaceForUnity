using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MedicalDevicesMgr : MonoBehaviour
{
    static public MedicalDevicesMgr instance;

    void Awake()
    {
        instance = this;

#if UNITY_EDITOR
        SceneManager.LoadScene("GallBladder_Cutting", LoadSceneMode.Additive);
#endif
    }

    void Start()
    {
        instance = this;
    }

    public MedicalDevices Left;
    public MedicalDevices Right;

    public FingerControl mFingerControl;

    public void SetLeft(GameObject obj)
    {
        Left = obj.GetComponent<MedicalDevices>();
        mFingerControl.handleKeyBoard = Left;

    }

    public void SetRight(GameObject obj)
    {
        Right = obj.GetComponent<MedicalDevices>();
    }
}
