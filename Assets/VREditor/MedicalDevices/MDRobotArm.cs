using UnityEngine;
using LibVRGeometry;
using LibVRGeometry.VRWorld;

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

    public GameObject mShadow;

    void Start()
    {
#if UNITY_EDITOR
        mHDRobotArmMessage = new HDRobotArmMessage();
        mHDRobotArmMessage.mFaceAngle = 30;
        mHDRobotArmMessage.mElbowAngle = 30;
        mHDRobotArmMessage.mShoulderHeight = 50f;
        mHDRobotArmMessage.mUpperarmLen = 50f;
        mHDRobotArmMessage.mForearmLen = 80f;
        mHDRobotArmMessage.mOriginPos = new _Vector3(0, 0, 0);
        mHDRobotArmMessage.length = 220;

        RobotArm mRRobotArm = new RobotArm(mHDRobotArmMessage);
        mRRobotArm.Fresh(mHDRobotArmMessage);

        mRRobotArm.UpdateTool(mHDRobotArmMessage.length, 150);

        UpdateData(mHDRobotArmMessage);
#endif

    }

    public void UpdateData(HDRobotArmMessage arm)
    {
        if (arm == null)
            return;

        mHDRobotArmMessage = arm;

        shoulder.transform.position = new Vector3(0, arm.mShoulderHeight, 0);

        elbow.transform.position = new Vector3( -arm.mToolElbow.Z,
                                                arm.mToolElbow.Y,
                                                arm.mToolElbow.X);

        hand.transform.position = new Vector3(  -arm.mToolKey.Z,
                                                arm.mToolKey.Y,
                                                arm.mToolKey.X);

        head.transform.position = new Vector3(  arm.mToolHead.Z,
                                                arm.mToolHead.Y,
                                                arm.mToolHead.X);

        mBindDir = (-hand.transform.position + head.transform.position).normalized;

    }

    void Update()
    {
        mLineRenderer.SetPosition(0, origion.transform.position);
        mLineRenderer.SetPosition(1, shoulder.transform.position);
        mLineRenderer.SetPosition(2, elbow.transform.position);
        mLineRenderer.SetPosition(3, hand.transform.position);
        //mLineRenderer.SetPosition(4, head.transform.position);

        UpdateBind();

        UpdateShadow();
    }

    public void UpdateBind()
    {
        if (mBind == null)
            return;

        mBind.transform.position = head.transform.position;

        mBindTarget.transform.position = mBind.transform.position + mBindDir * 0.5f;

        mBind.transform.LookAt(mBindTarget.transform.position);
    }

    RaycastHit hitinfo = new RaycastHit();

    public void UpdateShadow()
    {
        if (mShadow == null)
            return;

        if (Physics.Raycast(head.transform.position, mBindDir, out hitinfo, 10000, 1 >> 10))
        {
            mShadow.transform.position = hitinfo.point;
        }
        else
        {
            mShadow.transform.position = Vector3.one * 1000;
        }
    }
}
