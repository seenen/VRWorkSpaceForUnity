using UnityEngine;
using System.Collections;
using System;

public class MedicalDevices : MonoBehaviour, IKeyBoardControl
{
    public int id;

    public Transform Root;

    public float move_speed = 10;

    public float rotate_speed = 10;

    virtual public void Clock()
    {
    }

    virtual public void WiseClock()
    {
    }

    virtual public void Up()
    {
        transform.position += transform.forward * move_speed * Time.deltaTime;
    }

    virtual public void Down()
    {
        transform.position -= transform.forward * move_speed * Time.deltaTime;
    }

    virtual public void Left()
    {
        Root.Rotate(Root.up, rotate_speed * Time.deltaTime, Space.World);
    }

    virtual public void Right()
    {
        Root.Rotate(Root.up, -rotate_speed * Time.deltaTime, Space.World);
    }

    protected bool bShift = false;

    virtual public void Shift(bool flag)
    {
        bShift = flag;
    }

    protected bool bCtrl = false;

    virtual public void Ctrl(bool flag)
    {
        bCtrl = flag;
    }

    protected bool bAlt = false;

    virtual public void Alt(bool flag)
    {
        bAlt = flag;
    }
}
