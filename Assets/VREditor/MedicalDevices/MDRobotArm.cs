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

    public void UpdateData(HDRobotArmMessage arm)
    {
        mHDRobotArmMessage = arm;

        shoulder.transform.localPosition = new Vector3(0, arm.mShoulderHeight / 100f, 0);

        hand.transform.localPosition = new Vector3( arm.mToolKey.Z / 100f,
                                                    arm.mToolKey.Y / 100f,
                                                    arm.mToolKey.X / 100f);

        head.transform.localPosition = new Vector3( arm.mToolHead.Z / 100f,
                                                    arm.mToolHead.Y / 100f,
                                                    arm.mToolHead.X / 100f);
    }

}
