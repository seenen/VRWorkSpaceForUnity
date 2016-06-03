using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class MDScissors : MedicalDevices
{
    public GameObject Target;

    //  旋转轴
    public GameObject Axis_L;
    public GameObject Axis_R;

    public float openangle = 0;
    public float merge_speed = 1;

    public float OPEN_ANGLE = 0;

    public float CLOSE_ANGLE = 25;

    void Start()
    {
        MedicalDevicesMgr.instance.SetRight(gameObject);
    }

    void Update()
    {
        transform.LookAt(Target.transform.position);

#if UNITY_EDITOR
        Progress(openangle);
#endif
    }

    public void Progress(float opendegree)
    {
        openangle = Mathf.Clamp(opendegree, OPEN_ANGLE, CLOSE_ANGLE);

        Axis_L.transform.localRotation = Quaternion.Euler(0, 0, -openangle);
        Axis_R.transform.localRotation = Quaternion.Euler(0, 0, openangle);
    }

    #region keyboard
    Vector3 tmp = Vector3.zero;

    override public void Up()
    {
        if (bShift)
        {
            tmp = Target.transform.position;
            tmp.y += move_speed * Time.deltaTime;
            Target.transform.position = tmp;

            return;
        }

        base.Up();
    }

    override public void Down()
    {
        if (bShift)
        {
            tmp = Target.transform.position;
            tmp.y -= move_speed * Time.deltaTime;
            Target.transform.position = tmp;

            return;
        }

        base.Down();
    }


    override public void Left()
    {
        if (bShift)
        {
            tmp = Target.transform.position;
            tmp.x -= move_speed * Time.deltaTime;
            Target.transform.position = tmp;

            return;
        }

        base.Left();
    }

    override public void Right()
    {
        if (bShift)
        {
            tmp = Target.transform.position;
            tmp.x += move_speed * Time.deltaTime;
            Target.transform.position = tmp;

            return;
        }

        base.Right();
    }

    override public void WiseClock()
    {
        openangle += Time.deltaTime * merge_speed;

        Progress(openangle);
    }

    override public void Clock()
    {
        openangle -= Time.deltaTime * merge_speed;

        Progress(openangle);
    }
    #endregion

}
