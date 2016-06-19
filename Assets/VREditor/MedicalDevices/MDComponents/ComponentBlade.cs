using UnityEngine;
using System.Collections;
using LibVRGeometry;

public class ComponentBlade : MonoBehaviour
{
    /// <summary>
    /// 脚本绑定的组件
    /// </summary>
    public HDComponentMessage cm;

    /// <summary>
    /// 隶属设备
    /// </summary>
    public MedicalDevices md;

    public ComponentProerty cp;

    public Material mMaterial;

    public Renderer mRenderer;

    public int order;

    void Start()
    {
        mRenderer = gameObject.GetComponent<Renderer>();

        mMaterial = mRenderer.material;
    }

    public void Selected( bool flag )
    {
        Debuger.Log("ComponentBlade.Selected " + flag);

        if (flag)
            mRenderer.material = MaterialManager.GetBeginMat();
        else
            mRenderer.material = mMaterial;
    }
}
