using UnityEngine;
using LibVRGeometry;

public class MDRobotArm : MedicalDevices
{
    public GameObject origion;

    public GameObject shoulder;

    public GameObject elbow;

    public GameObject hand;

    public GameObject head;

    public HDRobotArmMessage mHDRobotArmMessage;

    public LineRenderer mLineRenderer;

    public GameObject mBind;

    public Vector3 mBindDir;

    public GameObject mBindTarget;

    public void UpdateData(HDRobotArmMessage arm)
    {
        mHDRobotArmMessage = arm;

        shoulder.transform.position = new Vector3(0, arm.mShoulderHeight / 100f, 0);

        elbow.transform.position = new Vector3( -arm.mToolElbow.Z / 100f,
                                                arm.mToolElbow.Y / 100f,
                                                arm.mToolElbow.X / 100f);

        hand.transform.position = new Vector3(  -arm.mToolKey.Z / 100f,
                                                arm.mToolKey.Y / 100f,
                                                arm.mToolKey.X / 100f);

        head.transform.position = new Vector3(  arm.mToolHead.Z / 100f,
                                                arm.mToolHead.Y / 100f,
                                                arm.mToolHead.X / 100f);

        mBindDir = (-hand.transform.position + head.transform.position).normalized;

    }

    void Update()
    {
        mLineRenderer.SetPosition(0, origion.transform.position);
        mLineRenderer.SetPosition(1, shoulder.transform.position);
        mLineRenderer.SetPosition(2, elbow.transform.position);
        mLineRenderer.SetPosition(3, hand.transform.position);
        mLineRenderer.SetPosition(4, head.transform.position);

        UpdateBind();
    }

    public void UpdateBind()
    {
        if (mBind == null)
            return;

        mBind.transform.position = head.transform.position;

        mBindTarget.transform.position = mBind.transform.position + mBindDir * 0.5f;

        mBind.transform.LookAt(mBindTarget.transform.position);
    }
}
