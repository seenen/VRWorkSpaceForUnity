using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class MDTitaniumClamp : MedicalDevices
{
    public GameObject Target;

    //  旋转轴
    public GameObject Axis;

    public float Cur_Ange = 0;
    public float merge_speed = 1;

    public float OPEN_ANGLE = -30;
    public float CLOSE_ANGLE = 0;

    void Start()
    {
        MedicalDevicesMgr.instance.SetLeft(gameObject);

        Cur_Ange = OPEN_ANGLE;
    }

    void Update()
    {
        //transform.LookAt(Target.transform.position);

#if UNITY_EDITOR
        Progress(Cur_Ange);
#endif
    }

    public void Progress(float opendegree)
    {
        Cur_Ange = Mathf.Clamp(opendegree, OPEN_ANGLE, CLOSE_ANGLE);

        Axis.transform.localRotation = Quaternion.Euler(0, 0, Cur_Ange);
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
        Cur_Ange += Time.deltaTime * merge_speed;

        Progress(Cur_Ange);
    }

    override public void Clock()
    {
        Cur_Ange -= Time.deltaTime * merge_speed;

        Progress(Cur_Ange);
    }
    #endregion


    void OnTriggerEnter(Collider collider)
    {
        Debug.Log("MDTitaniumClamp.OnTriggerEnter" + collider.gameObject.name + ":" + Time.time);

    }

    void OnTriggerQuit(Collider collider)
    {
        Debug.Log("MDTitaniumClamp.OnTriggerQuit" + collider.gameObject.name + ":" + Time.time);

    }


}
