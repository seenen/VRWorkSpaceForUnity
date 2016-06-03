using UnityEngine;
using LibVRGeometry;

public class MDRobotArm : MedicalDevices
{
    public GameObject origion;

    public GameObject shoulder;

    public GameObject elbow;

    public GameObject hand;

    public void UpdateData(HDRobotArmMessage arm)
    {
        transform.position = new Vector3(arm.mOriginPos.X, arm.mOriginPos.Y, arm.mOriginPos.Z);

        shoulder.transform.localPosition = new Vector3(0, arm.mShoulderHeight, 0);
    }
}
