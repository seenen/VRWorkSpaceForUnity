using UnityEngine;
using System.Collections;
using LibVRGeometry;

public class ComponentBlade : MonoBehaviour
{
    /// <summary>
    /// 隶属设备
    /// </summary>
    public MedicalDevices md;

    public Material mMaterial;

    public Renderer mRenderer;

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
