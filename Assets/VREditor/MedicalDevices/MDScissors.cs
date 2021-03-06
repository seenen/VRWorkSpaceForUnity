﻿using UnityEngine;
using System.Collections;
using LibVRGeometry;

[ExecuteInEditMode]
public class MDScissors : MedicalDevices
{
    //  旋转轴
    public GameObject Axis_L;
    public GameObject Axis_R;

    public float openangle = 0;
    public float merge_speed = 1;

    public float OPEN_ANGLE = 0;

    public float CLOSE_ANGLE = 25;

    void Start()
    {
        //MedicalDevicesMgr.instance.SetRight(gameObject);
    }

    void Update()
    {
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

        base.Up();
    }

    override public void Down()
    {

        base.Down();
    }


    override public void Left()
    {

        base.Left();
    }

    override public void Right()
    {

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

    #region 绑定组件
    public void BindComponent(HDComponentMessage cm)
    {

    }
    #endregion

}
